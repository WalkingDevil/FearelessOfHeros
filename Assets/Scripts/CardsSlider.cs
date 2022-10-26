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
    // Start is called before the first frame update
    void Start()
    {
        displayDirection = Screen.width - myRect.sizeDelta.x / 2;
        defDisplayPos = transform.position.x;
    }
    private void Update()
    {
        Debug.Log(myRect.sizeDelta.x / 2);
    }

    public void OnSlideButton()
    {
        if(close)
        {
            transform.DOMoveX(defDisplayPos, 1);
        }
        else
        {
            transform.DOMoveX(displayDirection, 1);
        }

        close = !close;
    }
}
