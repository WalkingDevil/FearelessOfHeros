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
    public enum GameState { Ready,�@InGame, Pouse, Clear, Over };
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

    private AudioController audioController;
    [SerializeField] TargetDisplay targetDisplay;
    [SerializeField] TowerController myTower;
    [SerializeField] LevelUpBonus levelUpBonus;
    [SerializeField] ResultManeger resultManeger;
    [SerializeField] CameraController cameraController;
    [SerializeField] CharacterGenerator enemyGene;
    [SerializeField] CharacterGenerator allyGene;
    [SerializeField] CardsGeneration cardsGeneration;
    [SerializeField] List<CharacterController> allyPrefabs;//�����p
    [SerializeField] List<CharacterController> enemyPrefabs;//�G�p
    [SerializeField] List<MonsterCard> deckCards;
    [SerializeField] PlayerController playerController;


    string datapath;

    Action towerAction = null;
    [SerializeField] Button sortieButton;//�o���p�{�^��
    [SerializeField] Slider towerSlider;
    [SerializeField] Slider cameraSlider;
    [SerializeField] GameObject OwnPlayPanel;
    [SerializeField] GameObject scroll;
    [SerializeField] List<UserInterface> towerUser;//�^���[�p�̃X�N���v�g
    [SerializeField] AudioSource bgm;
    [SerializeField] List<AudioClip> bgms;
    [SerializeField] int _towerCount = 3;
    [SerializeField] float audioVolume = 0.5f;
    public int towerCount
    {
        get { return _towerCount; }
        set
        {
            if(_towerCount != value)
            {
                _towerCount = value;
                towerUser.RemoveAt(0);
                towerUser[0].SetSlider();
                towerAction = enemyGene.GenerateBs;
            }
        }
    }
    [SerializeField] Slider costSlider;
    [SerializeField] float _costSpeed = 0.001f;//�R�X�g�𑝂₷�X�s�[�h�l
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
    [SerializeField] int maxCost = 0;//�R�X�g��Max�l

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
    public bool selfOperation = false;//�������Ԃ�
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
        savePath = saveData.Load();

        maxCost = savePath.cost;
        cost = maxCost;

        if (maxExp != 0)
        {
            maxExp = savePath.maxExp;
            exp = savePath.exp;            
        }

        
    }

    /// <summary>
    /// �R�X�g�̃X���C�_�[�̒l��ς���
    /// </summary>
    private void CostSlider()
    {
        cost += costSpeed;
    }

    private void Start()
    {
        //Audio�̏����ݒ�
        audioController = new AudioController(bgm, bgms[0]);
        audioController.ChengePlayAudio(true);
        audioController.SettingVolume(audioVolume);

        //�GHP�̐ݒ�
        towerAction = enemyGene.GenerateBs;
        towerUser[0].SetSlider();

        //�R�X�g�̐ݒ�
        costSlider.maxValue = maxCost;
        costSlider.value = maxCost;

        //���x���̐ݒ�
        _level = savePath.level;
        levelUpBonus.SetStartLevel(level);
        levelText.text = lv + level.ToString();

        //�G�A�����̃X�e�[�^�X�����x���ɉ������ݒ�
        StateUpdate(allyPrefabs);
        StateUpdate(enemyPrefabs);
        
        //�o���l�̐ݒ�
        expSlider.maxValue = maxExp;
        
        

    }

    private void Update()
    {
        if (costSlider.value < costSlider.maxValue && Time.timeScale != 0)
        {
            CostSlider();
        }

    }

    private void ChangeState()
    {
        switch(loadState)
        {
            case GameState.Clear:
                cameraController.ChengeSelfOperation(false);
                cameraController.endAction = () => resultManeger.ChengeText(false);//action�Ƀe�L�X�g������
                levelUpBonus.LevelBonus(level);
                scroll.SetActive(true);
                cameraController.FinishMove(false);         
                enemyGene.gameObject.SetActive(false);
                allyGene.gameObject.SetActive(false);
                OwnPlayPanel.SetActive(false);
                NewData();
                break;
            case GameState.Over:
                cameraController.ChengeSelfOperation(false);
                cameraController.endAction = () => resultManeger.ChengeText(true);//action�Ƀe�L�X�g������
                cameraController.FinishMove(true, selfOperation);
                enemyGene.gameObject.SetActive(false);
                allyGene.gameObject.SetActive(false);
                OwnPlayPanel.SetActive(false);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// �G�^���[�̃X���C�_�[�󋵂��󂯎��
    /// </summary>
    /// <param name="slider">�^���[�X���C�_�[</param>
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
    /// �f�[�^�̍X�V
    /// </summary>
    private void NewData()
    {
        savePath.level = level;
        savePath.exp = exp;
        savePath.maxExp = (int)maxExp;
        savePath.idData = levelUpBonus.SetIdData();
        if (maxCost != levelUpBonus.SetCost())
        {
            savePath.cost = levelUpBonus.SetCost();
        }

        saveData.Save(savePath);

    }

    /// <summary>
    /// ���x���A�b�v���ɃX�e�[�^�X���X�V������
    /// </summary>
    /// <param name="characters">�e�L�����N�^�[</param>
    private void StateUpdate(List<CharacterController> characters)
    {
        foreach (CharacterController character in characters)
        {
            //UserInterface�X�N���v�g���󂯎��
            UserInterface user = character.gameObject.GetComponent<UserInterface>();
            user.ChengeState(level);
        }
    }

    public void SetMonsterCards(MonsterCard card)
    {
        deckCards.Add(card);
    }

    /// <summary>
    /// ���x���A�b�v���Ƀf�b�L�̃X�e�[�^�X��ύX
    /// </summary>
    private void DisplayUpdate()
    {
        foreach(MonsterCard monsterCard in deckCards)
        {
            monsterCard.ResetDisplay();
        }
    }

    //���[�h�`�F���W��
    public IEnumerator BarinChenge()
    {
        selfOperation = true;
        sortieButton.gameObject.SetActive(false);

        audioController.ChengePlayAudio(false);//BGM���~�߂�

        cameraController.FinishMove(true);//�J�������ړ�������
        cameraController.endAction = () => myTower.ChengeMode();//action�Ƀe�L�X�g������

        yield return new WaitForSeconds(2f);

        playerController.gameObject.SetActive(true);
        myTower.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);

        //�J���������Ƃ̈ʒu��
        cameraController.CameraMoveAction();

        OwnPlayPanel.SetActive(true);
        cameraController.ChengeSelfOperation(true);
        targetDisplay.gameObject.SetActive(true);
        cameraSlider.gameObject.SetActive(false);

        //BGM��ς��čĐ�
        audioController.ChengeClip(bgms[1]);
        audioController.ChengePlayAudio(true);
        yield return null;
    }

    public void BrainMuscleMode()//�{�^���������ꂽ�烂�[�h��ς���
    {
        StartCoroutine(BarinChenge());

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
