using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource source;

    /// <summary>
    /// Audioの初期設定
    /// </summary>
    /// <param name="source">AudioSourceの設定</param>
    /// <param name="clip">AudioClipの初期設定</param>
    public AudioController(AudioSource source ,AudioClip clip = null)
    {
        this.source = source;
        this.source.clip = clip;

    }

    /// <summary>
    /// Audioの再生
    /// </summary>
    public void ChengePlayAudio(bool on)
    {
        if(on)
        {
            source.Play();
        }
        else
        {
            source.Stop();
        }
    }

    /// <summary>
    /// ボリュームを変える
    /// </summary>
    /// <param name="volume">ボリュームの値</param>
    public void SettingVolume(float volume)
    {
        source.volume = volume;
    }

    /// <summary>
    /// 音を変える
    /// </summary>
    /// <param name="clip">変更するClip</param>
    public void ChengeClip(AudioClip clip)
    {
        source.clip = clip;
    }
}
