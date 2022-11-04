using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CharacterGenerator : MonoBehaviour
{
    [SerializeField] GameDirector gameDirector;
    [SerializeField] PlayerController player;
    [SerializeField] List<CharacterController> geneObject;//生成するオブジェクト
    [SerializeField] List<CharacterController> bsObject;//ボスの生成するオブジェクト
    [SerializeField] List<Vector3> genePos;//生成する位置

    private AudioController audioController;
    [SerializeField] AudioSource seSource;
    [SerializeField] AudioClip seClip;
    [SerializeField] bool enemy = false;
    //生成確率
    [SerializeField] List<int> geneListsPlanLast = new List<int>(7);
    [SerializeField] List<int> geneListsPlan1 = new List<int>(7);
    [SerializeField] List<int> geneListsPlan2 = new List<int>(7);
    [SerializeField] List<int> geneListsPlan3 = new List<int>(7);
    [SerializeField] List<int> maxValue;


    [SerializeField] float _coolTime;
    public float coolTime
    {
        get { return _coolTime; }
        set
        {
            _coolTime = value;

            if (_coolTime <= 0)
            {
                _coolTime = maxCoolTimes[gameDirector.towerCount];
                GenerateChara(GetRandom(0, geneObject.Count));
            }
        }
    }

    [SerializeField] List<float> maxCoolTimes;//最大値のクールタイム
    [SerializeField] int maxGenePosX;//最大値の生成位置のX座標
    [SerializeField] int minGenePosX;//最小値の生成位置のX座標

    private void Start()
    {
        audioController = new AudioController(seSource, seClip);
    }

    void Update()
    {
        if (enemy)
        {
            if (coolTime > 0)
            {
                coolTime -= Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// 生成するモンスターの一覧を取得
    /// </summary>
    /// <param name="monsters"></param>
    public void GetGenerationObject(CharacterController monsters)
    {
        geneObject.Add(monsters);
    }

    public void GenerateChara(int obNum)
    {
        int genNum = 0;
        Vector3 gene = Vector3.zero;
        CharacterController ob = geneObject[obNum];
        if (enemy)//敵である場合
        {
            genNum = gameDirector.towerCount;
            ob = GenereationObject(genNum);
        }
        else
        {
            UserInterface user = ob.SetMyUserInterface();//UserInterfaceスクリプトを受け取る
            if (gameDirector.cost < user.GetState(3))//所持しているコストと必要コストが足りているか
            {
                Debug.Log(98);
                return;
            }
            else
            {
                Debug.Log(98);
                gameDirector.cost -= user.GetState(3);
            }
        }

        //生成位置をX座標ランダムで決める
        if(!enemy && gameDirector.selfOperation)
        {
            Vector3 playerPos = player.gameObject.transform.position;
            gene = new Vector3(GetRandom(minGenePosX, maxGenePosX), playerPos.y, playerPos.z);
        }
        else
        {
            gene = new Vector3(GetRandom(minGenePosX, maxGenePosX), genePos[genNum].y, genePos[genNum].z);
        }

        audioController.ChengePlayAudio(true);
        Instantiate(ob.gameObject, gene, Quaternion.identity, gameObject.transform);
        

    }

    /// <summary>
    /// ボスの生成
    /// </summary>
    public void GenerateBs()
    {
        var count = gameDirector.towerCount;
        audioController.ChengePlayAudio(true);
        Instantiate(bsObject[count], genePos[count], Quaternion.identity ,gameObject.transform);
    }

    private int GetRandom(int min, int max)
    {
        return Random.Range(min, max);
    }

    /// <summary>
    /// 生成するオブジェクトの確率を返す
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    private List<int> GetGenereationLists(int num)
    {
        switch(num)
        {
            case 0:
                return geneListsPlanLast;
            case 1:
                return geneListsPlan1;
            case 2:
                return geneListsPlan2;
            case 3:
                return geneListsPlan3;
            default:
                return null;
        }
    }


    /// <summary>
    /// 生成するオブジェクトを返す
    /// </summary>
    /// <param name="num">タワーの回数</param>
    /// <returns></returns>
    private CharacterController GenereationObject(int num)
    {
        List<int> generators = null;
        generators = GetGenereationLists(num);
        int ran = GetRandom(0, maxValue[num]);


        if(0 <= ran && ran < generators[0])
        {
            return geneObject[0];
        }
        else if(generators[0] < ran && ran < generators[1])
        {
            return geneObject[1];
        }
        else if (generators[1] < ran && ran < generators[2])
        {
            return geneObject[2];
        }
        else if (generators[2] < ran && ran < generators[3])
        {
            return geneObject[3];
        }
        else if (generators[3] < ran && ran < generators[4])
        {
            return geneObject[4];
        }
        else if (generators[4] < ran && ran < generators[5])
        {
            return geneObject[5];
        }
        else if (generators[5] < ran && ran < generators[6])
        {
            return geneObject[6];
        }
        else if (generators[0] < ran)
        {
            return geneObject[7];
        }

        return null;
    }
}

