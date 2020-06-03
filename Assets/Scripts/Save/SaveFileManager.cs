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

        SaveFileDisplayer.allSaves = new List<SaveFileDisplayer>();
        SaveFileDisplayer.currentSave = null;

        foreach (string n in Directory.GetFiles(SaveStatefolder))
        {
            if (Path.GetFileName(n) != "Starting_Default") continue;
            DateTime d = File.GetCreationTime(n);
            GameObject go = Instantiate(displayerPrefab, contentBox.transform);
            go.GetComponent<SaveFileDisplayer>().Create(Path.GetFileName(n), d);
        }

        foreach (string n in Directory.GetFiles(SaveStatefolder))
        {
            if (Path.GetFileName(n) == "Starting_Default") continue;
            DateTime d = File.GetCreationTime(n);
            GameObject go = Instantiate(displayerPrefab, contentBox.transform);
            go.GetComponent<SaveFileDisplayer>().Create(Path.GetFileName(n), d);
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
