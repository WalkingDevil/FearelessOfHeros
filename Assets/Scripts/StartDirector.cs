using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using DG.Tweening;


public class StartDirector : MonoBehaviour
{
    private SaveData saveData = new SaveData();
    private SavePath savePath = new SavePath();

    private AudioController audio;
    [SerializeField] AudioSource source;
    [SerializeField] List<AudioClip> bgm;

    //開始時の演出
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] string videoPlayerUrl;
    [SerializeField] RectTransform opPanel;
    [SerializeField] RectTransform rogo;
    [SerializeField] RawImage banner;
    [SerializeField] RawImage organization;
    [SerializeField] GameObject rod;
    [SerializeField] Button skipButton;
    [SerializeField] GameObject organizationScroll;

    [SerializeField] RectTransform selectPanel;
    [SerializeField] List<Text> displayTexts;//画面表示用テキスト
    [SerializeField] Slider expSlider;//画面表示用のEXPスライダー
    [SerializeField] int deckCount = 4;
    [SerializeField] int gachaKrystaal = 5;
    [SerializeField] int loginBouns = 1000;
    [SerializeField] float scaleSpeed = 0.5f;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float fastStopSpeed = 10f;

    static public int level = 1;

    private int krystaal;

    private const int one = 1;

    void Start()
    {
        //保存データを代入
        savePath = saveData.Load();
        level = savePath.level;
        krystaal = savePath.krystaal;

        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, videoPlayerUrl); 
        videoPlayer.prepareCompleted += PrepareCompleted;
        videoPlayer.Prepare();

        audio = new AudioController(source, bgm[0]);
        audio.ChengePlayAudio(true);
        
        SetDisplay();

        StartCoroutine(StartSubstitute());
    }

    void PrepareCompleted(VideoPlayer vp)
    {
        vp.prepareCompleted -= PrepareCompleted;
        vp.Play();
    }

    /// <summary>
    /// スタート時のコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator StartSubstitute()
    {
        yield return new WaitUntil(() => banner.texture != null && organization.texture != null);

        skipButton.gameObject.SetActive(true);
        organizationScroll.SetActive(false);

        yield return new WaitUntil(() => videoPlayer.isPlaying != true);

        rogo.DOAnchorPosY(0, moveSpeed);

        yield return new WaitForSeconds(fastStopSpeed);

        FeidOpeningPanel();

    }
    /// <summary>
    /// フェード時に行う処理
    /// </summary>
    private void FeidOpeningPanel()
    {
        opPanel.DOAnchorPosX(-Screen.width, moveSpeed).OnComplete(() => rod.SetActive(false));
        audio.ChengePlayAudio(false);
        audio.ChengeClip(bgm[1]);
        audio.ChengePlayAudio(true);
    }
    /// <summary>
    /// スキップボタン
    /// </summary>
    public void OnSkip()
    {
        StopCoroutine(StartSubstitute());
        FeidOpeningPanel();
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
        if(krystaal > count * gachaKrystaal)
        {
            krystaal -= count * gachaKrystaal;
            savePath.krystaal = krystaal;
            saveData.Save(savePath);
            Gacha.gachaCount = count;
            SceneManager.LoadScene("GachaScene");
        }
    }

    public void ExplanationDisplay(GameObject ob)
    {
        ob.SetActive(!ob.activeInHierarchy);
    }
    private void SetDisplay()//画面表示する
    {
        displayTexts[0].text = "Lv." + level;
        expSlider.maxValue = savePath.maxExp;
        expSlider.value = savePath.exp;
        displayTexts[1].text = savePath.exp + "/" + savePath.maxExp;
        displayTexts[2].text = "コスト：" + savePath.cost;
        displayTexts[3].text = savePath.krystaal.ToString();
    }

}
