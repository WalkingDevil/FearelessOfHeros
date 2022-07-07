using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeController : MonoBehaviour
{
    [SerializeField] Animator animator;

    /// <summary>
    /// アニメーションの変移
    /// </summary>
    /// <param name="parame">トリガーの名前</param>
    public void TransitionAnime(string parame)
    {
        animator.SetTrigger(parame);
    }
}
