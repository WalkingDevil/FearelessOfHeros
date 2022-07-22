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
    [SerializeField] int maxHp;
    [SerializeField] int attack;
    [SerializeField] int defence;

    public int time;

    void Start()
    {
        maxHp = user.GetState(0);
        attack = user.GetState(1);
        defence = user.GetState(2);
        user = monster.GetComponentInChildren<Canvas>().GetComponent<UserInterface>();//�����X�^�[�I�u�W�F�N�g�̎q�ɂ���L�����o�X����UserInterface���󂯎��
        statasText.text = "HP�F" + maxHp.ToString("D4") + "\nAT�F" + attack.ToString("D4") + "\nDF�F" + defence.ToString("D4");//�X�e�[�^�X�\��
        costText.text = user.GetState(3).ToString();//�R�X�g��\��
    }

    public void CardClick(Image clock)//�N�[���^�C��
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
