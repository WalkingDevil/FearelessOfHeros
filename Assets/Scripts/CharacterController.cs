using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    [SerializeField] GameDirector.GameState gameState = GameDirector.GameState.InGame;
    [SerializeField] AnimeController anime;
    [SerializeField] UserInterface userInterface;
    private GameDirector gameDirector;
    [SerializeField] Rigidbody myRigidbody;
    [SerializeField] List<Transform> castle = new List<Transform>();
    [SerializeField] string enemyTag;  //敵のタグ
    [SerializeField] string damageTag;  //敵の攻撃タグ
    [SerializeField] string fireBallTag;
    [SerializeField] string kingName;
    [SerializeField] List<string> tower = new List<string>();  //敵の城のオブジェクト名
    [SerializeField] float moveSpeed;  //移動速度
    [SerializeField] float stopDistance;  //攻撃を開始するときの敵との距離
    private NavMeshAgent agent;
    [SerializeField] private Transform target;
    private Vector3 latestPos;
    private Vector3 diff;
    private bool detection = false;
    private float distance = 0;  //ターゲットとの距離
    private float keepDistance = 10000;  //一番近いターゲットとの距離を保存
    private float timeCalculate = 1f;  //ターゲットとの距離を再度測りなおすまでの時間
    private bool timeStart = false;
    private float timeCount = 0;
    [SerializeField] private List<Transform> targets = new List<Transform>();

    //遠距離型用
    [SerializeField] GameObject firePrefab;
    [SerializeField] float throwSpeed;
    [SerializeField] float throwPos = 5f;

    [SerializeField] int plusCost = 2;//コストの増量
    private bool _dieCheck;
    public bool dieCheck
    {
        get { return _dieCheck; }
        set
        {
            if (_dieCheck != value)
            {
                _dieCheck = value;

                if(_dieCheck)
                {
                    anime.TransitionAnime("die");
                    gameDirector.cost += plusCost;
                    if (enemy)
                    {
                        gameDirector.exp += userInterface.ExpCalculation();
                    }
                }
            }
        }

    }

    [SerializeField] bool enemy = false;//敵であるかどうか
    void Start()
    {
        userInterface = GetComponent<UserInterface>();
        gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();
        if(!enemy)
        {
            userInterface.ChengeState(gameDirector.level);
        }
        anime.TransitionAnime("run");
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.acceleration = moveSpeed;
        agent.angularSpeed = 0;

        SearchTower(enemy);
        target = castle[0];
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }

        if (!detection)
        {
            diff = transform.position - latestPos;   //前回からどこに進んだかをベクトルで取得
            latestPos = transform.position;  //前回のPositionの更新
            if (diff.magnitude > 0.01f)
            {
                transform.rotation = Quaternion.LookRotation(diff); //向きを変更する
            }
        }

        if (timeStart)
        {
            timeCount -= Time.deltaTime;
        }

        if(gameDirector.loadState != gameState)
        {
            Destroy(gameObject);
        }

    }

    /// <summary>
    /// 目標の城の名前を探す
    /// </summary>
    /// <param name="enemy"></param>
    private void SearchTower(bool enemy)
    {
        //敵であるかどうか
        if(enemy)
        {
            if (GameObject.Find(tower[0]))
            {
                AddCastle(tower[0]);
            }
            else
            {
                AddCastle(kingName);
            }
        }
        else
        {
            for (int i = 0; i < tower.Count; i++)
            {
                if (GameObject.Find(tower[i]))
                {
                    AddCastle(tower[i]);
                }
            }
        }
    }
    /// <summary>
    /// 目的の城を入れる
    /// </summary>
    /// <param name="name"></param>
    private void AddCastle(string name)
    {
        castle.Add(GameObject.Find(name).GetComponent<Transform>());
    }

    /// <summary>
    /// 受けるダメージ
    /// </summary>
    /// <param name="collision">コライダーがトリガーではない場合</param>
    /// <param name="collider">コライダーがトリガーの場合</param>
    private void HitDamage(Collision collision = null, GameObject ob = null)
    {
        GameObject root = null;//オブジェクト
        if (collision != null)
        {
            root = collision.gameObject.transform.root.gameObject;//オブジェクトを渡す
        }
        else if (ob != null)
        {
            root = ob.transform.gameObject.transform.root.gameObject;//オブジェクトを渡す
        }
        UserInterface user = root.GetComponent<UserInterface>();//UserInterfaceスクリプトを受け取る
        dieCheck = userInterface.DamegeValue(user.GetState(1), (int)user.GetState(6));
    }

    public void FireBall()
    {
        if (target != null)
        {
            var worldTarget = transform.TransformDirection(target.localPosition);
            worldTarget.y += throwPos;
            GameObject ball = Instantiate(firePrefab, transform.position, firePrefab.transform.rotation);
            //ball.transform.LookAt(target);
            ball.GetComponent<Rigidbody>().AddForce(worldTarget * throwSpeed, ForceMode.Impulse);
            // ball.GetComponent<Rigidbody>().AddForce(Vector3.forward * throwSpeed, ForceMode.Impulse);
        }

    }

    public UserInterface SetMyUserInterface()
    {
        return userInterface;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == damageTag)
        {
            HitDamage(collision);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == fireBallTag)
        {
            HitDamage(null, other);
        }
    }

    void OnTriggerEnter(Collider other)
    {
      /*  if (other.gameObject.tag == fireBallTag)
        {
            HitDamage(null, other);
        }*/

        if (other.gameObject.tag == enemyTag && other.gameObject != castle[0])
        {

            if (other.isTrigger)
            {
                if (!targets.Contains(other.gameObject.transform))
                {
                    targets.Add(other.gameObject.transform);  //ターゲットをリストに設定
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == enemyTag)
        {

            timeStart = true;
            agent.angularSpeed = 120;
            detection = true;

            //castle.RemoveAll(null);

            if (timeCount < 0)
            {
                for (int i = 0; i < targets.Count; i++)  //敵との距離を測る
                {

                    if (targets[i] == null)
                    {
                        Debug.Log("ミッシング");
                        targets.RemoveAt(i);
                    }
                    else
                    {
                        distance = (transform.position - targets[i].position).magnitude;

                        if (distance < keepDistance)  //一番近い敵をターゲットにする
                        {
                            target = targets[i];
                            keepDistance = distance;
                        }
                    }
                }
                timeCount = timeCalculate;
            }
            //target = targets[0];  //リストの0番目の敵をターゲットに設定
        }

        //if (target == null)  //敵ターゲットがいなくなったら城をターゲットに設定
        //{
            //target = castle[0];
            //detection = false;
            //agent.angularSpeed = 0;
        //}
        //else
        //{
            //transform.LookAt(target);
        //}

        if (agent.remainingDistance < stopDistance)  //敵に一定距離近づいたら
        {
            agent.isStopped = true;
            // myRigidbody.isKinematic = false;
            anime.TransitionAnime("attack");
        }
        else
        {
            agent.isStopped = false;
            // myRigidbody.isKinematic = true;
            anime.TransitionAnime("run");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (castle.Any(n => n == null))
        {
            castle.RemoveAt(0);
        }

        if (target == null)  //敵ターゲットがいなくなったら城をターゲットに設定
        {
            target = castle[0];
            detection = false;
            agent.angularSpeed = 0;
            timeStart = false;
            timeCount = 0;
        }
        else
        {
            transform.LookAt(target);
        }

        //if (target == null)  //敵ターゲットがいなくなったら城をターゲットに設定
        //{
        //target = castle[0];
        //}
    }
}
