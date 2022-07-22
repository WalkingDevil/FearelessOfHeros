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
    private int level = 1;
    [SerializeField] float magnification = 1.1f;
   // public List<int> state;
    [SerializeField] bool cameraLook = true;//カメラを見させるか
    [SerializeField] bool tower = false;//このHPがタワーであるかどうか


    private int expRate = 5;
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
    /// ステータスを返す
    /// 0:HP
    /// 1:攻撃力
    /// 2:防御力
    /// 3:コスト
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

    /// <summary>
    /// レベルに応じてステータスを更新する
    /// </summary>
    /// <param name="level">現在のレべル</param>
    public void ChengeState(int level)
    {
        if (level != this.level)
        {
            maxHp = (int)(maxHp * magnification * level);
            attack = (int)(attack * magnification * level);
            defence = (int)(defence * magnification * level);
            this.level = level;
        }
    }

    public void HeelValue(int heel)
    {
       // playerSlider.value = hpManager.PlusHp(heel);
    }

    public void SetSlider()//スライダーをセットする
    {
        statusManeger = new StatusManeger(maxHp, attack, defence);
        slider.maxValue = maxHp;
        slider.value = maxHp;
        if(slider.value == 0)//セットされていなかったときの保険
        {
            slider.value = slider.maxValue;
        }
    }

    public int ExpCalculation()
    {
        return maxHp / expRate;
    }
}
