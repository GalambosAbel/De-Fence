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

		AbstractManager.board = new AbstractBoard(abstractTiles, abstractWalls);
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

		List<int[]> neighbours = new List<int[]>();
        for (int i = 0; i < neighbourAmount; i++)
        {
			int neighbour = ReadInt();
			int wall = ReadInt();
			neighbours.Add(new int[] { neighbour, wall });
        }

        Instantiate(tile, new Vector3(x, y, 0), Quaternion.Euler(0, 0, rotZ), tileParent.transform).GetComponent<Tile>().ID = ID;
		abstractTiles.Add(new AbstractTile(ID, neighbours));
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
