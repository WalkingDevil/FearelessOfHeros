using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeController : MonoBehaviour
{
    [SerializeField] GameObject monster;
    [SerializeField] Animator animator;
    [SerializeField] GameObject paticle;

    /// <summary>
    /// アニメーションの変移
    /// </summary>
    /// <param name="parame">トリガーの名前</param>
    public void TransitionAnime(string parame)
    {
        animator.SetTrigger(parame);
    }

　　/// <summary>
　　/// アニメーション側からアニメーション終了後にキャラクターをDestroyする
　　/// </summary>
    public void DieIvent()
    {
        Destroy(monster.gameObject);
    }

    /// <summary>
    /// パーティクルをONにする
    /// </summary>
    public void OnParticle()
    {
        paticle.SetActive(true);
    }

    /// <summary>
    /// パーティクルをOFFにする
    /// </summary>
    public void OffParticle()
    {
        paticle.SetActive(false);
    }
}
