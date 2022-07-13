using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MonsterCard : MonoBehaviour
{
    private UserInterface user;
    [SerializeField] GameObject monster;
    [SerializeField] Button cardButton;
    [SerializeField] Text statasText;

    public int time;

    void Start()
    {
        user = monster.GetComponentInChildren<Canvas>().GetComponent<UserInterface>();//モンスターオブジェクトの子にあるキャンバスからUserInterfaceを受け取る
        statasText.text = "HP：" + user.maxHp.ToString("D4") + "\nAT：" + user.attack.ToString("D4") + "\nDF：" + user.defence.ToString("D4");//ステータス表示
    }

    /*Update is called once per frame
    void Update()
    {
        
    }*/

    public void CardClick(Image clock)//クールタイム
    {
        cardButton.interactable = false;
        DOTween.To
            (
            () => clock.fillAmount,
            (t) => clock.fillAmount = t,
            1,
            time
            )
            .OnComplete(() => ResetButton(clock));
    }

    private void ResetButton(Image clock)
    {
        cardButton.interactable = true;
        clock.fillAmount = 0;
    }
}
