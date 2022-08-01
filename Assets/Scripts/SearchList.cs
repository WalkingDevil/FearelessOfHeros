using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class SearchList : MonoBehaviour
{
    [SerializeField] Scrollbar scrollbar;
    [SerializeField] RectTransform content;
    [SerializeField] List<Button> refineButtons;
    [SerializeField] List<Button> sortButtons;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Search()
    {

    }

    public void RefineButton(Button button)
    {
        button.interactable = false;
    }

    public void ResetSearch()
    {
        foreach(Button b in refineButtons)
        {
            if(!b.interactable)
            {
                b.interactable = true;
            }
        }
    }
}
