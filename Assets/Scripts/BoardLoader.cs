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

        for (int i = 0; i < tileAmount; i++)
        {
            float x;
            float y;
            float rotZ;
            int neighbourAmount;

            x = ReadFloat();
            y = ReadFloat();
            rotZ = ReadFloat();
            neighbourAmount = ReadInt();

            TileData tmp = Instantiate(tile, new Vector3(x, y, 0), Quaternion.Euler(0, 0, rotZ)).GetComponent<TileData>();

            for (int j = 0; j < neighbourAmount; j++)
            {
                tmp.neighbours.Add(ReadInt());
            }
        }

        for (int i = 0; i < wallAmount; i++)
        {
            float x;
            float y;
            float rotZ;
            int neighbour1;
            int neighbour2;

            x = ReadFloat();
            y = ReadFloat();
            rotZ = ReadFloat();

            Wall tmp = Instantiate(wall, new Vector3(x, y, 0), Quaternion.Euler(0, 0, rotZ)).GetComponent<Wall>();

            tmp.neighbour1 = ReadInt();
            tmp.neighbour2 = ReadInt();
        }
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
