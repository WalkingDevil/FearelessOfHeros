using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDelete : MonoBehaviour
{
    [SerializeField] string enemyWeapon;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == enemyWeapon)
        {
            Debug.Log("Ž€‚ñ‚¾");
             Destroy(other.transform.root.gameObject);
        }  
    }
}
