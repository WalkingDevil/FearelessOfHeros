using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MonsterCard : MonoBehaviour
{
    [SerializeField] GameDirector gameDirector;
    private UserInterface user;
    [SerializeField] GameObject monster;
    [SerializeField] Button cardButton;
    [SerializeField] Text statasText;
    [SerializeField] Text costText;

    public int time;

    void Start()
    {
        user = monster.GetComponentInChildren<Canvas>().GetComponent<UserInterface>();//モンスターオブジェクトの子にあるキャンバスからUserInterfaceを受け取る
        statasText.text = "HP：" + user.GetState(0).ToString("D4") + "\nAT：" + user.GetState(1).ToString("D4") + "\nDF：" + user.GetState(2).ToString("D4");//ステータス表示
        costText.text = user.GetState(3).ToString();//コストを表示
    }

    /*Update is called once per frame
    void Update()
    {
        
    }*/

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
}
