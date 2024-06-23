using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public const string directory = "/SaveData/";
    public const string fileName = "SaveMath.json";
    public static void Save(SaveVariables sv)
    {
        string dir = Application.persistentDataPath + directory;

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        string json = JsonUtility.ToJson(sv);
        File.WriteAllText(dir + fileName, json);
    }
    public static SaveVariables Load()
    {
        string fullPath = Application.persistentDataPath + directory + fileName;
        SaveVariables sv = new SaveVariables();

        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            sv = JsonUtility.FromJson<SaveVariables>(json);
        }
        else
        {
            Debug.Log("Not");
        }
        return sv;
    }
}
