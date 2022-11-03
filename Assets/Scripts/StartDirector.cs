using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class StartDirector : MonoBehaviour
{
    private SaveData saveData = new SaveData();
    private SavePath savePath = new SavePath();
    //  SavePath savePath = new SavePath();
    [SerializeField] int deckCount = 4;
    [SerializeField] RectTransform selectPanel;


    void Start()
    {
        savePath = saveData.Load();
    }
    public void Sortie()
    {
        List<int> list = new List<int>();
        for (int s = 0; s < selectPanel.childCount; s++)
        {
            list.Add(selectPanel.GetChild(s).GetComponent<MonsterCard>().GetState(0));
        }

        if(selectPanel.childCount == deckCount)
        {
            savePath.myDeckData = list;
            saveData.Save(savePath);
            SceneManager.LoadScene("GameScene");
        }
    }

    public void OnDisplay(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public void OnGacha(int count)
    {
        Gacha.gachaCount = count;
        SceneManager.LoadScene("GachaScene");
    }
}
