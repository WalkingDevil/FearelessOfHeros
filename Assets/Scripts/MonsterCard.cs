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
    private int maxHp;
    private int attack;
    private int defence;

    public int time;

    void Start()
    {
        user = monster.GetComponentInChildren<Canvas>().GetComponent<UserInterface>();//�����X�^�[�I�u�W�F�N�g�̎q�ɂ���L�����o�X����UserInterface���󂯎��
        maxHp = user.GetState(0);
        attack = user.GetState(1);
        defence = user.GetState(2);

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

    public void ResetDisplay()
    {
        maxHp = user.GetState(4);
        attack = user.GetState(1);
        defence = user.GetState(2);

        statasText.text = "HP�F" + maxHp.ToString("D4") + "\nAT�F" + attack.ToString("D4") + "\nDF�F" + defence.ToString("D4");//�X�e�[�^�X�\��
    }
}
