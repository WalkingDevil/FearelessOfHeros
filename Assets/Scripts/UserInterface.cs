using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
   // [SerializeField] GameDirector.GameState overState;//ゲームオーバー
    StatusManeger statusManeger;
    [SerializeField] Slider slider;
    [SerializeField] int maxHp;
    [SerializeField] int attack;
    [SerializeField] int defence;

    // Start is called before the first frame update
    void Start()
    {
        statusManeger = new StatusManeger(maxHp, attack, defence);
        slider.maxValue = maxHp;
        slider.value = maxHp;
    }

    public void DamegeValue(int damege)
    {
      /*  playerSlider.value = hpManager.MinusHp(damege);
        if (hpManager.MinusHp(damege) <= 0)
        {
            GameDirector.LoadState = overState;
        }*/
    }

    public void HeelValue(int heel)
    {
       // playerSlider.value = hpManager.PlusHp(heel);
    }
}
