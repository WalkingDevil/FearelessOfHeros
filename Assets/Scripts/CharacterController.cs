using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    [SerializeField] AnimeController anime;
    [SerializeField] UserInterface userInterface;
    private Transform castle;
    [SerializeField] string enemyTag;
    [SerializeField] string tower;
    [SerializeField] float moveSpeed;
    [SerializeField] float stopDistance;
    private NavMeshAgent agent;
    [SerializeField] private Transform target;
    [SerializeField] private List<Transform> targets = new List<Transform>();

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

    // Update is called once per frame
    void Update()
    {
        // anime.TransitionAnime("attack");
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == enemyTag)
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
                    targets.Add(other.gameObject.transform);
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

            target = targets[0];
        }

        if (target == null)
        {
            target = castle;
        }

        if (agent.remainingDistance < stopDistance)
        {
            if (agent.speed != 0f)
            {
                agent.speed = 0f;
                anime.TransitionAnime("attack");
            }
        }
        else
        {
            if (agent.speed != moveSpeed)
            {
                agent.speed = moveSpeed;
                anime.TransitionAnime("run");
            }
        }
    }
}
