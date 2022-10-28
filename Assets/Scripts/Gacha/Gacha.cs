using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class Gacha : MonoBehaviour
{
    [SerializeField] GachaLottery gachaTest;
    [SerializeField] GameObject cardPanel;
    public List<int> character = new List<int>();
    [SerializeField] private List<MonsterCard> monsterCards = new List<MonsterCard>();
    public List<GameObject> objects = new List<GameObject>();
    public List<MonsterCard> cards = new List<MonsterCard>();
    static public int gachaCount = 10;
    // Start is called before the first frame update
    void Start()
    {
        GachaStart(gachaCount);
    }

    void Update()
    {
        
    }

    public void GachaStart(int countPerGacha)
    {
        Debug.Log(countPerGacha);
        character = gachaTest.Lottery(countPerGacha);
        for (int i = 0; i < character.Count; i++)
        {
            for (int j = 0; j < monsterCards.Count; j++)
            {
                if (character[i] == monsterCards[j].GetState(0))
                {
                    objects.Add(monsterCards[j].GetObject().gameObject);
                    cards.Add(monsterCards[j]);
                }
            }
        }

        StartCoroutine("Performance");

    }

    IEnumerator Performance()
    {
        foreach (GameObject obj in objects)
        {
            GameObject game = Instantiate(obj, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
            game.GetComponentInChildren<Canvas>().GetComponent<UserInterface>().ChengeGach();
            game.GetComponent<CharacterController>().enabled = false;
            game.GetComponent<NavMeshAgent>().enabled = false;
            game.GetComponent<BoxCollider>().enabled = false;
            Destroy(game.GetComponent<Rigidbody>());
            yield return new WaitForSeconds(3f);
            Destroy(game);
        }

        yield return new WaitForSeconds(0.5f);
        cardPanel.SetActive(true);
        yield return new WaitForSeconds(1f);

        foreach (MonsterCard obj in cards)
        {
            MonsterCard card = Instantiate(obj) as MonsterCard;
            card.transform.parent = cardPanel.transform;
            yield return new WaitForSeconds(1f);
        }
    }
}
