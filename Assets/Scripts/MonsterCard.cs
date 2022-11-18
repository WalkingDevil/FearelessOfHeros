using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MonsterCard : MonoBehaviour
{
    [SerializeField] GameDirector gameDirector;
    private CharacterGenerator allyGene;
    private UserInterface user;
    [SerializeField] CharacterController monster;
    [SerializeField] Button cardButton;
    [SerializeField] Text statasText;
    [SerializeField] Text costText;
    [SerializeField] string gameScene = "GameScene";
    [SerializeField] int id;
    [SerializeField] int attributes;
    private int maxHp;
    private int attack;
    private int defence;
    private int cost;
    private int _geneNumber;
    public int geneNumber
    {
        get { return _geneNumber; }
        set
        {
            _geneNumber = value;
            allyGene = GameObject.Find(geneName).GetComponent<CharacterGenerator>();
            this.gameObject.GetComponent<Button>().onClick.AddListener(() => allyGene.GenerateChara(_geneNumber));
        }

    }
    public int time;
    [SerializeField] string director = "GameDirector";
    [SerializeField] string geneName = "AllyGene";
    private bool checkScene;
    void Start()
    {
        checkScene = SceneManager.GetActiveScene().name == gameScene;
        if(checkScene)
        {
            gameDirector = GameObject.Find(director).GetComponent<GameDirector>();

        }
        NewState();
        
    }

    private void Update()
    {
       // Debug.Log(this.gameObject.GetComponent<Button>().onClick.GetPersistentEventCount());
    }
    private void NewState()
    {
        user = monster.gameObject.GetComponent<UserInterface>();//�����X�^�[�I�u�W�F�N�g�̎q�ɂ���L�����o�X����UserInterface���󂯎��
        if(!checkScene)
        {
            SetState(StartDirector.level == 1);
        }
        else
        {
            SetState(gameDirector.level == 1);
        }


        cost = (int)user.GetState(3);
        statasText.text = "HP�F" + maxHp.ToString("D4") + "\nAT�F" + attack.ToString("D4") + "\nDF�F" + defence.ToString("D4");//�X�e�[�^�X�\��
        costText.text = cost.ToString();//�R�X�g��\��
    }

    private void SetState(bool scene)
    {
        if (scene)
        {
            maxHp = (int)user.GetState(0);
            attack = (int)user.GetState(5);
            defence = (int)user.GetState(6);
        }
        else
        {
            maxHp = (int)user.GetState(4);
            attack = (int)user.GetState(1);
            defence = (int)user.GetState(2);
        }
    }

    /// <summary>
    /// �X�e�[�^�X��Ԃ�
    /// 0�FID
    /// 1�F����
    /// 2�F�U����
    /// 3�F�h���
    /// 4�F�g�o
    /// 5�F�R�X�g
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public int GetState(int num)
    {
        NewState();   
        switch (num)
        {
            case 0:
                return id;
            case 1:
                return attributes;
            case 2:
                return attack;
            case 3:
                return defence;
            case 4:
                return maxHp;
            case 5:
                return cost;
            default:
                return 0;
        }
    }
    public void CardClick(Image clock)//�N�[���^�C��
    {
        if (checkScene)
        {
            if (gameDirector.cost >= user.GetState(3))
            {
                cardButton.interactable = false;
                DOTween.To
                    (
                    () => clock.fillAmount,
                    (t) => clock.fillAmount = t,
                    1,
                    time
                    )
                    .OnComplete(() => ResetButton(clock));
            }
        }
    }

    public void SetNumber(int num)
    {
        geneNumber = num;
    }

    public CharacterController GetObject()
    {
        return monster;
    }
    private void ResetButton(Image clock)
    {
        cardButton.interactable = true;
        clock.fillAmount = 0;
    }

    public void ResetDisplay()
    {
        maxHp = (int)user.GetState(4);
        attack = (int)user.GetState(1);
        defence = (int)user.GetState(2);

        statasText.text = "HP�F" + maxHp.ToString("D4") + "\nAT�F" + attack.ToString("D4") + "\nDF�F" + defence.ToString("D4");//�X�e�[�^�X�\��
    }
}
