using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{
    [SerializeField] GameDirector gameDirector;
    [SerializeField] List<GameObject> geneObject;//生成するオブジェクト
    [SerializeField] List<Vector3> genePos;//生成する位置

    [SerializeField] float _coolTime;
    public float coolTime
    {
        get { return _coolTime; }
        set 
        { 
            _coolTime = value; 

            if(_coolTime <= 0)
            {
                _coolTime = maxCoolTime;
                GenerateChara(GetRandom(geneObject.Count));
            }
        }
    }

    [SerializeField] float maxCoolTime;
    void Update()
    {
        if (maxCoolTime != 0)
        {
            if (coolTime > 0)
            {
                coolTime -= Time.deltaTime;
            }
        }
    }

    public void GenerateChara(int obNum)
    {
        int genNum = 0;
        if(maxCoolTime != 0)
        {
            genNum = gameDirector.towerCount;
        }
        Instantiate(geneObject[obNum], genePos[genNum], Quaternion.identity);
    }

    private int GetRandom(int length)
    {
        return Random.Range(0, length);
    }
}
