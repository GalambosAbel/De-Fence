using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BoardLoader : MonoBehaviour
{
    public static string inputFileName;
    int tileAmount;
    int wallAmount;
    int currentLineIndex = 0;
    string[] allLines;

    GameObject tile;
    GameObject wall;

    void Start()
    {
        allLines = File.ReadAllLines(inputFileName);
        tileAmount = ReadInt();
        wallAmount = ReadInt();

        LoadTiles(tileAmount);
        LoadWalls(wallAmount);
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
        float x = ReadFloat();
        float y = ReadFloat();
        float rotZ = ReadFloat();
        int neighbourAmount = ReadInt();

        TileData tmp = Instantiate(tile, new Vector3(x, y, 0), Quaternion.Euler(0, 0, rotZ)).GetComponent<TileData>();

        for (int j = 0; j < neighbourAmount; j++)
        {
            tmp.neighbours.Add(ReadInt());
        }
    }

    void LoadWall()
    {
        float x = ReadFloat();
        float y = ReadFloat();
        float rotZ = ReadFloat();

        Wall tmp = Instantiate(wall, new Vector3(x, y, 0), Quaternion.Euler(0, 0, rotZ)).GetComponent<Wall>();

        tmp.neighbour1 = ReadInt();
        tmp.neighbour2 = ReadInt();
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
