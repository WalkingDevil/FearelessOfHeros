using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    string dataPath;
    SavePath savePath = new SavePath();
    [SerializeField] List<int> defIdData;
    private void Awake()
    {
        dataPath = Application.dataPath + "/SaveData.json";
        savePath = Load();
        if (savePath.level == 0)
        {
            savePath.level = 1;
            savePath.exp = 0;
            savePath.idData = defIdData;
            savePath.myDeckData = defIdData;
            Save(savePath);
        }
    }


    public SavePath Load()
    {
        dataPath = Application.dataPath + "/SaveData.json";
        StreamReader streamReader;
        streamReader = new StreamReader(dataPath);
        string data = streamReader.ReadToEnd();
        streamReader.Close();
        return JsonUtility.FromJson<SavePath>(data);
    }

    public void Save(SavePath newSave)
    {
        string jsonstr = JsonUtility.ToJson(newSave);//�󂯎����PlayerData��JSON�ɕϊ�
        StreamWriter writer = new StreamWriter(dataPath, false);//���߂Ɏw�肵���f�[�^�̕ۑ�����J��
        writer.WriteLine(jsonstr);//JSON�f�[�^����������
        writer.Flush();//�o�b�t�@���N���A����
        writer.Close();//�t�@�C�����N���[�Y����
    }

  /*  public void SetData(List<MonsterCard> cards)
    {
        savePath.myMonsterCards = cards;
        Save();
    }*/
}

[Serializable]
public class SavePath
{
    public int level;
    public float exp;
    public int maxExp;
    public List<int> idData;
    public List<int> myDeckData;
}
