using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UserInterface : MonoBehaviour
{
    StatusManeger statusManeger;
    [SerializeField] Slider slider;
    [SerializeField] int earlyMaxHp;
    private int maxHp;
    [SerializeField] int earlyAttack;
    private int attack;
    [SerializeField] int earlyDefence;
    private int defence;
    [SerializeField] int cost;
    private int level = 1;
    [SerializeField] float magnification = 1.1f;
   // public List<int> state;
    [SerializeField] bool cameraLook = true;//�J�������������邩
    [SerializeField] bool tower = false;//����HP���^���[�ł��邩�ǂ���


    private int expRate = 5;

    private void Awake()
    {
        maxHp = earlyMaxHp;
        attack = earlyAttack;
        defence = earlyDefence;
    }

    void Start()
    {
        if (!tower)
        {
            statusManeger = new StatusManeger(earlyMaxHp, earlyAttack, earlyDefence, cost);
            slider.maxValue = earlyMaxHp;
            slider.value = earlyMaxHp;
        }
    }

    void Update()
    {
        if (cameraLook)
        {
            transform.LookAt(Camera.main.transform.position);
        }
    }
    /// <summary>
    /// �X�e�[�^�X��Ԃ�
    /// 0:HP
    /// 1:�U����
    /// 2:�h���
    /// 3:�R�X�g
    /// </summary>
    /// <param name="stateNum"></param>
    /// <returns></returns>
    public int GetState(int stateNum = 0)
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
            default:
                return 1;
        }
    }

    public bool DamegeValue(int damege)
    {
        slider.value = statusManeger.DamageCalculation(damege);
        return slider.value <= 0;
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

    public void HeelValue(int heel)
    {
       // playerSlider.value = hpManager.PlusHp(heel);
    }

    public void SetSlider()//�X���C�_�[���Z�b�g����
    {
        if (!tower)
        {
            statusManeger = new StatusManeger(maxHp, attack, defence);
            slider.maxValue = maxHp;
            slider.value = maxHp;
        }
        else
        {
            statusManeger = new StatusManeger(earlyMaxHp, attack, defence);
            slider.maxValue = earlyMaxHp;
            slider.value = earlyMaxHp;
        }

        if(slider.value <= 0)//�Z�b�g����Ă��Ȃ������Ƃ��̕ی�
        {
            slider.value = slider.maxValue;
        }
    }

    public int ExpCalculation()
    {
        return maxHp / expRate;
    }
}
