using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class SearchList : MonoBehaviour
{
    private SaveData saveData = new SaveData();
    private SavePath savePath = new SavePath();
    [SerializeField] Scrollbar scrollbar;
    [SerializeField] RectTransform content;
    [SerializeField] List<Button> refineButtons;
    [SerializeField] List<Button> sortButtons;
    [SerializeField] List<MonsterCard> cardsList;
    [SerializeField] List<int> myCardsId = new List<int>();
    private List<MonsterCard> monsterCards = new List<MonsterCard>();
    string datapath;
    void Start()
    {
        savePath = saveData.Load();
        myCardsId = savePath.idData;
        Debug.Log(myCardsId.Count);
        foreach(var card in cardsList)
        {
            if (myCardsId.Contains(card.GetId()))//IDが含まれているかどうか
            {
                monsterCards.Add(card);
            }
        }
        //monsterCards = 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Search()
    {
        foreach (var card in monsterCards)
        {
            Instantiate(card.gameObject, content);
        }

    }

    //押したボタンを押せなくする
    public void RefineButton(Button button)
    {
        button.interactable = false;
    }

    public void SortButton(Button button)
    {
        foreach(Button b in sortButtons)
        {
            if(b != button)
            {
                if(!b.interactable)
                {
                    b.interactable = true;
                }
            }
        }
    }


    public void ResetSearch()
    {
        ResetButtons(refineButtons);
        ResetButtons(sortButtons);
    }

    private void ResetButtons(List<Button> buttons)
    {
        foreach(Button b in buttons)
        {
            if(!b.interactable)
            {
                b.interactable = true;
            }
        }
    }
}
