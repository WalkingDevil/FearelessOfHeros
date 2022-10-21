using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    [SerializeField] ParticleSystem myParticleSystem;
    [SerializeField] GameObject parent;
    [SerializeField] GameObject nextParticle;

    private void OnParticleSystemStopped()
    {
        if(nextParticle != null)
        {
            nextParticle.SetActive(true);
        }
        else
        {
            Destroy(parent);
        }
    }
}
