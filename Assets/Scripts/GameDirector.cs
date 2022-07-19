using System.Collections;
using System.Collections.Generic;
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
            }
        }
    }

    [SerializeField] List<Slider> costSliders;//生成する位置
    private int maxCost = 0;
    private int plusCost = 0;
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
                    var plus = value - plusCost;
                    if (maxCost >= _cost + value)//maxの値に達していなかったら
                    {
                        foreach (Slider s in costSliders)
                        {
                            if (s.value < s.maxValue)
                            {
                                s.value += s.maxValue;
                                plus--;
                            }

                            if (plus == 0)
                            {
                                break;
                            }
                        }
                    }
                    else//maxの値だったら
                    {
                        _cost = maxCost;
                        foreach (Slider s in costSliders)
                        {
                            if (s.value < s.maxValue)
                            {
                                s.value += s.maxValue;
                            }
                        }
                    }
                }
                plusCost = value;

                if(_cost != maxCost)
                {
                    StartCoroutine(CostSlider());
                }
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

    IEnumerator CostSlider()
    {
        foreach (Slider s in costSliders)
        {
            if (s.value < s.maxValue)
            {   
                DOTween.To(() => s.value, (t) => s.value = t, 1, 5f);
                yield return new WaitForSeconds(5.5f);
                // .OnComplete(() => ResetButton(clock));
            }
        }
    }

    private void Start()
    {
        towerUser[0].SetSlider();
        maxCost = costSliders.Count;
        cost = maxCost;
        plusCost = maxCost;
    }

    private void Update()
    {
     //   Debug.Log(cost);
    }

    private void ChangeState()
    {

    }
}
