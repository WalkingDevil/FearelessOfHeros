using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeController : MonoBehaviour
{
    [SerializeField] Animator animator;

    /// <summary>
    /// �A�j���[�V�����̕ψ�
    /// </summary>
    /// <param name="parame">�g���K�[�̖��O</param>
    public void TransitionAnime(string parame)
    {
        animator.SetTrigger(parame);
    }
}
