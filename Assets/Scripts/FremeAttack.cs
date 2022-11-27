using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FremeAttack : MonoBehaviour
{
    private float destoyTime;
    [SerializeField] float maxDestoyTime;

    private void Update()
    {
        if(destoyTime < maxDestoyTime)
        {
            destoyTime += Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void DestroyBall()
    {
        Destroy(this.gameObject);
    }

}
