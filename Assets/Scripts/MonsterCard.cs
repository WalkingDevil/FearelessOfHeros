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
        user = monster.GetComponentInChildren<Canvas>().GetComponent<UserInterface>();//�����X�^�[�I�u�W�F�N�g�̎q�ɂ���L�����o�X����UserInterface���󂯎��
        statasText.text = "HP�F" + user.maxHp.ToString("D4") + "\nAT�F" + user.attack.ToString("D4") + "\nDF�F" + user.defence.ToString("D4");//�X�e�[�^�X�\��
        costText.text = user.cost.ToString();//�R�X�g��\��
    }

    /*Update is called once per frame
    void Update()
    {
        
    }*/

    public void CardClick(Image clock)//�N�[���^�C��
    {
        if (gameDirector.cost >= user.cost)
        {
            cardButton.interactable = false;
            gameDirector.cost -= user.cost;
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
