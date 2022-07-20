using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameDirector : MonoBehaviour
{
    public enum GameState { InGame, Pouse, OutGame };
    private GameState _loadState = GameState.InGame;
    static public GameState loadState
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
    [SerializeField] CharacterGenerator enemyGene;



    Action towerAction = null;
    [SerializeField] Slider towerSlider;
    [SerializeField] List<UserInterface> towerUser;//タワー用のスクリプト
    [SerializeField] int _towerCount = 0;
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
    private int plusCost = 0; //現在の値の差を入れる
    private int _cost;
    public int cost
    {
        get { return _cost; }
        set
        {
            if (_cost != value)
            {
                _cost = value;
                if(plusCost < value)
                { 
                    var plus = value - plusCost;//現在の値の差
                    if (maxCost > _cost + value)//maxの値に達していなかったら
                    {
                        _cost = plus;
                        costSlider.value = plus;
                    }
                    else//maxの値だったら
                    {
                        _cost = maxCost;
                        costSlider.value = maxCost;
                    }
                }
                plusCost = value;
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
    }

    /// <summary>
    /// コストのスライダーの値を変える
    /// </summary>
    private void CostSlider()
    {
        costSlider.value += costSpeed;
    }

    private void Start()
    {
        towerAction = enemyGene.GenerateBs;
        towerUser[0].SetSlider();
        costSlider.maxValue = maxCost;
        costSlider.value = maxCost;
        cost = maxCost;
        plusCost = maxCost;
    }

    private void Update()
    {
        Debug.Log(cost);
        if (costSlider.value < costSlider.maxValue)
        {
            CostSlider();
        }
    }

    private void ChangeState()
    {

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
    }
}
