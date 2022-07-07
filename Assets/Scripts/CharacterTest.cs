using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterTest : MonoBehaviour
{
    [SerializeField] Transform castle;
    [SerializeField] string enemyTag;
    private NavMeshAgent agent;
    [SerializeField] private Transform target;
    [SerializeField] private List<Transform> targets = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
            Debug.Log("ì¸Ç¡ÇΩ");
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
            Debug.Log("èÈ");
            target = castle;
        }
    }
}
