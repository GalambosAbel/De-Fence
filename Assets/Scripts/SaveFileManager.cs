using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

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

    public static void Setup(GameObject contentBox, GameObject displayerPrefab)
    {
        if (!Directory.Exists(SaveStatefolder))
        {
            Directory.CreateDirectory(SaveStatefolder);
        }
        if (!Directory.Exists(SaveMapfolder))
        {
            Directory.CreateDirectory(SaveMapfolder);
        }

        foreach (string n in Directory.GetFiles(SaveStatefolder))
        {
            if (n == SaveStatefolder + "Starting_Default") continue;
            DateTime d = File.GetCreationTime(n);
            Instantiate(displayerPrefab, contentBox.transform).GetComponent<SaveFileDisplayer>().Create(Path.GetFileName(n), d);
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
