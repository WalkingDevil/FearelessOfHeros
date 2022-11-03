using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource source;

    /// <summary>
    /// Audio�̏����ݒ�
    /// </summary>
    /// <param name="source">AudioSource�̐ݒ�</param>
    /// <param name="clip">AudioClip�̏����ݒ�</param>
    public AudioController(AudioSource source ,AudioClip clip = null)
    {
        this.source = source;
        this.source.clip = clip;

    }

    /// <summary>
    /// Audio�̍Đ�
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
