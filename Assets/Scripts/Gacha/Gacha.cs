using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class Gacha : MonoBehaviour
{
    [SerializeField] GachaLottery gachaTest;
    [SerializeField] GameObject cardPanel;
    public List<int> character = new List<int>();  // ガチャの抽選結果
    [SerializeField] private List<MonsterCard> monsterCards = new List<MonsterCard>();  // モンスターの種類
    public List<GameObject> objects = new List<GameObject>();  // 抽選されたモンスター
    public List<MonsterCard> cards = new List<MonsterCard>();  // 抽選されたモンスターカード
    static public int gachaCount = 10;  //ガチャの回数
    // Start is called before the first frame update
    void Start()
    {
        GachaStart(gachaCount);
    }

    public void GachaStart(int countPerGacha)  //ガチャの抽選
    {
        Debug.Log(countPerGacha);
        character = gachaTest.Lottery(countPerGacha);
        for (int i = 0; i < character.Count; i++)
        {
            for (int j = 0; j < monsterCards.Count; j++)
            {
                if (character[i] == monsterCards[j].GetState(0))  // 抽選結果をモンスターとモンスターカードのリストに入れる
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
        foreach (GameObject obj in objects)  // モンスターを表示
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

        foreach (MonsterCard obj in cards)  // モンスターカードを表示
        {
            MonsterCard card = Instantiate(obj) as MonsterCard;
            card.transform.parent = cardPanel.transform;
            yield return new WaitForSeconds(1f);
        }
    }
}
