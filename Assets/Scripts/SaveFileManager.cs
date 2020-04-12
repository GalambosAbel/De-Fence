using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveFileManager : MonoBehaviour
{
    public static string SaveStatefolder
    {
        get
        {
            return Application.persistentDataPath + "/Saves/";
        }
    }
    public static string SaveMapfolder
    {
        get
        {
            return Application.persistentDataPath + "/Maps/";
        }
    }

    public static void Setup()
    {
        if (!Directory.Exists(SaveStatefolder))
        {
            Directory.CreateDirectory(SaveStatefolder);
        }
        if (!Directory.Exists(SaveMapfolder))
        {
            Directory.CreateDirectory(SaveMapfolder);
        }
    }

    public static void LogAllSaves()
    {
        foreach (string file in Directory.GetFiles(SaveStatefolder))
        {
            Debug.Log(file);
        }
    }
    public static void LogAllMaps()
    {
        foreach (string file in Directory.GetFiles(SaveMapfolder))
        {
            Debug.Log(file);
        }
    }
}
