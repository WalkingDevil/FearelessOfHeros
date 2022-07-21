using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    StatusManeger statusManeger;
    [SerializeField] Slider slider;
    [SerializeField] int maxHp;
    [SerializeField] int attack;
    [SerializeField] int defence;
    [SerializeField] int cost;
   // public List<int> state;
    [SerializeField] bool cameraLook = true;//�J�������������邩
    [SerializeField] bool tower = false;//����HP���^���[�ł��邩�ǂ���


    void Start()
    {
        if(!tower)
        {
            statusManeger = new StatusManeger(maxHp, attack, defence, cost);
            slider.maxValue = maxHp;
            slider.value = maxHp;
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
        switch(stateNum)
        {
            case 0:
                return maxHp;
            case 1:
                return attack;
            case 2:
                return defence;
            case 3:
                return cost;
                default:
                return 1;
        }  
    }

    public bool DamegeValue(int damege)
    {
        slider.value = statusManeger.DamageCalculation(damege);
        return slider.value <= 0;
    }

    public void HeelValue(int heel)
    {
       // playerSlider.value = hpManager.PlusHp(heel);
    }

    public void SetSlider()//�X���C�_�[���Z�b�g����
    {
        statusManeger = new StatusManeger(maxHp, attack, defence);
        slider.maxValue = maxHp;
        slider.value = maxHp;
        if(slider.value == 0)//�Z�b�g����Ă��Ȃ������Ƃ��̕ی�
        {
            slider.value = slider.maxValue;
        }
    }
}
