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
                GenerateChara(GetRandom(0, geneObject.Count));
            }
        }
    }

    [SerializeField] float maxCoolTime;//最大値のクールタイム
    [SerializeField] int maxGenePosX;//最大値の生成位置のX座標
    [SerializeField] int minGenePosX;//最小値の生成位置のX座標
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

        if(maxCoolTime != 0)//敵である場合
        {
            genNum = gameDirector.towerCount;
        }

        //生成位置をX座標ランダムで決める
        Vector3 gene = new Vector3(GetRandom(minGenePosX, maxGenePosX), genePos[genNum].y, genePos[genNum].z);

        Instantiate(geneObject[obNum], gene, Quaternion.identity);
    }

    private int GetRandom(int min, int max)
    {
        return Random.Range(min, max);
    }
}
