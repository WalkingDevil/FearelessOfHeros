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
    [SerializeField] string enemyTag;  //�G�̃^�O
    [SerializeField] string damageTag;  //�G�̍U���^�O
    [SerializeField] string fireBallTag;
    [SerializeField] string kingName;
    [SerializeField] List<string> tower = new List<string>();  //�G�̏�̃I�u�W�F�N�g��
    [SerializeField] float moveSpeed;  //�ړ����x
    [SerializeField] float stopDistance;  //�U�����J�n����Ƃ��̓G�Ƃ̋���
    private NavMeshAgent agent;
    [SerializeField] private Transform target;
    private Vector3 latestPos;
    private Vector3 diff;
    private bool detection = false;
    private float distance = 0;  //�^�[�Q�b�g�Ƃ̋���
    private float keepDistance = 10000;  //��ԋ߂��^�[�Q�b�g�Ƃ̋�����ۑ�
    private float timeCalculate = 1f;  //�^�[�Q�b�g�Ƃ̋������ēx����Ȃ����܂ł̎���
    private bool timeStart = false;
    private float timeCount = 0;
    [SerializeField] private List<Transform> targets = new List<Transform>();

    //�������^�p
    [SerializeField] GameObject firePrefab;
    [SerializeField] float throwSpeed;
    [SerializeField] float throwPos = 5f;

    [SerializeField] int plusCost = 2;//�R�X�g�̑���
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

    [SerializeField] bool enemy = false;//�G�ł��邩�ǂ���
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
            diff = transform.position - latestPos;   //�O�񂩂�ǂ��ɐi�񂾂����x�N�g���Ŏ擾
            latestPos = transform.position;  //�O���Position�̍X�V
            if (diff.magnitude > 0.01f)
            {
                transform.rotation = Quaternion.LookRotation(diff); //������ύX����
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
    /// �ڕW�̏�̖��O��T��
    /// </summary>
    /// <param name="enemy"></param>
    private void SearchTower(bool enemy)
    {
        //�G�ł��邩�ǂ���
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
    /// �ړI�̏������
    /// </summary>
    /// <param name="name"></param>
    private void AddCastle(string name)
    {
        castle.Add(GameObject.Find(name).GetComponent<Transform>());
    }

    /// <summary>
    /// �󂯂�_���[�W
    /// </summary>
    /// <param name="collision">�R���C�_�[���g���K�[�ł͂Ȃ��ꍇ</param>
    /// <param name="collider">�R���C�_�[���g���K�[�̏ꍇ</param>
    private void HitDamage(Collision collision = null, GameObject ob = null)
    {
        GameObject root = null;//�I�u�W�F�N�g
        if (collision != null)
        {
            root = collision.gameObject.transform.root.gameObject;//�I�u�W�F�N�g��n��
        }
        else if (ob != null)
        {
            root = ob.transform.gameObject.transform.root.gameObject;//�I�u�W�F�N�g��n��
        }
        UserInterface user = root.GetComponent<UserInterface>();//UserInterface�X�N���v�g���󂯎��
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
                    targets.Add(other.gameObject.transform);  //�^�[�Q�b�g�����X�g�ɐݒ�
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
                for (int i = 0; i < targets.Count; i++)  //�G�Ƃ̋����𑪂�
                {

                    if (targets[i] == null)
                    {
                        Debug.Log("�~�b�V���O");
                        targets.RemoveAt(i);
                    }
                    else
                    {
                        distance = (transform.position - targets[i].position).magnitude;

                        if (distance < keepDistance)  //��ԋ߂��G���^�[�Q�b�g�ɂ���
                        {
                            target = targets[i];
                            keepDistance = distance;
                        }
                    }
                }
                timeCount = timeCalculate;
            }
            //target = targets[0];  //���X�g��0�Ԗڂ̓G���^�[�Q�b�g�ɐݒ�
        }

        //if (target == null)  //�G�^�[�Q�b�g�����Ȃ��Ȃ��������^�[�Q�b�g�ɐݒ�
        //{
            //target = castle[0];
            //detection = false;
            //agent.angularSpeed = 0;
        //}
        //else
        //{
            //transform.LookAt(target);
        //}

        if (agent.remainingDistance < stopDistance)  //�G�Ɉ�苗���߂Â�����
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

        if (target == null)  //�G�^�[�Q�b�g�����Ȃ��Ȃ��������^�[�Q�b�g�ɐݒ�
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

        //if (target == null)  //�G�^�[�Q�b�g�����Ȃ��Ȃ��������^�[�Q�b�g�ɐݒ�
        //{
        //target = castle[0];
        //}
    }
}
