using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MonsterCard : MonoBehaviour
{
    private MonsterId monsterId;
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
        user = monster.GetComponentInChildren<Canvas>().GetComponent<UserInterface>();//モンスターオブジェクトの子にあるキャンバスからUserInterfaceを受け取る
        maxHp = user.GetState(0);
        attack = user.GetState(1);
        defence = user.GetState(2);
        cost = user.GetState(3);
        statasText.text = "HP：" + maxHp.ToString("D4") + "\nAT：" + attack.ToString("D4") + "\nDF：" + defence.ToString("D4");//ステータス表示
        costText.text = cost.ToString();//コストを表示

        monsterId = new MonsterId(id, attributes, maxHp, attack, defence, cost);
    }

    /// <summary>
    /// ステータスを返す
    /// 0：ID
    /// 1：属性
    /// 2：攻撃力
    /// 3：防御力
    /// 4：ＨＰ
    /// 5：コスト
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
    public void CardClick(Image clock)//クールタイム
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
        maxHp = user.GetState(4);
        attack = user.GetState(1);
        defence = user.GetState(2);

        statasText.text = "HP：" + maxHp.ToString("D4") + "\nAT：" + attack.ToString("D4") + "\nDF：" + defence.ToString("D4");//ステータス表示
    }
}
