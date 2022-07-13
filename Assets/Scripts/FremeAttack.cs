using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FremeAttack : MonoBehaviour
{
    [SerializeField] GameObject fire;
    [SerializeField] GameObject hit;
    [SerializeField] string floorTag;
    [SerializeField] string enemyTag;
    [SerializeField] float stopTime;
    private float destoyTime;
    [SerializeField] float maxDestoyTime;

    private void Awake()
    {
        fire.SetActive(true);
    }

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

    private void OnCollisionEnter(Collision collision)
    {
       /* if(collision.gameObject.tag == floorTag || collision.gameObject.tag == enemyTag)
        {
            StartCoroutine("Hit");
        }*/

        if(collision != null)
        {
            StartCoroutine("Hit");
        }
    }

    IEnumerable Hit()
    {
        fire.SetActive(false);
        hit.SetActive(true);
        Destroy(this.gameObject);
        yield return new WaitForSeconds(stopTime);

        
    }
}
