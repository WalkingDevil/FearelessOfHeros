using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    [SerializeField] AnimeController anime;
    private Transform castle;
    [SerializeField] string enemyTag;
    [SerializeField] string tower;
    private NavMeshAgent agent;
    [SerializeField] private Transform target;
    [SerializeField] private List<Transform> targets = new List<Transform>();

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
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == enemyTag)
        {
            targets.Add(other.gameObject.transform);
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
    }
}
