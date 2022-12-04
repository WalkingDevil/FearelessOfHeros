using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using DG.Tweening;

public class GameDirector : MonoBehaviour
{
    public enum GameState { Ready,　InGame, Pouse, Clear, Over };
    private GameState _loadState = GameState.Ready;
    public GameState loadState
    {
        get { return gameDire._loadState; }
        set
        {
            if(gameDire._loadState != value)
            {
                gameDire._loadState = value;
                gameDire.ChangeState();
            }
        }
    }


    private SaveData saveData = new SaveData();
    private SavePath savePath = new SavePath();

    private AudioController bgmAudioCo;
    private AudioController seAudioCo;
    [SerializeField] TargetDisplay targetDisplay;
    [SerializeField] TowerController myTower;
    [SerializeField] LevelUpBonus levelUpBonus;
    [SerializeField] ResultManeger resultManeger;
    [SerializeField] CameraController cameraController;
    [SerializeField] CharacterGenerator enemyGene;
    [SerializeField] CharacterGenerator allyGene;
    [SerializeField] CardsGeneration cardsGeneration;
    [SerializeField] List<CharacterController> allyPrefabs;//味方用
    [SerializeField] List<CharacterController> enemyPrefabs;//敵用
    [SerializeField] List<MonsterCard> deckCards;
    [SerializeField] PlayerController playerController;


    string datapath;

    Action towerAction = null;
    [SerializeField] Button sortieButton;//出撃用ボタン
    [SerializeField] Slider towerSlider;
    [SerializeField] Slider cameraSlider;
    [SerializeField] GameObject OwnPlayPanel;
    [SerializeField] GameObject scroll;
    [SerializeField] List<UserInterface> towerUser;//タワー用のスクリプト
    [SerializeField] UserInterface myTowerUser;//自分の

    [SerializeField] AudioSource se;
    [SerializeField] AudioSource bgm;
    [SerializeField] List<AudioClip> bgms;
    [SerializeField] List<AudioClip> ses;

    [SerializeField] float audioVolume = 0.5f;
    //砦の残り数
    [SerializeField] int _towerCount = 3;

    public int towerCount
    {
        get { return _towerCount; }
        set
        {
            if(_towerCount != value)
            {
                _towerCount = value;
                towerUser.RemoveAt(0);
                towerUser[0].SetSlider();//次の砦のスライダーをセットする
                towerAction = enemyGene.GenerateBs;
            }
        }
    }
    [SerializeField] Slider costSlider;
    [SerializeField] float _costSpeed = 0.001f;//コストを増やすスピード値
    public float costSpeed
    {
        get { return _costSpeed; }
        set
        {
            if(costSpeed != value)
            {
                costSpeed = value;
            }
        }
    }
    [SerializeField] int maxCost = 0;//コストのMax値

    private float _cost;
    public float cost
    {
        get { return _cost; }
        set
        {
            if (_cost != value)
            {
                _cost = value;
                if(_cost > maxCost)
                {
                    _cost = maxCost;
                }
                costSlider.value = _cost;
            }

        } 
    }

    [SerializeField] Slider expSlider;
    private int _level = 1;
    public int level
    {
        get { return _level; }
        set
        {
            if(level != value)
            {
                _level = value;
                levelText.text = lv + _level.ToString();
                StateUpdate(allyPrefabs);
                DisplayUpdate();
            }
        }
    }

    [SerializeField] Text levelText;
    [SerializeField] string lv;
    [SerializeField] float chengeMaxHp;
    [SerializeField] float _maxExp;
    public bool selfOperation = false;//自操作状態か
    public float maxExp
    {
        get { return _maxExp; }
        set
        {
            if (maxExp != value)
            {
                _maxExp = value;
                expSlider.maxValue = _maxExp;
            }
        }
    }

    private float _exp;
    public float exp
    {
        get { return _exp; }
        set
        {
            if(exp != value)
            {
                _exp = value;
                if (_maxExp <= _exp)
                {
                    level++;
                    _exp = _exp - maxExp;
                    float halfMaxHp = maxExp / 2;
                    maxExp = (int)halfMaxHp * chengeMaxHp;
                    if(_exp / maxExp >= 1)
                    {
                        exp = _exp;
                    }
                }
                expSlider.value = _exp;
            }
        }
    }
    //倒した体数
    private int enemyDefeatCount;
    //味方が死んだ数
    private int allyDieCount;


    static GameDirector gameDire = null;
    private void Awake()
    {
        if (gameDire == null)
        {
            gameDire = this;
        }
        else
        {
            Destroy(this);
        }
        loadState = GameState.InGame;

        savePath = saveData.Load();

        //コストをセットする
        maxCost = savePath.cost;
        cost = maxCost;

        if (maxExp != 0)
        {
            maxExp = savePath.maxExp;
            exp = savePath.exp;            
        }

        
    }

    /// <summary>
    /// コストのスライダーの値を変える
    /// </summary>
    private void CostSlider()
    {
        cost += costSpeed;
    }

