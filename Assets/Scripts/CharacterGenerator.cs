using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{
    [SerializeField] List<GameObject> geneObject;//生成するオブジェクト
    [SerializeField] List<Vector3> genePos;//生成する位置

    [SerializeField] float coolTime;
    [SerializeField] float maxCoolTime;
    void Update()
    {
        if (maxCoolTime != 0)
        {
            if (coolTime < 0)
            {
                coolTime -= Time.deltaTime;
            }
            else
            {
                GenerateChara(GetRandom(genePos.Count));
            }
        }
    }

    public void GenerateChara(int num)
    {
        Instantiate(geneObject[num], genePos[0], Quaternion.identity);
    }

    private int GetRandom(int length)
    {
        return Random.Range(0, length);
    }
}
