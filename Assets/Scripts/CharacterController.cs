using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    [SerializeField] AnimeController anime;
    [SerializeField] UserInterface userInterface;
    [SerializeField] Rigidbody myRigidbody;
    private Transform castle;
    [SerializeField] string enemyTag;  //敵のタグ
    [SerializeField] string damageTag;  //敵の攻撃タグ
    [SerializeField] string tower;  //敵の城のタグ
    [SerializeField] float moveSpeed;  //移動速度
    [SerializeField] float stopDistance;  //攻撃を開始するときの敵との距離
    private NavMeshAgent agent;
    private Transform target;
    [SerializeField] private List<Transform> targets = new List<Transform>();
    [SerializeField] GameObject firePrefab;//遠距離型用

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
                }
            }
        }

    }

    void Start()
    {
        anime.TransitionAnime("run");
        agent = GetComponent<NavMeshAgent>();
        castle = GameObject.Find(tower).GetComponent<Transform>();
        target = castle;
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    public void FireBall()
    {
        GameObject ball = Instantiate(firePrefab, transform.position, Quaternion.identity);
       // ball.
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == damageTag)
        {
            GameObject root = collision.gameObject.transform.root.gameObject;//一番親のオブジェクトを渡す
            UserInterface user = root.GetComponentInChildren<Canvas>().GetComponent<UserInterface>();//UserInterfaceスクリプトを受け取る
            dieCheck = userInterface.DamegeValue(user.GetDamege());
        }
    }

    void OnTriggerEnter(Collider other)
    {
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

            target = targets[0];  //リストの0番目の敵をターゲットに設定
        }

        if (target == null)  //敵ターゲットがいなくなったら城をターゲットに設定
        {
            target = castle;
        }
        else
        {
            transform.LookAt(target.position);
        }

        if (agent.remainingDistance < stopDistance)  //敵に一定距離近づいたら
        {
            if (agent.speed != 0f)  //止まって攻撃
            {
                agent.speed = 0f;
               // myRigidbody.isKinematic = false;
                anime.TransitionAnime("attack");
            }
        }
        else
        {
            if (agent.speed != moveSpeed)  //敵がいなくなったらターゲットに向かって移動
            {
                agent.speed = moveSpeed;
               // myRigidbody.isKinematic = true;
                anime.TransitionAnime("run");
            }
        }
    }
}