    private void Start()
    {
        //Audioの初期設定
        bgmAudioCo = new AudioController(bgm, bgms[0]);
        bgmAudioCo.ChengePlayAudio(true);
        bgmAudioCo.SettingVolume(audioVolume);
        seAudioCo = new AudioController(se);

        //敵HPの設定
        towerAction = enemyGene.GenerateBs;
        towerUser[0].SetSlider();

        //陣地の設定
        myTowerUser.SetSlider();

        //コストの設定
        costSlider.maxValue = maxCost;
        costSlider.value = maxCost;

        //レベルの設定
        _level = savePath.level;
        levelUpBonus.SetStartLevel(level);
        levelText.text = lv + level.ToString();

        //敵、味方のステータスをレベルに応じた設定
        StateUpdate(allyPrefabs);
        StateUpdate(enemyPrefabs);
        
        //経験値の設定
        expSlider.maxValue = maxExp;
        
        

    }

    private void Update()
    {
        //コストぞ増加させる
        if (costSlider.value < costSlider.maxValue)
        {
            CostSlider();
        }

    }
    /// <summary>
    /// ゲームステート
    /// </summary>
    private void ChangeState()
    {
        switch(loadState)
        {
            case GameState.Clear:
                ProcessCommon(0, false);
                //スコア表示
                levelUpBonus.LevelBonus(level);
                levelUpBonus.ClearBonus(enemyDefeatCount, allyDieCount);
                scroll.SetActive(true);
                //データを更新
                NewData();
                break;
            case GameState.Over:
                ProcessCommon(1, true);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 共通している処理
    /// </summary>
    /// <param name="seNum">SEの番号</param>
    /// <param name="over">ゲームオーバーかどうか</param>
    private void ProcessCommon(int seNum = 0, bool over = true)
    {
        bgmAudioCo.ChengePlayAudio(false);
        cameraController.ChengeSelfOperation(false);//追従を切る
        cameraController.endAction = () => resultManeger.ChengeText(over);//actionにテキストを入れる
        cameraController.FinishMove(over, selfOperation);

        enemyGene.gameObject.SetActive(false);
        allyGene.gameObject.SetActive(false);

        OwnPlayPanel.SetActive(false);
        seAudioCo.ChengeClip(ses[seNum]);
        seAudioCo.ChengePlayAudio(true);
    }

    /// <summary>
    /// 敵タワーのスライダー状況を受け取る
    /// </summary>
    /// <param name="slider">タワースライダー</param>
    public void GetTowerValue(Slider slider)
    {
        if(slider.value < slider.maxValue / 2)
        {
            if(towerAction != null)
            {
                towerAction();
                towerAction = null;
            }
        }
        else if(slider.value <= 0)
        {
            towerUser[0].SetSlider();
        }
    }

    /// <summary>
    /// データの更新
    /// </summary>
    private void NewData()
    {
        savePath.level = level;
        savePath.exp = exp;
        savePath.maxExp = (int)maxExp;
        savePath.idData = levelUpBonus.SetIdData();
        savePath.krystaal += levelUpBonus.SetKrystaal();
        if (maxCost != levelUpBonus.SetCost())
        {
            savePath.cost = levelUpBonus.SetCost();
        }

        saveData.Save(savePath);
    }

    /// <summary>
    /// レベルアップ時にステータスを更新させる
    /// </summary>
    /// <param name="characters">各キャラクター</param>
    private void StateUpdate(List<CharacterController> characters)
    {
        foreach (CharacterController character in characters)
        {
            //UserInterfaceスクリプトを受け取る
            UserInterface user = character.gameObject.GetComponent<UserInterface>();
            user.ChengeState(level);
        }
    }

    public void SetMonsterCards(MonsterCard card)
    {
        deckCards.Add(card);
    }

    /// <summary>
    /// レベルアップ時にデッキのステータスを変更
    /// </summary>
    private void DisplayUpdate()
    {
        foreach(MonsterCard monsterCard in deckCards)
        {
            monsterCard.ResetDisplay();
        }
    }

    //モードチェンジ時
    public IEnumerator BarinChenge()
    {
        selfOperation = true;
        sortieButton.gameObject.SetActive(false);

        bgmAudioCo.ChengePlayAudio(false);//BGMを止める

        cameraController.FinishMove(true);//カメラを移動させる
        cameraController.endAction = () => myTower.ChengeMode();//actionにテキストを入れる

        yield return new WaitForSeconds(3f);

        playerController.gameObject.SetActive(true);
        myTower.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);

        //カメラをもとの位置に
        cameraController.CameraMoveAction();

        OwnPlayPanel.SetActive(true);
        cameraController.ChengeSelfOperation(true);
        targetDisplay.gameObject.SetActive(true);
        cameraSlider.gameObject.SetActive(false);

        //BGMを変えて再生
        bgmAudioCo.ChengeClip(bgms[1]);
        bgmAudioCo.ChengePlayAudio(true);
        yield return null;
    }

    public void BrainMuscleMode()//ボタンが押されたらモードを変える
    {
        StartCoroutine(BarinChenge());

    }

    public void SetDestroyCount(bool enemy)
    {
        if(enemy)
        {
            enemyDefeatCount++;
        }
        else
        {
            allyDieCount++;
        }
    }
    public void ChengeScene(bool con)
    {
        if(con)
        {
           
            SceneManager.LoadScene("GameScene");
        }
        else
        {

            SceneManager.LoadScene("TitleScene");
        }
    }
}
