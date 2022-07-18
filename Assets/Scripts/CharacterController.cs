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
    [SerializeField] Rigidbody myRigidbody;
    [SerializeField] List<Transform> castle = new List<Transform>();
    [SerializeField] string enemyTag;  //�G�̃^�O
    [SerializeField] string damageTag;  //�G�̍U���^�O
    [SerializeField] string fireBallTag;
    [SerializeField] List<string> tower = new List<string>();  //�G�̏�̃I�u�W�F�N�g��
    [SerializeField] float moveSpeed;  //�ړ����x
    [SerializeField] float stopDistance;  //�U�����J�n����Ƃ��̓G�Ƃ̋���
    private NavMeshAgent agent;
    private Transform target;
    [SerializeField] private List<Transform> targets = new List<Transform>();

    //�������^�p
    [SerializeField] GameObject firePrefab;
    [SerializeField] float throwSpeed;

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
    /// �󂯂�_���[�W
    /// </summary>
    /// <param name="collision">�R���C�_�[���g���K�[�ł͂Ȃ��ꍇ</param>
    /// <param name="collider">�R���C�_�[���g���K�[�̏ꍇ</param>
    private void HitDamage(Collision collision = null, GameObject ob = null)
    {
        GameObject root = null;//��Ԑe�̃I�u�W�F�N�g
        if (collision != null)
        {
            root = collision.gameObject.transform.root.gameObject;//��Ԑe�̃I�u�W�F�N�g��n��
        }
        else if (ob != null)
        {
            root = ob.transform.root.gameObject;//��Ԑe�̃I�u�W�F�N�g��n��
        }
        UserInterface user = root.GetComponentInChildren<Canvas>().GetComponent<UserInterface>();//UserInterface�X�N���v�g���󂯎��
        dieCheck = userInterface.DamegeValue(user.GetDamege());
    }

    public void FireBall()
    {
        var worldTarget = transform.TransformDirection(target.localPosition);
        GameObject ball = Instantiate(firePrefab, transform.position, firePrefab.transform.rotation);
        //ball.transform.LookAt(target);
        ball.GetComponent<Rigidbody>().AddForce(worldTarget * throwSpeed, ForceMode.Impulse);
       // ball.GetComponent<Rigidbody>().AddForce(Vector3.forward * throwSpeed, ForceMode.Impulse);
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


            //castle.RemoveAll(null);

            target = targets[0];  //���X�g��0�Ԗڂ̓G���^�[�Q�b�g�ɐݒ�
        }

        if (target == null)  //�G�^�[�Q�b�g�����Ȃ��Ȃ��������^�[�Q�b�g�ɐݒ�
        {
            target = castle[0];
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
    private void OnTriggerExit(Collider other)
    {
        if (castle.Any(n => n == null))
        {
            castle.RemoveAt(0);
        }

        if (target == null)  //�G�^�[�Q�b�g�����Ȃ��Ȃ��������^�[�Q�b�g�ɐݒ�
        {
            target = castle[0];
        }
        else
        {
            transform.LookAt(target.position);
        }
    }
}
