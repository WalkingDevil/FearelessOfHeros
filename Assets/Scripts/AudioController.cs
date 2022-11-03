using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource source;

    /// <summary>
    /// Audio‚Ì‰Šúİ’è
    /// </summary>
    /// <param name="source">AudioSource‚Ìİ’è</param>
    /// <param name="clip">AudioClip‚Ì‰Šúİ’è</param>
    public AudioController(AudioSource source ,AudioClip clip = null)
    {
        this.source = source;
        this.source.clip = clip;

    }

    /// <summary>
    /// Audio‚ÌÄ¶
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

    public void SettingVolume(float volume)
    {
        source.volume = volume;
    }

    public void ChengeClip(AudioClip clip)
    {
        source.clip = clip;
    }
}
