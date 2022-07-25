using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    string dataPath;
    SavePath savePath = new SavePath();
    private void Awake()
    {
        dataPath = Application.dataPath + "/SaveData.json";

        if(savePath.level == 0)
        {
            savePath.level = 1;
            Save();
        }
    }


    public void Load()
    {
        if (File.Exists(dataPath))
        {
            StreamReader streamReader;
            streamReader = new StreamReader(dataPath);
            string data = streamReader.ReadToEnd();
            streamReader.Close();
            savePath = JsonUtility.FromJson<SavePath>(data);
        }
    }

    public void Save()
    {
        string jsonstr = JsonUtility.ToJson(savePath);//�󂯎����PlayerData��JSON�ɕϊ�
        StreamWriter writer = new StreamWriter(dataPath, false);//���߂Ɏw�肵���f�[�^�̕ۑ�����J��
        writer.WriteLine(jsonstr);//JSON�f�[�^����������
        writer.Flush();//�o�b�t�@���N���A����
        writer.Close();//�t�@�C�����N���[�Y����
    }
}

[Serializable]
public class SavePath
{
    public int level;
}
