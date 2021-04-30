using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem 
{
    private static SaveSystem instance = new SaveSystem();
    public static SaveSystem Instance => instance;

    private SaveSystem() { Load(); }

    public string Path => Application.dataPath + "/data.json";
  
    public UserData UserData { get; private set; }　

    public void Save()
    {
        string jsonData = JsonUtility.ToJson(UserData);
        StreamWriter writer = new StreamWriter(Path, false);
        writer.WriteLine(jsonData);
        writer.Flush();
        writer.Close();
    }

    public void Load()
    {
        if (!File.Exists(Path))
        {
            Debug.Log("初回起動");
            UserData = new UserData();
            Save();
            return;
        }
        StreamReader reader = new StreamReader(Path);
        string jsonData = reader.ReadToEnd();
        UserData = JsonUtility.FromJson<UserData>(jsonData);
        reader.Close();
    }
}
