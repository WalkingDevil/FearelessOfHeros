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
        string dataPath = DataPath(true);

        if (!File.Exists(dataPath))//セーブデータを作成
        {
            // File.Create(dataPath);
            savePath.level = 1;
            savePath.exp = 0;
            savePath.idData = defIdData;
            savePath.myDeckData = defIdData;
            savePath.cost = 6;
            savePath.krystaal = 0;
            Save(savePath);
        }

        savePath = Load();


    }


    public SavePath Load()
    {
        string dataPath = DataPath(true);
        StreamReader streamReader;
        streamReader = new StreamReader(dataPath);
        string data = streamReader.ReadToEnd();
        streamReader.Close();
        return JsonUtility.FromJson<SavePath>(data);
    }

    public void Save(SavePath newSave)
    {
        string dataPath = DataPath();
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
    private String DataPath(bool load = false)
    {
        string path = null;
        /*
#if UNITY_WEBGL
        {
            if (load)
            {
                path = Application.streamingAssetsPath + "/SaveData.csv";
            }
            else
            {
                path = Application.persistentDataPath + "/SaveData.csv";
            }

        }
#endif

#if UNITY_EDITOR || UNITY_EDITOR_WIN
        {
            {
                path = Application.dataPath + "/SaveData.json";
            }

            
        }
#endif*/
        path = Application.dataPath + "/SaveData.json";
        return path;
    }
}



[Serializable]
public class SavePath
{
    public int level = 0;
    public float exp = 0;
    public int maxExp = 50;
    public int cost = 6;
    public int krystaal = 0;
    public bool fast = true;
    public List<int> idData;
    public List<int> myDeckData;
}
