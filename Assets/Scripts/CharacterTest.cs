using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterTest : MonoBehaviour
{
    private Transform castle;
    [SerializeField] string enemyTag;
    [SerializeField] string tower;
    [SerializeField] float moveSpeed;
    [SerializeField] float stopDistance;
    private NavMeshAgent agent;
    [SerializeField] private Transform target;
    [SerializeField] private List<Transform> targets = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        castle = GameObject.Find(tower).GetComponent<Transform>();
        target = castle;
    }

    // Update is called once per frame
    void Update()
    {
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

        if (agent.remainingDistance < stopDistance)
        {
            if (agent.speed != 0f)
            {
                agent.speed = 0f;
            }
        }
        else
        {
            if (agent.speed != moveSpeed)
            {
                agent.speed = moveSpeed;
            }
        }
    }
}
