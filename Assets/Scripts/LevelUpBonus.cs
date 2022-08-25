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
    [SerializeField] RectTransform content;
    [SerializeField] RectTransform resultPrefab;
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

    private void SetBonus(int num)
    {
        string massege = null;
        List<int> list;
        switch (num)
        {
            case 3:
                list = new List<int>() { 5, 6};
                idDatas.AddRange(list);
                massege = "新たに2キャラ追加されました。";
                GenerationText(massege);
                break;
            case 5:
                list = new List<int>(){ 7, 8};
                idDatas.AddRange(list);
                massege = "新たに2キャラ追加されました。";
                GenerationText(massege);
                cost++;
                massege = "コストが１つ増加しました。";
                GenerationText(massege);
                break;
            default:
                Debug.Log(num);
                break;
        }
    }

    private void GenerationText(string r)
    {
        GameObject ob = Instantiate(resultPrefab.gameObject, content);
        ob.GetComponent<Text>().text = r;
    }

    public List<int> SetIdData()
    {
        return idDatas;
    }

    public int SetCost()
    {
        return cost;
    }
}
