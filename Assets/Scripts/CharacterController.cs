using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    [SerializeField] AnimeController anime;
    [SerializeField] UserInterface userInterface;
    private GameDirector gameDirector;
    [SerializeField] Rigidbody myRigidbody;
    [SerializeField] List<Transform> castle = new List<Transform>();
    [SerializeField] string enemyTag;  //敵のタグ
    [SerializeField] string damageTag;  //敵の攻撃タグ
    [SerializeField] string fireBallTag;
    [SerializeField] List<string> tower = new List<string>();  //敵の城のオブジェクト名
    [SerializeField] float moveSpeed;  //移動速度
    [SerializeField] float stopDistance;  //攻撃を開始するときの敵との距離
    private NavMeshAgent agent;
    private Transform target;
    [SerializeField] private List<Transform> targets = new List<Transform>();

    //遠距離型用
    [SerializeField] GameObject firePrefab;
    [SerializeField] float throwSpeed;

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
                    gameDirector.exp += userInterface.ExpCalculation();
                }
            }
        }

    }

    void Start()
    {
        anime.TransitionAnime("run");
        agent = GetComponent<NavMeshAgent>();
        gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();
        agent.speed = moveSpeed;
        agent.acceleration = moveSpeed;
        for(int i = 0; i < tower.Count; i++)
        {
            if (GameObject.Find(tower[i]))
            {
                castle.Add(GameObject.Find(tower[i]).GetComponent<Transform>());
            }
        }
        target = castle[0];
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    /// <summary>
    /// 受けるダメージ
    /// </summary>
    /// <param name="collision">コライダーがトリガーではない場合</param>
    /// <param name="collider">コライダーがトリガーの場合</param>
    private void HitDamage(Collision collision = null, GameObject ob = null)
    {
        GameObject root = null;//一番親のオブジェクト
        if (collision != null)
        {
            root = collision.gameObject.transform.root.gameObject;//一番親のオブジェクトを渡す
        }
        else if (ob != null)
        {
            root = ob.transform.root.gameObject;//一番親のオブジェクトを渡す
        }
        UserInterface user = root.GetComponentInChildren<Canvas>().GetComponent<UserInterface>();//UserInterfaceスクリプトを受け取る
        dieCheck = userInterface.DamegeValue(user.GetState(1));
    }

    public void FireBall()
    {
        if (target != null)
        {
            var worldTarget = transform.TransformDirection(target.localPosition);
            GameObject ball = Instantiate(firePrefab, transform.position, firePrefab.transform.rotation);
            //ball.transform.LookAt(target);
            ball.GetComponent<Rigidbody>().AddForce(worldTarget * throwSpeed, ForceMode.Impulse);
            // ball.GetComponent<Rigidbody>().AddForce(Vector3.forward * throwSpeed, ForceMode.Impulse);
        }

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

        if (other.gameObject.tag == enemyTag)
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
            if (targets[0] == null)
            {
                targets.RemoveAt(0);
            }


            //castle.RemoveAll(null);

            target = targets[0];  //リストの0番目の敵をターゲットに設定
        }

        if (target == null)  //敵ターゲットがいなくなったら城をターゲットに設定
        {
            target = castle[0];
        }
        else
        {
            transform.LookAt(target.position);
        }

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
        }
        else
        {
            transform.LookAt(target.position);
        }
    }
}
