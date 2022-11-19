using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using DG.Tweening;

public class StartDirector : MonoBehaviour
{
    private SaveData saveData = new SaveData();
    private SavePath savePath = new SavePath();
    [SerializeField] Fade fade;
    [SerializeField] FadeImage fadeImage;
    [SerializeField] Texture2D fadeTexture;
    [SerializeField] RawImage banner;
    [SerializeField] GameObject rod;
    [SerializeField] int deckCount = 4;
    [SerializeField] float scaleSpeed = 0.5f;
    [SerializeField] RectTransform selectPanel;
    static public int level = 1;
    private const int one = 1;

    void Start()
    {
        savePath = saveData.Load();
        level = savePath.level;
        fadeImage.Range = 1;
        fadeImage.UpdateMaskTexture(fadeTexture);
    }

    private void Update()
    {
        if(banner.texture != null)
        {
            //rod.SetActive(false);
            fade.FadeOut(0.01f);
        }
    }

    /// <summary>
    /// デッキを編成しゲームを開始する
    /// </summary>
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

    /// <summary>
    /// 表示する
    /// </summary>
    /// <param name="rect"></param>
    public void OnDisplay(RectTransform rect)
    {
        rect.gameObject.SetActive(true);
        rect.DOScaleY(one, scaleSpeed);
    }

    /// <summary>
    /// 非表示にする
    /// </summary>
    /// <param name="rect"></param>
    public void OffDisplay(RectTransform rect)
    {
        rect.DOScaleY(0, scaleSpeed);
    }

    /// <summary>
    /// ガチャの連数を設定しガチャを引く
    /// </summary>
    /// <param name="count"></param>
    public void OnGacha(int count)
    {
        Gacha.gachaCount = count;
        SceneManager.LoadScene("GachaScene");
    }
}
