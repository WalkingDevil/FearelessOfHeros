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
    [SerializeField] RectTransform selectPanel;
    [SerializeField] List<Text> displayTexts;//��ʕ\���p�e�L�X�g
    [SerializeField] Slider expSlider;//��ʕ\���p��EXP�X���C�_�[
    [SerializeField] int deckCount = 4;
    [SerializeField] int gachaKrystaal = 5;
    [SerializeField] int loginBouns = 1000;
    [SerializeField] float scaleSpeed = 0.5f;


    static public int level = 1;
    private int _krystaal = 0;
    public int krystaal
    {
        get { return _krystaal; }
        set
        {
            _krystaal = value;
            if(savePath.fast)
            {
                _krystaal += loginBouns;
                savePath.fast = false;
                saveData.Save(savePath);
                displayTexts[3].text = _krystaal.ToString();
            }

        }     
    }
    private const int one = 1;

    void Start()
    {
        //�ۑ��f�[�^����
        savePath = saveData.Load();
        level = savePath.level;
        krystaal = savePath.krystaal;
        fadeImage.Range = 1;
        fadeImage.UpdateMaskTexture(fadeTexture);

        SetDisplay();
    }

    private void Update()
    {
        if(banner.texture != null)
        {
            rod.SetActive(false);
            //fade.FadeOut(0.01f);
        }
    }

    /// <summary>
    /// �f�b�L��Ґ����Q�[�����J�n����
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
    /// �\������
    /// </summary>
    /// <param name="rect"></param>
    public void OnDisplay(RectTransform rect)
    {
        rect.gameObject.SetActive(true);
        rect.DOScaleY(one, scaleSpeed);
    }

    /// <summary>
    /// ��\���ɂ���
    /// </summary>
    /// <param name="rect"></param>
    public void OffDisplay(RectTransform rect)
    {
        rect.DOScaleY(0, scaleSpeed);
    }

    /// <summary>
    /// �K�`���̘A����ݒ肵�K�`��������
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

    private void SetDisplay()//��ʕ\������
    {
        displayTexts[0].text = "Lv." + level;
        expSlider.maxValue = savePath.maxExp;
        expSlider.value = savePath.exp;
        displayTexts[1].text = savePath.exp + "/" + savePath.maxExp;
        displayTexts[2].text = "�R�X�g�F" + savePath.cost;
        displayTexts[3].text = krystaal.ToString();
    }

}
