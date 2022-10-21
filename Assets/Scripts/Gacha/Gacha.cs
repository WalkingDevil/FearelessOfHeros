using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gacha : MonoBehaviour
{
    [SerializeField] GachaLottery gachaTest;
    public List<int> character = new List<int>();
    static public int gachaCount = 10;
    // Start is called before the first frame update
    void Awake()
    {
        GachaStart(gachaCount);
    }
    public void GachaStart(int countPerGacha)
    {
        Debug.Log(countPerGacha);
        character = gachaTest.Lottery(countPerGacha);
        for (int i = 0; i < character.Count; i++)
        {
            Debug.Log(character[i]);
        }
    }
}
