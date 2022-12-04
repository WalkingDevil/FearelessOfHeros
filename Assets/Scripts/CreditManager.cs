using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CreditManager : MonoBehaviour
{
    private AudioController seCo;
    [SerializeField] AudioSource source;
    [SerializeField] List<AudioClip> se;
    [SerializeField] Text urlText;
    [SerializeField] RectTransform confirmation;
    [SerializeField] float speed = 1f;
    private string cutUrl;

    private void Start()
    {
        seCo = new AudioController(source);
    }

    /// <summary>
    /// 各URLのボタン
    /// </summary>
    /// <param name="url">URLが書かれたText</param>
    public void OnCreditButton(Text url)
    {
        seCo.ChengeClip(se[2]);
        seCo.ChengePlayAudio(true);
        //"（"のインデックス番号をテキストから取得
        int fast = url.text.IndexOf("（");
        //"）"のインデックス番号をテキストから取得
        int end = url.text.IndexOf("）");
        //かっこの中身を切り取る
        cutUrl = url.text.Substring(fast + 1, end - fast - 1);

        urlText.text = cutUrl;
        confirmation.DOScale(1, speed);
    }

    /// <summary>
    /// ボタンを押したときにWebページを開く
    /// </summary>
    /// <param name="yes"></param>
    public void TransposeDisplay(bool yes)
    {
        if(yes)
        {
            seCo.ChengeClip(se[0]);
            seCo.ChengePlayAudio(true);
            Application.OpenURL(cutUrl);//Webページを開く
        }
        else
        {
            seCo.ChengeClip(se[1]);
            confirmation.DOScale(0, speed).OnStart(() => seCo.ChengePlayAudio(true));
        }
    }
}
