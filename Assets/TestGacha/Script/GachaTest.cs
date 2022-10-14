using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaTest : MonoBehaviour
{
    public GachaTable[] gachaTables = new GachaTable[3];
    public System.Random random;

    const int maxIndex = 3; // ���A���e�B�̌��iNormal�ARare�ASuperRare�łR�j
    const int countPerGacha = 10; // 1��Ɉ������

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < maxIndex; i++)
        {
            gachaTables[i] = Resources.Load<GachaTable>("GachaCards/" + (i + 1).ToString());
        }
        random = new System.Random((int)DateTime.Now.Ticks);
    }

    public List<string> Lottery()
    {
        List<string> result = new List<string>();
        int totalProbability = 0;

        for (int i = 0; i < maxIndex; i++)
        {
            totalProbability += gachaTables[i].probability;
        }

        result = new List<string>();

        for (int i = 0; i < countPerGacha; i++)
        {
            string card = getCard(totalProbability);
            result.Add(card);
        }

        return result;
    }

    private string getCard(int allProbability)
    {
        int randamProbability = random.Next(0, allProbability);
        int totalProbability = 0;

        for (int i = 0; i < maxIndex; i++)
        {
            totalProbability += gachaTables[i].probability;
            if (totalProbability >= randamProbability)
            {
                string id = gachaTables[i].character[random.Next(0, gachaTables[i].character.Count)];
                return id;
            }
        }

        return null;
    }
}
