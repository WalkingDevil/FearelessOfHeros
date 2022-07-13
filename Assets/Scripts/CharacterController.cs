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
    [SerializeField] string enemyTag;  //�G�̃^�O
    [SerializeField] string damageTag;  //�G�̍U���^�O
    [SerializeField] string tower;  //�G�̏�̃^�O
    [SerializeField] float moveSpeed;  //�ړ����x
    [SerializeField] float stopDistance;  //�U�����J�n����Ƃ��̓G�Ƃ̋���
    private NavMeshAgent agent;
    private Transform target;
    [SerializeField] private List<Transform> targets = new List<Transform>();
    [SerializeField] GameObject firePrefab;//�������^�p

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
            GameObject root = collision.gameObject.transform.root.gameObject;//��Ԑe�̃I�u�W�F�N�g��n��
            UserInterface user = root.GetComponentInChildren<Canvas>().GetComponent<UserInterface>();//UserInterface�X�N���v�g���󂯎��
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
                    targets.Add(other.gameObject.transform);  //�^�[�Q�b�g�����X�g�ɐݒ�
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

            target = targets[0];  //���X�g��0�Ԗڂ̓G���^�[�Q�b�g�ɐݒ�
        }

        if (target == null)  //�G�^�[�Q�b�g�����Ȃ��Ȃ��������^�[�Q�b�g�ɐݒ�
        {
            target = castle;
        }
        else
        {
            transform.LookAt(target.position);
        }

        if (agent.remainingDistance < stopDistance)  //�G�Ɉ�苗���߂Â�����
        {
            if (agent.speed != 0f)  //�~�܂��čU��
            {
                agent.speed = 0f;
               // myRigidbody.isKinematic = false;
                anime.TransitionAnime("attack");
            }
        }
        else
        {
            if (agent.speed != moveSpeed)  //�G�����Ȃ��Ȃ�����^�[�Q�b�g�Ɍ������Ĉړ�
            {
                agent.speed = moveSpeed;
               // myRigidbody.isKinematic = true;
                anime.TransitionAnime("run");
            }
        }
    }
}
