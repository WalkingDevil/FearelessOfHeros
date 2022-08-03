using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MonsterCard : MonoBehaviour
{
    private MonsterId monsterId;
    [SerializeField] GameDirector gameDirector;
    private UserInterface user;
    [SerializeField] GameObject monster;
    [SerializeField] Button cardButton;
    [SerializeField] Text statasText;
    [SerializeField] Text costText;
    [SerializeField] string gameScene = "GameScene";
    [SerializeField] int id;
    [SerializeField] int attributes;
    private int maxHp;
    private int attack;
    private int defence;
    private int cost;
    public int time;

    private bool checkScene;
    void Start()
    {
        checkScene = SceneManager.GetActiveScene().name == gameScene;
        GetState();
        
    }

    private void GetState()
    {
        user = monster.GetComponentInChildren<Canvas>().GetComponent<UserInterface>();//モンスターオブジェクトの子にあるキャンバスからUserInterfaceを受け取る
        maxHp = user.GetState(0);
        attack = user.GetState(1);
        defence = user.GetState(2);
        cost = user.GetState(3);
        statasText.text = "HP：" + maxHp.ToString("D4") + "\nAT：" + attack.ToString("D4") + "\nDF：" + defence.ToString("D4");//ステータス表示
        costText.text = cost.ToString();//コストを表示

        monsterId = new MonsterId(id, attributes, maxHp, attack, defence, cost);
    }

    public int GetId()
    {
        GetState();
        return id;
    }
    public void CardClick(Image clock)//クールタイム
    {
        if (checkScene)
        {
            if (gameDirector.cost >= user.GetState(3))
            {
                cardButton.interactable = false;
                gameDirector.cost -= user.GetState(3);
                DOTween.To
                    (
                    () => clock.fillAmount,
                    (t) => clock.fillAmount = t,
                    1,
                    time
                    )
                    .OnComplete(() => ResetButton(clock));
            }
        }

    }

    private void ResetButton(Image clock)
    {
        cardButton.interactable = true;
        clock.fillAmount = 0;
    }

    public void ResetDisplay()
    {
        maxHp = user.GetState(4);
        attack = user.GetState(1);
        defence = user.GetState(2);

        statasText.text = "HP：" + maxHp.ToString("D4") + "\nAT：" + attack.ToString("D4") + "\nDF：" + defence.ToString("D4");//ステータス表示
    }
}
