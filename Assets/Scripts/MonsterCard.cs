using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MonsterCard : MonoBehaviour
{
    private MonsterId monsterId;
    [SerializeField] GameDirector gameDirector;
    private UserInterface user;
    [SerializeField] GameObject monster;
    [SerializeField] Button cardButton;
    [SerializeField] Text statasText;
    [SerializeField] Text costText;

    [SerializeField] int id;
    private int maxHp;
    private int attack;
    private int defence;
    private int cost;
    public int time;

    void Start()
    {
        user = monster.GetComponentInChildren<Canvas>().GetComponent<UserInterface>();//モンスターオブジェクトの子にあるキャンバスからUserInterfaceを受け取る
        maxHp = user.GetState(0);
        attack = user.GetState(1);
        defence = user.GetState(2);
        cost = user.GetState(3);
        statasText.text = "HP：" + maxHp.ToString("D4") + "\nAT：" + attack.ToString("D4") + "\nDF：" + defence.ToString("D4");//ステータス表示
        costText.text = cost.ToString();//コストを表示

        monsterId = new MonsterId(id, maxHp, attack, defence, cost);
    }

    public void CardClick(Image clock)//クールタイム
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
