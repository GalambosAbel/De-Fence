using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveFileDisplayer : MonoBehaviour
{
    string fileName;

    public void Create(string _name, DateTime date)
    {
        fileName = _name;
        transform.GetChild(0).GetComponent<Text>().text = fileName;
        transform.GetChild(1).GetComponent<Text>().text = date.ToString();
        transform.GetChild(2).GetComponent<Button>().onClick.AddListener(Delete);

        gameObject.GetComponent<Button>().onClick.AddListener(Open);   
    }

    void Open()
    {
        GameObject.Find("BoardGenerator").GetComponent<InputReciever>().NewGame(fileName);
    }

    void Delete()
    {
        File.Delete(SaveFileManager.SaveStatefolder + fileName);
        Destroy(gameObject);
    }
}
