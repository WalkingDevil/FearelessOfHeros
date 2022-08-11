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
    private int startLevel = 0;
    [SerializeField] RectTransform content;
    [SerializeField] Text resultPrefab;
    public LevelUpBonus(int startLevel = 0)
    {
        this.startLevel = startLevel;
    }

    private void Start()
    {
        saveData = new SaveData();
        savePath = saveData.Load();
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
        }


    }

    private void SetBonus(int num)
    {
        switch(num)
        {
            case 5:
                List<int> list = new List<int>(){ 5, 6, 7, 8 };
                savePath.idData.AddRange(list);
                GenerationText("êVÇΩÇ…4ÉLÉÉÉâí«â¡Ç≥ÇÍÇ‹ÇµÇΩÅB");
                saveData.Save(savePath);
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
