using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{
    [SerializeField] List<GameObject> geneObject;//生成するオブジェクト
    [SerializeField] List<Vector3> genePos;//生成する位置

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateChara(int num)
    {
        Instantiate(geneObject[num], genePos[num], Quaternion.identity);
    }
}
