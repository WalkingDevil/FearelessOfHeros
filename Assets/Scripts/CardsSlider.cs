using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardsSlider : MonoBehaviour
{
    [SerializeField] RectTransform myRect;
    private float displayDirection; //��ʒ[�̍�
    private float defDisplayPos;
    private bool close = false;//�X���C�h����Ă��邩�ǂ���
    [SerializeField] float speed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        displayDirection = Screen.width - myRect.sizeDelta.x / 2;
        defDisplayPos = transform.position.x;
    }

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
