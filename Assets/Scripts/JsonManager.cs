using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class JsonManager : MonoBehaviour
{
    public static void SaveState(string outputFileName)
	{
		if (File.Exists(outputFileName))
		{
			int nameFix = 0;
			while (File.Exists(outputFileName + nameFix.ToString()))
			{
				nameFix++;
			}
			outputFileName += nameFix.ToString();
		}
		TestJSON test = new TestJSON();
		test.string1 = "aa";
		test.string2 = "aa";
		test.bigyo1 = new Bigyo();
		test.bigyo1.i1 = 5;
		test.bigyo1.string3 = "cc";

		string json = JsonUtility.ToJson(test);
		Debug.Log(json);
	}

	public static void SaveMap(string outputFileName)
	{

	}

	public static void LoadState(string stateName)
	{

	}

	public static void LoadMap(string mapName)
	{

	}
}

[Serializable]
public class TestJSON
{
	public string string1;
	public string string2;
	public Bigyo bigyo1;
}

public class Bigyo
{
	public int i1;
	public string string3;
}