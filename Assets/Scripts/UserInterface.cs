using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    StatusManeger statusManeger;
    [SerializeField] Slider slider;
    public int maxHp;
    public int attack;
    public int defence;
    [SerializeField] bool cameraLook = true;//�J�������������邩
    [SerializeField] bool tower = false;//����HP���^���[�ł��邩�ǂ���


    void Start()
    {
        if(!tower)
        {
            statusManeger = new StatusManeger(maxHp, attack, defence);
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

    public int GetDamege()
    {
        return attack;
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
