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
    [SerializeField] Fade fade;
    [SerializeField] FadeImage fadeImage;
    [SerializeField] Texture2D fadeTexture;
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
        skipBotton.SetActive(false);
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

    /// <summary>
    /// ガチャの抽選を開始する
    /// </summary>
    /// <param name="countPerGacha">ガチャを回す回数</param>
    public void GachaStart(int countPerGacha)
    {
        character = gachaLottery.Lottery(countPerGacha);
        for (int i = 0; i < character.Count; i++)
        {
            for (int j = 0; j < monsterCards.Count; j++)
            {
                if (character[i] == monsterCards[j].GetState(0))  // 抽選結果をモンスターとモンスターカードのリストに入れる
                {
                    Debug.Log(i);
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
            skipBotton.SetActive(false);
            //yield return StartCoroutine("Effect");
            float attribute = obj.GetComponent<CharacterController>().SetMyUserInterface().GetState(7);
            fadeImage.color = GetColor(attribute);
            fadeImage.UpdateMaskTexture(fadeTexture);
            fade.FadeIn(1.5f, () => print("フェードイン完了"));
            yield return new WaitForSeconds(1.5f);
            fade.FadeOut(1.5f, () => print("フェードアウト完了"));
            skippable = true;
            GameObject game = Instantiate(obj, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
            game.GetComponent<UserInterface>().ChengeGach();
            game.GetComponent<CharacterController>().enabled = false;
            game.GetComponent<NavMeshAgent>().enabled = false;
            game.GetComponent<BoxCollider>().enabled = false;
            Destroy(game.GetComponent<Rigidbody>());
            skipBotton.SetActive(true);
            yield return StartCoroutine(PerformanceSkip(3));
            skippable = false;
            if (skip)
            {
                skip = false;
                Destroy(game);
                break;
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
                break;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private Color GetColor(float num)  // モンスターの属性によってフェードの色を変える
    {
        switch(num)
        {
            case 0:
                return Color.red;
            case 1:
                return Color.blue;
            case 2:
                return Color.green;
            case 3:
                return Color.magenta;
            default:
                return Color.white;
        }
    }
}
