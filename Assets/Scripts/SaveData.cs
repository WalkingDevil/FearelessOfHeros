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
        string jsonstr = JsonUtility.ToJson(savePath);//受け取ったPlayerDataをJSONに変換
        StreamWriter writer = new StreamWriter(dataPath, false);//初めに指定したデータの保存先を開く
        writer.WriteLine(jsonstr);//JSONデータを書き込み
        writer.Flush();//バッファをクリアする
        writer.Close();//ファイルをクローズする
    }
}

[Serializable]
public class SavePath
{
    public int level;
}
