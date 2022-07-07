using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] AnimeController anime;
  
    void Start()
    {
        anime.TransitionAnime("run");
    }

    // Update is called once per frame
    void Update()
    {
       // anime.TransitionAnime("attack");
    }
}
