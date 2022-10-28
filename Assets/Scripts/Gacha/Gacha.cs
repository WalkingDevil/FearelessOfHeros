using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class Gacha : MonoBehaviour
{
    [SerializeField] GachaLottery gachaTest;
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

        GachaInstance();

    }

    void GachaInstance()
    {
        foreach(GameObject obj in objects)
        {
            GameObject game = Instantiate(obj);
            game.GetComponent<CharacterController>().enabled = false;
            game.GetComponent<NavMeshAgent>().enabled = false;
            game.GetComponent<BoxCollider>().enabled = false;
            game.GetComponentInChildren<Canvas>().gameObject.SetActive(false);
            Destroy(game.GetComponent<Rigidbody>());
            
        }
    }

    IEnumerator Performance()
    {

        yield return null;
    }
}
