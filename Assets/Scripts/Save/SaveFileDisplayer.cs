using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveFileDisplayer : MonoBehaviour
{
    public static List<SaveFileDisplayer> allSaves = new List<SaveFileDisplayer>();
    public static SaveFileDisplayer currentSave;


    string fileName;

    public void Create(string _name, DateTime date)
    {
        fileName = _name;
        transform.GetChild(0).GetComponent<Text>().text = fileName;
        transform.GetChild(1).GetComponent<Text>().text = date.ToString();
        transform.GetChild(2).GetComponent<Button>().onClick.AddListener(Delete);

        gameObject.GetComponent<Button>().onClick.AddListener(Select);
    }

    void Start()
    {
        allSaves.Add(this);
        CheckIfDefault();
    }

    public void CheckIfDefault()
    {
        if (fileName != "Starting_Default") return;
        if (transform.childCount > 2) Destroy(transform.GetChild(2).gameObject);
        transform.GetChild(0).GetComponent<Text>().text = "New Game";
        Select();
    }

    void Select()
    {
        if(currentSave == this && fileName != "Starting_Default")
        {
            UnSelect();
            foreach (SaveFileDisplayer sd in allSaves)
            {
                sd.CheckIfDefault();
            }
            return;
        }

        foreach (SaveFileDisplayer sd in allSaves)
        {
            sd.UnSelect();
        }
        currentSave = this;
        GetComponent<Image>().color = new Color(87f / 255f, 87f / 255f, 87f / 255f, 137f / 255f);
        InputReciever.instance.stateName = fileName;
    }

    void UnSelect()
    {
        GetComponent<Image>().color = new Color(1f, 1f, 1f, 137f / 255f);
    }

    void Delete()
    {
        File.Delete(SaveFileManager.SaveStatefolder + fileName);
        allSaves.Remove(this);
        if (currentSave == this) Select();
        Destroy(gameObject);
    }
}
