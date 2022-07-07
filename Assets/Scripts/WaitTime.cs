using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WaitTime : MonoBehaviour
{
    [SerializeField] Button cardButton;
    public int time;

    void Start()
    {
        
    }

    /*Update is called once per frame
    void Update()
    {
        
    }*/

    public void CardClick(Image clock)
    {
        cardButton.interactable = false;
        DOTween.To
            (
            () => clock.fillAmount,
            (t) => clock.fillAmount = t,
            1,
            time
            )
            .OnComplete(() => cardButton.interactable = true) 
            .OnComplete(() => clock.fillAmount = 0);
    }
}
