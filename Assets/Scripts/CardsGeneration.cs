using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsGeneration : MonoBehaviour
{
    private SaveData saveData = new SaveData();
    private SavePath savePath = new SavePath();
    [SerializeField] List<int> myCardsId = new List<int>();
    [SerializeField] List<MonsterCard> cardsList;
    [SerializeField] GameDirector gameDirector;
    [SerializeField] CharacterGenerator allyGene;
    private List<MonsterCard> defMonsterCards = new List<MonsterCard>();

    private void Awake()
    {
        savePath = saveData.Load();
        myCardsId = savePath.myDeckData;
        foreach (var card in cardsList)
        {
            if (myCardsId.Contains(card.GetState(0)))//ID‚ªŠÜ‚Ü‚ê‚Ä‚¢‚é‚©‚Ç‚¤‚©
            {
                defMonsterCards.Add(card);
            }
        }

        Generation();
    }



    private void Generation()
    {
        for(int c = 0; c < defMonsterCards.Count; c++)
        {
            GameObject ob = Instantiate(defMonsterCards[c].gameObject, this.gameObject.transform);      
            allyGene.GetGenerationObject(defMonsterCards[c].GetObject());
            MonsterCard monsterCard = ob.GetComponent<MonsterCard>();
            gameDirector.SetMonsterCards(monsterCard);
            monsterCard.SetNumber(c);
        }
    }
}
