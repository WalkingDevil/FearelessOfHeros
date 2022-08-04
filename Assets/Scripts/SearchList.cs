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
    private List<MonsterCard> defMonsterCards = new List<MonsterCard>();
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
               defMonsterCards.Add(card);
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
        List<MonsterCard> monsterCards = new List<MonsterCard>();
        DestroyCards();
        List<int> attributeses = new List<int>();
        for (int b = 0; b < refineButtons.Count; b++)//表示する型を入れる
        {
            if (refineButtons[b].interactable)
            {
                attributeses.Add(b);
            }
        }

        foreach (MonsterCard card in defMonsterCards)
        {
            if (attributeses.Contains(card.GetAttributes()))
            {
                monsterCards.Add(card);
            }

        }
        foreach (Button b in refineButtons)
        {

        }


        foreach (MonsterCard cards in monsterCards)
        {
            Instantiate(cards.gameObject, content);
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
        DestroyCards();


       // monsterCards = defMonsterCards;
    }


    private void DestroyCards()
    {
        if (content.childCount != 0)
        {
            foreach (Transform t in content.gameObject.transform)
            {
                Destroy(t.gameObject);
            }
        }
    }

    private void ResetButtons(List<Button> buttons)
    {
        foreach(Button b in buttons)//OffになっているボタンをOnにする
        {
            if(!b.interactable)
            {
                b.interactable = true;
            }
        }
    }
}
