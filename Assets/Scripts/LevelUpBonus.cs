using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpBonus : MonoBehaviour
{
    private SaveData saveData = new SaveData();
    private SavePath savePath = new SavePath();
    private List<int> idDatas;
    private int cost;
    private int startLevel = 0;
    private int krystal;
    [SerializeField] int kystaalRate = 10;
    [SerializeField] int clearKrystaal = 10;
    [SerializeField] RectTransform content;
    [SerializeField] RectTransform resultPrefab;
    const int zero = 0;
    //開始時のレベルを入れる
    public void SetStartLevel(int startLevel = 0)
    {
        this.startLevel = startLevel;
    }

    private void Start()
    {
        saveData = new SaveData();
        savePath = saveData.Load();
        idDatas = savePath.idData;
        cost = savePath.cost;
    }

    /// <summary>
    /// レベルアップ時の表示
    /// </summary>
    /// <param name="endLevel"></param>
    public void LevelBonus(int endLevel = 0)
    {
        if (startLevel != endLevel)
        {
            int l = startLevel + 1;
            for (; l <= endLevel; l++)
            {
                SetBonus(l);
            }
            string massege = startLevel + " → " + endLevel + "にレベルアップした";
            GenerationText(massege);
        }
    }

    /// <summary>
    /// クリア時のボーナス
    /// </summary>
    /// <param name="enemyCount">敵を倒した回数</param>
    /// <param name="allyCount">味方が倒れた回数</param>
    public void ClearBonus(int enemyCount, int allyCount)
    {
        GenerationText("敵を" + enemyCount + "体倒しました。");
        GenerationText("味方が" + enemyCount + "体倒れました。");

        //敵を倒した数 - 倒された数を0以上の数で計算
        var diff = Mathf.Clamp(enemyCount - allyCount, zero, Mathf.Infinity);
        //差を割り、小数点以下切り上げで計算
        krystal =  (int)Mathf.Ceil(diff / kystaalRate) + clearKrystaal;

        GenerationText("クリスタルを" + krystal + "個手に入れた。");

    }

    /// <summary>
    /// レベルに合わせたボーナス
    /// </summary>
    /// <param name="num">レベル</param>
    private void SetBonus(int num)
    {
        List<int> list;
        switch (num)
        {
            case 3:
                list = new List<int>() { 5, 6};
                idDatas.AddRange(list);
                GenerationText("新たに2キャラ追加されました。");
                break;
            case 5:
                list = new List<int>(){ 7, 8};
                idDatas.AddRange(list);
                GenerationText("新たに2キャラ追加されました。");
                cost++;
                GenerationText("コストが１つ増加しました。");
                break;
            default:
                Debug.Log(num);
                break;
        }
    }

    /// <summary>
    /// メッセージを追加する
    /// </summary>
    /// <param name="r">メッセージ</param>
    private void GenerationText(string r)
    {
        GameObject ob = Instantiate(resultPrefab.gameObject, content);
        ob.GetComponent<Text>().text = r;
    }

    /// <summary>
    /// 新しく保存するモンスターデータ
    /// </summary>
    /// <returns></returns>
    public List<int> SetIdData()
    {
        return idDatas;
    }

    /// <summary>
    /// 新しく保存するコスト
    /// </summary>
    /// <returns></returns>
    public int SetCost()
    {
        return cost;
    }

    public int SetKrystaal()
    {
        return krystal;
    }
}
