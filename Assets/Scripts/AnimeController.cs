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

    public void DieIvent()
    {
        Destroy(monster.gameObject);
    }

    public void OnParticle()
    {
        paticle.SetActive(true);
    }

    public void OffParticle()
    {
        paticle.SetActive(false);
    }
}
