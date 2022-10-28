using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaLottery : MonoBehaviour
{
    public GachaTable[] gachaTables = new GachaTable[3];
    public System.Random random;

    const int maxIndex = 3; // レアリティの個数（Normal、Rare、SuperRareで３つ）

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < maxIndex; i++)
        {
            gachaTables[i] = Resources.Load<GachaTable>("GachaCards/" + (i + 1).ToString());
        }
        random = new System.Random((int)DateTime.Now.Ticks);
    }

    public List<int> Lottery(int countPerGacha)
    {
        List<int> result = new List<int>();
        int totalProbability = 0;

        for (int i = 0; i < maxIndex; i++)
        {
            totalProbability += gachaTables[i].probability;
        }

        result = new List<int>();

        for (int i = 0; i < countPerGacha; i++)
        {
            int card = getCard(totalProbability);
            result.Add(card);
        }

        return result;
    }

    private int getCard(int allProbability)
    {
        int randamProbability = random.Next(0, allProbability);
        int totalProbability = 0;

        for (int i = 0; i < maxIndex; i++)
        {
            totalProbability += gachaTables[i].probability;
            if (totalProbability >= randamProbability)
            {
                int id = gachaTables[i].character[random.Next(0, gachaTables[i].character.Count)];
                return id;
            }
        }

        return 0;
    }
}
