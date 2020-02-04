﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BoardLoader : MonoBehaviour
{
    string[] allLines;
    int currentLineIndex = 0;

    public GameObject tile;
    public GameObject wall;

    public GameObject tileParent;
    public GameObject wallParent;

	List<AbstractTile> abstractTiles;
	List<AbstractWall> abstractWalls;

    public void LoadBoard(string inputFileName)
    {
        Debug.Log("Loading");
        if (!File.Exists(inputFileName))
        {
            Debug.Log("Couldn't load file: " + inputFileName);
            return;
        }

        allLines = File.ReadAllLines(inputFileName);
        int tileAmount = ReadInt();
        int wallAmount = ReadInt();

		abstractTiles = new List<AbstractTile>();
		abstractWalls = new List<AbstractWall>();

        LoadTiles(tileAmount);
        LoadWalls(wallAmount);

		AbstarctManager.board = new AbstractBoard(abstractTiles, abstractWalls);
        Debug.Log("Loaded file: " + inputFileName);
    }

    void LoadTiles (int tileAmount)
    {
        for (int i = 0; i < tileAmount; i++)
        {
            LoadTile();
        }
    }

    void LoadWalls(int wallAmount)
    {
        for (int i = 0; i < wallAmount; i++)
        {
            LoadWall();
        }
    }

    void LoadTile()
	{
		int ID = ReadInt();
        float x = ReadFloat();
        float y = ReadFloat();
        float rotZ = ReadFloat();
        int neighbourAmount = ReadInt();
        TileData data = new TileData();

        data.ID = ID;

		List<int[]> ns = new List<int[]>();
        for (int i = 0; i < neighbourAmount; i++)
        {
			int n = ReadInt();
			int w = ReadInt();
            data.neighbours.Add(n);
			ns.Add(new int[] { n, w });
        }

		abstractTiles.Add(new AbstractTile(ID, ns));
        Instantiate(tile, new Vector3(x, y, 0), Quaternion.Euler(0, 0, rotZ), tileParent.transform).GetComponent<Tile>().data = data;
    }

    void LoadWall()
    {
		int ID = ReadInt();
        float x = ReadFloat();
        float y = ReadFloat();
        float rotZ = ReadFloat();

		Instantiate(wall, new Vector3(x, y, 0), Quaternion.Euler(0, 0, rotZ), wallParent.transform).GetComponent<Wall>().ID = ID;
		abstractWalls.Add(new AbstractWall(ID));
    }

    int ReadInt ()
    {
        int value = int.Parse(allLines[currentLineIndex]);
        currentLineIndex++;
        return value;
    }

    float ReadFloat ()
    {
        float value = float.Parse(allLines[currentLineIndex]);
        currentLineIndex++;
        return value;
    }
}
