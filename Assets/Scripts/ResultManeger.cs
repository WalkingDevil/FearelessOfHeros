using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResultManeger : MonoBehaviour
{
    [SerializeField] AnimeController anime;
    [SerializeField] string clearString;
    [SerializeField] string overString;
    [SerializeField] List<string> animeParameters;
    [SerializeField] List<Text> chengeTexts;

    /// <summary>
    /// リザルト画面に表示する文字とアニメーション
    /// </summary>
    /// <param name="over">ゲームオーバーしたか</param>
    public void ChengeText(bool over = true)
    {
        string display = "";
        string animeName = "";
        if(over)
        {
            display = overString;
            animeName = animeParameters[0];

        }
        else
        {
            display = clearString;
            animeName = animeParameters[1];
        } 

        for(int c = 0;c < chengeTexts.Count; c++)
        {
            chengeTexts[c].text = display[c].ToString();
        }

        anime.TransitionAnime(animeName);
    }
}
