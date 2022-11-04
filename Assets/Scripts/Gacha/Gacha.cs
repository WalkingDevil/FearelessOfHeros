using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class Gacha : MonoBehaviour
{
    [SerializeField] GachaLottery gachaLottery;
    [SerializeField] GameObject cardPanel;
    [SerializeField] GameObject effectObj;  // �����X�^�[�o�ꎞ�̃G�t�F�N�g
    [SerializeField] GameObject skipBotton;  // �X�L�b�v�{�^��
    [SerializeField] GameObject titleBotton;  // �^�C�g���ɖ߂�{�^��
    public List<int> character = new List<int>();  // �K�`���̒��I����
    [SerializeField] private List<MonsterCard> monsterCards = new List<MonsterCard>();  // �����X�^�[�̎��
    public List<GameObject> objects = new List<GameObject>();  // ���I���ꂽ�����X�^�[
    public List<MonsterCard> cards = new List<MonsterCard>();  // ���I���ꂽ�����X�^�[�J�[�h
    static public int gachaCount = 10;  //�K�`���̉�
    private bool skip = false;  // �X�L�b�v���邩�ǂ���
    private bool skippable = false;  // �X�L�b�v�\��
    // Start is called before the first frame update
    void Start()
    {
        GachaStart(gachaCount);
        skipBotton.SetActive(true);
        titleBotton.SetActive(false);
        cardPanel.SetActive(false);
    }

    public void SkipButton()  // �X�L�b�v�{�^��
    {
        if (skippable)
        {
            if (!skip)
            {
                skip = true;
            }
        }
    }

    public void BackTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void GachaStart(int countPerGacha)  //�K�`���̒��I
    {
        Debug.Log(countPerGacha);
        character = gachaLottery.Lottery(countPerGacha);
        for (int i = 0; i < character.Count; i++)
        {
            for (int j = 0; j < monsterCards.Count; j++)
            {
                if (character[i] == monsterCards[j].GetState(0))  // ���I���ʂ������X�^�[�ƃ����X�^�[�J�[�h�̃��X�g�ɓ����
                {
                    objects.Add(monsterCards[j].GetObject().gameObject);
                    cards.Add(monsterCards[j]);
                }
            }
        }

        StartCoroutine("Performance");

    }

    IEnumerator Performance()  // �K�`�����o
    {
        foreach (GameObject obj in objects)  // �����X�^�[��\��
        {
            yield return StartCoroutine("Effect");
            skippable = true;
            GameObject game = Instantiate(obj, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
            game.GetComponentInChildren<Canvas>().GetComponent<UserInterface>().ChengeGach();
            game.GetComponent<CharacterController>().enabled = false;
            game.GetComponent<NavMeshAgent>().enabled = false;
            game.GetComponent<BoxCollider>().enabled = false;
            Destroy(game.GetComponent<Rigidbody>());
            yield return StartCoroutine(PerformanceSkip(3));
            skippable = false;
            if (skip)
            {
                skip = false;
            }
            Destroy(game);
        }

        yield return new WaitForSeconds(0.5f);
        skipBotton.SetActive(false);
        cardPanel.SetActive(true);
        yield return new WaitForSeconds(1f);

        foreach (MonsterCard obj in cards)  // �����X�^�[�J�[�h��\��
        {
            MonsterCard card = Instantiate(obj) as MonsterCard;
            card.transform.parent = cardPanel.transform;
            yield return new WaitForSeconds(0.5f);
        }

        titleBotton.SetActive(true);
    }

    IEnumerator Effect()  // �����X�^�[�o�ꎞ�̃G�t�F�N�g
    {
        GameObject effect = Instantiate(effectObj, new Vector3(0,5,0), Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(1f);
        Destroy(effect);
    }

    /// <summary>
    /// �X�L�b�v
    /// </summary>
    /// <param name="count">�҂����ԁi�b�j</param>
    /// <returns></returns>
    IEnumerator PerformanceSkip(int count)
    {
        for(int i = 0; i < count; i++)
        {
            if (skip && skippable)
            {
                skip = false;
                break;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
