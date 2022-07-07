using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDelete : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
             Destroy(other.gameObject);
        }
        
    }
}
