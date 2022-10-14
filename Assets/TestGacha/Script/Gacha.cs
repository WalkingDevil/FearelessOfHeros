using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gacha : MonoBehaviour
{
    [SerializeField] GachaTest gachaTest;
    public List<string> character = new List<string>();
    // Start is called before the first frame update
    public void GachaStart()
    {
        character = gachaTest.Lottery();
        for (int i = 0; i < character.Count; i++)
        {
            Debug.Log(character[i]);
        }
    }
}
