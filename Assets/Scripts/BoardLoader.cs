using System.Collections;
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

    public void LoadBoard(string inputFileName)
    {
        Debug.Log("Loading");

        allLines = File.ReadAllLines(inputFileName);
        int tileAmount = ReadInt();
        int wallAmount = ReadInt();

        LoadTiles(tileAmount);
        LoadWalls(wallAmount);

        Debug.Log("Load complete");
    }

    void LoadTiles (int tileAmount)
    {
        for (int i = 0; i < tileAmount; i++)
        {
            LoadTile(i);
        }
    }

    void LoadWalls(int wallAmount)
    {
        for (int i = 0; i < wallAmount; i++)
        {
            LoadWall(i);
        }
    }

    void LoadTile(int i)
    {
        float x = ReadFloat();
        float y = ReadFloat();
        float rotZ = ReadFloat();
        int neighbourAmount = ReadInt();
        TileData data = new TileData();

        data.ID = i;

        for (int j = 0; j < neighbourAmount; j++)
        {
            data.neighbours.Add(ReadInt());
        }

        Instantiate(tile, new Vector3(x, y, 0), Quaternion.Euler(0, 0, rotZ), tileParent.transform).GetComponent<Tile>().data = data;
    }

    void LoadWall(int i)
    {
        float x = ReadFloat();
        float y = ReadFloat();
        float rotZ = ReadFloat();

        Wall tmp = Instantiate(wall, new Vector3(x, y, 0), Quaternion.Euler(0, 0, rotZ), wallParent.transform).GetComponent<Wall>();

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
