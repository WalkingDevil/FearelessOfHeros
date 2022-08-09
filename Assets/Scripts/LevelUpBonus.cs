using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpBonus : MonoBehaviour
{
    private SaveData saveData;
    private SavePath savePath;
    private int startLevel = 0;
    [SerializeField] RectTransform content;
    [SerializeField] Text resultPrefab;
    public LevelUpBonus(int startLevel = 0)
    {
        this.startLevel = startLevel;
    }

    private void Start()
    {
        savePath = saveData.Load();
    }

    public void LevelBonus(int endLevel = 0)
    {
        int l = startLevel + 1;
        for(;l <= endLevel;l++)
        {
            SetBonus(l);
        }

    }

    private void SetBonus(int num)
    {
        switch(num)
        {
            case 5:
                List<int> list = new List<int>(){ 5, 6, 7, 8 };
                savePath.idData.AddRange(list);
                GenerationText("54");
                break;
            default:
                break;
        }
    }

    private void GenerationText(string r)
    {
        Text text = Instantiate(resultPrefab, content.transform);
        text.text = r;
    }
}
