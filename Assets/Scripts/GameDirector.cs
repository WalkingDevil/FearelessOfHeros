using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
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

    [SerializeField] ResultManeger resultManeger;
    [SerializeField] CameraController cameraController;
    [SerializeField] CharacterGenerator enemyGene;
    [SerializeField] CharacterGenerator allyGene;
    [SerializeField] CardsGeneration cardsGeneration;
    [SerializeField] List<CharacterController> allyPrefabs;//味方用
    [SerializeField] List<CharacterController> enemyPrefabs;//敵用
    [SerializeField] List<MonsterCard> deckCards;

    string datapath;

    Action towerAction = null;
    [SerializeField] Slider towerSlider;
    [SerializeField] List<UserInterface> towerUser;//タワー用のスクリプト
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
                towerUser[0].SetSlider();
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
        datapath = Application.dataPath + "/SaveData.json";



        StateUpdate(allyPrefabs);
        StateUpdate(enemyPrefabs);
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
        towerAction = enemyGene.GenerateBs;
        towerUser[0].SetSlider();
        costSlider.maxValue = maxCost;
        costSlider.value = maxCost;
        expSlider.maxValue = maxExp;
        levelText.text = lv + level.ToString();
        cost = maxCost;

    }

    private void Update()
    {
        if (costSlider.value < costSlider.maxValue)
        {
            CostSlider();
        }

    }

    private void ChangeState()
    {
        switch(loadState)
        {
            case GameState.Ready:
                break;
            case GameState.InGame:
                break;
            case GameState.Pouse:
                break;
            case GameState.Clear:
                cameraController.endAction = () => resultManeger.ChengeText(false);//actionにテキストを入れる
                cameraController.FinishMove(false);         
                enemyGene.gameObject.SetActive(false);
                allyGene.gameObject.SetActive(false);
                break;
            case GameState.Over:
                cameraController.endAction = () => resultManeger.ChengeText(true);//actionにテキストを入れる
                cameraController.FinishMove(true);
                enemyGene.gameObject.SetActive(false);
                allyGene.gameObject.SetActive(false);
                break;
            default:
                break;
        }
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

    private void StateUpdate(List<CharacterController> characters)
    {
        foreach (CharacterController character in characters)
        {
            UserInterface user = character.gameObject.GetComponentInChildren<Canvas>().GetComponent<UserInterface>();//UserInterfaceスクリプトを受け取る
            user.ChengeState(level);
        }
    }

    public void SetMonsterCards(MonsterCard card)
    {
        deckCards.Add(card);
    }

    private void DisplayUpdate()
    {
        foreach(MonsterCard monsterCard in deckCards)
        {
            monsterCard.ResetDisplay();
        }
    }
}
