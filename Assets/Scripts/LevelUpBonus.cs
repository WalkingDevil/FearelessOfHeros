using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpBonus : MonoBehaviour
{
    private SaveData saveData = new SaveData();
    private SavePath savePath = new SavePath();
    private List<int> idDatas;
    private int cost;
    private int startLevel = 0;
    private int krystal;
    [SerializeField] int kystaalRate = 10;
    [SerializeField] int clearKrystaal = 10;
    [SerializeField] RectTransform content;
    [SerializeField] RectTransform resultPrefab;
    const int zero = 0;
    //�J�n���̃��x��������
    public void SetStartLevel(int startLevel = 0)
    {
        this.startLevel = startLevel;
    }

    private void Start()
    {
        saveData = new SaveData();
        savePath = saveData.Load();
        idDatas = savePath.idData;
        cost = savePath.cost;
    }

    /// <summary>
    /// ���x���A�b�v���̕\��
    /// </summary>
    /// <param name="endLevel"></param>
    public void LevelBonus(int endLevel = 0)
    {
        if (startLevel != endLevel)
        {
            int l = startLevel + 1;
            for (; l <= endLevel; l++)
            {
                SetBonus(l);
            }
            string massege = startLevel + " �� " + endLevel + "�Ƀ��x���A�b�v����";
            GenerationText(massege);
        }
    }

    /// <summary>
    /// �N���A���̃{�[�i�X
    /// </summary>
    /// <param name="enemyCount">�G��|������</param>
    /// <param name="allyCount">�������|�ꂽ��</param>
    public void ClearBonus(int enemyCount, int allyCount)
    {
        GenerationText("�G��" + enemyCount + "�̓|���܂����B");
        GenerationText("������" + enemyCount + "�̓|��܂����B");

        //�G��|������ - �|���ꂽ����0�ȏ�̐��Ōv�Z
        var diff = Mathf.Clamp(enemyCount - allyCount, zero, Mathf.Infinity);
        //��������A�����_�ȉ��؂�グ�Ōv�Z
        krystal =  (int)Mathf.Ceil(diff / kystaalRate) + clearKrystaal;

        GenerationText("�N���X�^����" + krystal + "��ɓ��ꂽ�B");

    }

    /// <summary>
    /// ���x���ɍ��킹���{�[�i�X
    /// </summary>
    /// <param name="num">���x��</param>
    private void SetBonus(int num)
    {
        List<int> list;
        switch (num)
        {
            case 3:
                list = new List<int>() { 5, 6};
                idDatas.AddRange(list);
                GenerationText("�V����2�L�����ǉ�����܂����B");
                break;
            case 5:
                list = new List<int>(){ 7, 8};
                idDatas.AddRange(list);
                GenerationText("�V����2�L�����ǉ�����܂����B");
                cost++;
                GenerationText("�R�X�g���P�������܂����B");
                break;
            default:
                Debug.Log(num);
                break;
        }
    }

    /// <summary>
    /// ���b�Z�[�W��ǉ�����
    /// </summary>
    /// <param name="r">���b�Z�[�W</param>
    private void GenerationText(string r)
    {
        GameObject ob = Instantiate(resultPrefab.gameObject, content);
        ob.GetComponent<Text>().text = r;
    }

    /// <summary>
    /// �V�����ۑ����郂���X�^�[�f�[�^
    /// </summary>
    /// <returns></returns>
    public List<int> SetIdData()
    {
        return idDatas;
    }

    /// <summary>
    /// �V�����ۑ�����R�X�g
    /// </summary>
    /// <returns></returns>
    public int SetCost()
    {
        return cost;
    }

    public int SetKrystaal()
    {
        return krystal;
    }
}
