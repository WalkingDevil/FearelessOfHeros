using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class Gacha : MonoBehaviour
{
    private SaveData saveData = new SaveData();
    private SavePath savePath = new SavePath();
    private List<int> idDatas;
    [SerializeField] GachaLottery gachaLottery;
    [SerializeField] GameObject cardPanel;
    [SerializeField] GameObject effectObj;  // モンスター登場時のエフェクト
    [SerializeField] GameObject skipBotton;  // スキップボタン
    [SerializeField] GameObject titleBotton;  // タイトルに戻るボタン
    [SerializeField] float effectTime = 1f;  // エフェクトを表示する時間
    [SerializeField] int monsterDisplayTime = 3;  // モンスターの表示時間
    [SerializeField] float cardDisplayIntervalTime = 0.5f;  // カードを表示する間隔の時間
    static public int gachaCount = 10;  //ガチャの回数
    public List<int> character = new List<int>();  // ガチャの抽選結果
    [SerializeField] private List<MonsterCard> monsterCards = new List<MonsterCard>();  // モンスターの種類
    public List<GameObject> objects = new List<GameObject>();  // 抽選されたモンスター
    public List<MonsterCard> cards = new List<MonsterCard>();  // 抽選されたモンスターカード
    private bool skip = false;  // スキップするかどうか
    private bool skippable = false;  // スキップ可能か
    // Start is called before the first frame update
    void Start()
    {
        saveData = new SaveData();
        savePath = saveData.Load();
        idDatas = savePath.idData;
        GachaStart(gachaCount);
        skipBotton.SetActive(true);
        titleBotton.SetActive(false);
        cardPanel.SetActive(false);
    }

    public void SkipButton()  // スキップボタン
    {
        if (skippable)
        {
            if (!skip)
            {
                skip = true;
            }
        }
    }

    public void BackTitle()  // タイトルシーンに戻るボタン
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void GachaStart(int countPerGacha)  //ガチャの抽選
    {
        Debug.Log(countPerGacha);
        character = gachaLottery.Lottery(countPerGacha);
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

        //List<int> list = character;
        //List<int> list2 = character.Union(idDatas).ToList();
        idDatas.AddRange(idDatas.Union(character).ToList());
        var newData = idDatas.Distinct().ToList();// 重複する要素を削除
        savePath.idData = newData;
        saveData.Save(savePath);

        StartCoroutine("Performance");

    }

    IEnumerator Performance()  // ガチャ演出
    {
        foreach (GameObject obj in objects)  // モンスターを表示
        {
            yield return StartCoroutine("Effect");
            skippable = true;
            GameObject game = Instantiate(obj, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
            game.GetComponent<UserInterface>().ChengeGach();
            game.GetComponent<CharacterController>().enabled = false;
            game.GetComponent<NavMeshAgent>().enabled = false;
            game.GetComponent<BoxCollider>().enabled = false;
            Destroy(game.GetComponent<Rigidbody>());
            yield return StartCoroutine(PerformanceSkip(3));
            skippable = false;
            if (skip)
            {
                skip = false;
            }
            Destroy(game);
        }

        yield return new WaitForSeconds(0.5f);
        skipBotton.SetActive(false);
        cardPanel.SetActive(true);
        yield return new WaitForSeconds(1f);

        foreach (MonsterCard obj in cards)  // モンスターカードを表示
        {
            MonsterCard card = Instantiate(obj) as MonsterCard;
            card.transform.parent = cardPanel.transform;
            yield return new WaitForSeconds(cardDisplayIntervalTime);
        }

        titleBotton.SetActive(true);
    }

    IEnumerator Effect()  // モンスター登場時のエフェクト
    {
        GameObject effect = Instantiate(effectObj, new Vector3(0,5,0), Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(effectTime);
        Destroy(effect);
    }

    /// <summary>
    /// スキップ
    /// </summary>
    /// <param name="count">待ち時間（秒）</param>
    /// <returns></returns>
    IEnumerator PerformanceSkip(int count)
    {
        for(int i = 0; i < count; i++)
        {
            if (skip && skippable)
            {
                skip = false;
                break;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
