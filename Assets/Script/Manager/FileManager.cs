using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class FileManager : Singleton<FileManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    //public string PlayerItemDataPath
    //{
    //    get { return Application.streamingAssetsPath + "/PlayerItemData.json"; }
    //}
    //public string TipsDataPath
    //{
    //    get { return Application.streamingAssetsPath + "/TipsData.json"; }
    //}
    public string PlayerAchievementDataPath
    {
        get { return Application.streamingAssetsPath + "/PlayerAchievementData.json"; }
    }
    public void Write<T>(T data, string path)
    {
        if (!File.Exists(path))
        {
            File.Create(path).Dispose();
        }
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }
    //public void CreateFile<T>(T data, string path)
    //{
    //    if (!File.Exists(path))
    //    {
    //        File.Create(path).Dispose();

    //        string json = JsonUtility.ToJson(data);
    //        File.WriteAllText(path, json);
    //    }
    //}
    public bool ReadFile<T>(string path, out T data)
    {
        if (!File.Exists(path))
        {
            data = default(T);
            return false;
        }
        string json = File.ReadAllText(path);
        data = JsonUtility.FromJson<T>(json);
        return true;
    }
    
}
