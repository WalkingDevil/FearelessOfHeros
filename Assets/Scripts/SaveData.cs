using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    SavePath savePath = new SavePath();
    [SerializeField] List<int> defIdData;
    private void Awake()
    {
        string dataPath = Application.dataPath + "/SaveData.json";

        if(!File.Exists(dataPath))//セーブデータを作成
        {
            File.Create(dataPath);
            Save(savePath);
        }
        
        savePath = Load();

        if (savePath.fast)
        {
            savePath.level = 1;
            savePath.exp = 0;
            savePath.idData = defIdData;
            savePath.myDeckData = defIdData;
            savePath.fast = false;
            Save(savePath);
        }
        
    }


    public SavePath Load()
    {
        string dataPath = Application.dataPath + "/SaveData.json";
        StreamReader streamReader;
        streamReader = new StreamReader(dataPath);
        string data = streamReader.ReadToEnd();
        streamReader.Close();
        return JsonUtility.FromJson<SavePath>(data);
    }

    public void Save(SavePath newSave)
    {
        string dataPath = Application.dataPath + "/SaveData.json";
        string jsonstr = JsonUtility.ToJson(newSave);//受け取ったPlayerDataをJSONに変換
        StreamWriter writer = new StreamWriter(dataPath, false);//初めに指定したデータの保存先を開く
        writer.WriteLine(jsonstr);//JSONデータを書き込み
        writer.Flush();//バッファをクリアする
        writer.Close();//ファイルをクローズする
        //File.WriteAllText(dataPath, jsonstr);
        /*
#if UNITY_EDITOR
        {
            StreamWriter writer = new StreamWriter(dataPath, false);//初めに指定したデータの保存先を開く
            writer.WriteLine(jsonstr);//JSONデータを書き込み
            writer.Flush();//バッファをクリアする
            writer.Close();//ファイルをクローズする
        }
#endif
        {
            File.WriteAllText(dataPath, jsonstr);
        }*/

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
    public float exp = 0;
    public int maxExp = 50;
    public bool fast = true;
    public List<int> idData;
    public List<int> myDeckData;
}
