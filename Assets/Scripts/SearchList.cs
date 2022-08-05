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
    [SerializeField] RectTransform selectPanel;
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
            if (myCardsId.Contains(card.GetState(0)))//IDが含まれているかどうか
            {
               defMonsterCards.Add(card);
            }
        }
        Search(false);
        //monsterCards = 
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void Search(bool def = true)
    {
        List<MonsterCard> monsterCards = new List<MonsterCard>();
        List<int> selects = new List<int>();
        DestroyCards();
        selects = GetSelectsList();

        List<int> attributeses = new List<int>();
        for (int b = 0; b < refineButtons.Count; b++)//表示する型を入れる
        {
            if (refineButtons[b].interactable)
            {
                attributeses.Add(b);
            }
        }

        monsterCards = GenerationMonsters(attributeses, selects);

        if (def)
        {
            int sortNumber = 0;
            for (int b = 0; b < sortButtons.Count; b++)
            {
                if (!sortButtons[b].interactable)
                {
                    sortNumber = b + 2;
                    break;
                }
            }

            monsterCards = SortSearch(monsterCards, sortNumber);
        }

        if(selectPanel.childCount != 0)
        {

        }


        foreach (MonsterCard cards in monsterCards)
        {
            GameObject ob = Instantiate(cards.gameObject, content);
            ob.AddComponent<DragObject>();
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

    private List<MonsterCard> GenerationMonsters(List<int> attributes, List<int> select)
    {
        List<MonsterCard> cards = new List<MonsterCard>();
        foreach (MonsterCard card in defMonsterCards)
        {
            int cardId = card.GetState(0);
            int cardAtt = card.GetState(1);
            if (attributes.Contains(cardAtt) && !select.Contains(cardId))
            {
                cards.Add(card);
            }
        }
        return cards;
    }
    private List<int> GetSelectsList()
    {
        List<int> list = new List<int>();
        for (int s = 0; s < selectPanel.childCount; s++)
        {
            list.Add(selectPanel.GetChild(s).GetComponent<MonsterCard>().GetState(0));
        }

        return list;
    }
    public void ResetSearch()
    {
        ResetButtons(refineButtons);
        ResetButtons(sortButtons);
        DestroyCards();

        Search(false);
       // monsterCards = defMonsterCards;
    }

    private List<MonsterCard> SortSearch(List<MonsterCard> ts, int num = 0)
    {
        return ts.OrderBy(x => x.GetState(num)).ToList();
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
