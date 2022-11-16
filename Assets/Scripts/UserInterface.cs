using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UserInterface : MonoBehaviour
{
    StatusManeger statusManeger;
    [SerializeField] DamegeShader damegeShader;
    [SerializeField] Slider slider;
    //�X�e�[�^�X
    [SerializeField] int earlyMaxHp;
    private int maxHp;
    [SerializeField] int earlyAttack;
    private int attack;
    [SerializeField] float earlyDefence;
    private float defence;
    [SerializeField] int cost;
    [SerializeField] int attributes;

    private int level = 1;
    [SerializeField] float magnification = 1.1f;
   // public List<int> state;
    [SerializeField] bool tower = false;//����HP���^���[�ł��邩�ǂ���

    private bool gach = false;
    private int expRate = 5;

    private void Awake()
    {
        maxHp = earlyMaxHp;
        attack = earlyAttack;
        defence = earlyDefence;
    }

    void Start()
    {

        statusManeger = new StatusManeger(earlyMaxHp, earlyAttack, earlyDefence, cost, attributes);

    }

    /// <summary>
    /// �X�e�[�^�X��Ԃ�
    /// 0:����HP
    /// 1:�U����
    /// 2:�h���
    /// 3:�R�X�g
    /// 4:�����U����
    /// 5:�����h���
    /// 6:����
    /// </summary>
    /// <param name="stateNum"></param>
    /// <returns></returns>
    public float GetState(int stateNum = 0)
    {
        switch (stateNum)
        {
            case 0:
                return earlyMaxHp;
            case 1:
                return attack;
            case 2:
                return defence;
            case 3:
                return cost;
            case 4:
                return maxHp;
            case 5:
                return earlyAttack;
            case 6:
                return earlyDefence;
            case 7:
                return attributes;
            default:
                return 1;
        }
    }

    //�K�`���̎��ɕ\����Off�ɂ���
    public void ChengeGach()
    {
        gach = true;
    }

    public bool DamegeValue(float damege, int enemyAttributes)
    {
        float returnHp = statusManeger.DamageCalculation(damege, attributes);
        if (tower)
        {
            slider.value = returnHp;
          //  return slider.value <= 0;
        }
        else
        {
            damegeShader.ChengeAlpha(statusManeger.GetRatio());
            
        }
        return returnHp <= 0;
    }

    /// <summary>
    /// ���x���ɉ����ăX�e�[�^�X���X�V����
    /// </summary>
    /// <param name="level">���݂̃��׃�</param>
    public void ChengeState(int level = 1)
    {
        if (level != 1)
        {
            maxHp = (int)(earlyMaxHp + magnification * level);
            attack = (int)(earlyAttack + magnification * level);
            defence = (int)(earlyDefence + magnification * level);
        }
        else
        {
            maxHp = earlyMaxHp;
            attack = earlyAttack;
            defence = earlyDefence;
        }

    }

    public void SetSlider()//�X���C�_�[���Z�b�g����
    {
        if (!tower)
        {
            statusManeger = new StatusManeger(maxHp, attack, defence, cost, attributes);
        }
        else
        {
            statusManeger = new StatusManeger(earlyMaxHp, attack, defence, cost, attributes);
            slider.maxValue = earlyMaxHp;
            slider.value = earlyMaxHp;

            if (slider.value <= 0)//�Z�b�g����Ă��Ȃ������Ƃ��̕ی�
            {
                slider.value = slider.maxValue;
            }
        }
    }

    public int ExpCalculation()
    {
        return maxHp / expRate;
    }
}
