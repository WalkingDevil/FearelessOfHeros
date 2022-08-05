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
        NewState();
        
    }

    private void NewState()
    {
        user = monster.GetComponentInChildren<Canvas>().GetComponent<UserInterface>();//�����X�^�[�I�u�W�F�N�g�̎q�ɂ���L�����o�X����UserInterface���󂯎��
        maxHp = user.GetState(0);
        attack = user.GetState(1);
        defence = user.GetState(2);
        cost = user.GetState(3);
        statasText.text = "HP�F" + maxHp.ToString("D4") + "\nAT�F" + attack.ToString("D4") + "\nDF�F" + defence.ToString("D4");//�X�e�[�^�X�\��
        costText.text = cost.ToString();//�R�X�g��\��

        monsterId = new MonsterId(id, attributes, maxHp, attack, defence, cost);
    }

    /// <summary>
    /// �X�e�[�^�X��Ԃ�
    /// 0�FID
    /// 1�F����
    /// 2�F�U����
    /// 3�F�h���
    /// 4�F�g�o
    /// 5�F�R�X�g
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public int GetState(int num)
    {
        NewState();   
        switch (num)
        {
            case 0:
                return id;
            case 1:
                return attributes;
            case 2:
                return attack;
            case 3:
                return defence;
            case 4:
                return maxHp;
            case 5:
                return cost;
            default:
                return 0;
        }
    }
    public void CardClick(Image clock)//�N�[���^�C��
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

        statasText.text = "HP�F" + maxHp.ToString("D4") + "\nAT�F" + attack.ToString("D4") + "\nDF�F" + defence.ToString("D4");//�X�e�[�^�X�\��
    }
}
