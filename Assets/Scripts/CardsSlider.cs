using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardsSlider : MonoBehaviour
{
    [SerializeField] RectTransform myRect;
    private float displayDirection; //画面端の差
    private float defDisplayPos;
    private bool close = false;//スライドを閉じているかどうか
    [SerializeField] float speed = 0.5f;


    void Start()
    {
        displayDirection = Screen.width - myRect.sizeDelta.x / 2;
        defDisplayPos = transform.position.x;
    }

    /// <summary>
    /// カード一覧を表示を切り替える
    /// </summary>
    public void OnSlideButton()
    {
        if(close)
        {
            transform.DOMoveX(defDisplayPos, speed);
        }
        else
        {
            transform.DOMoveX(displayDirection, speed);
        }

        close = !close;
    }
}
