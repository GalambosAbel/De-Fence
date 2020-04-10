using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using DeFenceAbstract;

public class JsonManager : MonoBehaviour
{
    public static void SaveState(string outputFileName)
	{
		Debug.Log("Saving!");
		if (File.Exists(outputFileName))
		{
			int nameFix = 0;
			while (File.Exists(outputFileName + nameFix.ToString()))
			{
				nameFix++;
			}
			outputFileName += nameFix.ToString();
		}

		State currentState = new State(GameMaster.am);

		string json = JsonUtility.ToJson(currentState);
		File.WriteAllText(outputFileName, json);
		Debug.Log("Saved!");
	}

	public static void LoadState(string inputFileName)
	{
		Debug.Log("Loading");
		if (!File.Exists(inputFileName))
		{
			Debug.Log("Couldn't load file: " + inputFileName);
			return;
		}

		string json = File.ReadAllText(inputFileName);
		State stateToLoad = JsonUtility.FromJson<State>(json);

		GameMaster.am.playerAmount = stateToLoad.playerAmount;
		GameMaster.am.currentPlayer = stateToLoad.currentPlayer;

		for (int i = 0; i < stateToLoad.tiles.Count; i++)
		{
			GameMaster.am.board.tiles[i].owner = stateToLoad.tiles[i].owner;
			GameMaster.am.board.tiles[i].hasFigure = stateToLoad.tiles[i].hasFigure;
		}
		for (int i = 0; i < stateToLoad.walls.Count; i++)
		{
			GameMaster.am.board.walls[i].active = stateToLoad.walls[i].active;
		}
		GameMaster.UpdateControlButton();
		GameMaster.am.board.LoadTerritorries();
	}

	public static void SaveMap(string outputFileName)
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

		GenerateBoard GB = GameObject.Find("BoardGenerator").GetComponent<GenerateBoard>();

		Map map = new Map(GameMaster.am.board, GB.tiles, GB.walls);

		string json = JsonUtility.ToJson(map);

		File.WriteAllText(outputFileName, json);
	}

	public static void LoadMap(string inputFileName)
	{
		Debug.Log("Loading");
		if (!File.Exists(inputFileName))
		{
			Debug.Log("Couldn't load file: " + inputFileName);
			return;
		}
	}
}



[Serializable]
public struct State
{
	public int playerAmount;
	public int currentPlayer;
	public List<StateTile> tiles;
	public List<StateWall> walls;

	public State (AbstractManager am)
	{
		playerAmount = am.playerAmount;
		currentPlayer = am.currentPlayer;
		tiles = new List<StateTile>();
		foreach (AbstractTile t in am.board.tiles)
		{
			tiles.Add(new StateTile(t));
		}
		walls = new List<StateWall>();
		foreach (AbstractWall w in am.board.walls)
		{
			walls.Add(new StateWall(w));
		}
	}
}

[Serializable]
public struct Map
{
	public List<MapTile> tiles;
	public List<MapWall> walls;

	public Map(AbstractBoard board, List<GameObject> _tiles, List<GameObject> _walls)
	{
		tiles = new List<MapTile>();
		for (int i = 0; i < tiles.Count; i++)
		{
			tiles.Add(new MapTile(board.tiles[i], _tiles[i].GetComponent<Tile>()));
		}
		walls = new List<MapWall>();
		for (int i = 0; i < tiles.Count; i++)
		{
			walls.Add(new MapWall(_walls[i].GetComponent<Wall>()));
		}
	}
}


[Serializable]
public struct StateTile
{
	public int ID;
	public int owner;
	public bool hasFigure;

	public StateTile(AbstractTile t)
	{
		ID = t.ID;
		owner = t.owner;
		hasFigure = t.hasFigure;
	}
}

[Serializable]
public struct StateWall
{
	public int ID;
	public bool active;

	public StateWall(AbstractWall w)
	{
		ID = w.ID;
		active = w.active;
	}
}

[Serializable]
public struct MapTile
{
	public int ID;
	public ObjPos position;
	public List<Neighbour> neighbours;

	public MapTile(AbstractTile at, Tile t)
	{
		ID = t.ID;
		position = new ObjPos(t.transform);
		neighbours = new List<Neighbour>();
		foreach (int[] n in at.neighbours)
		{
			neighbours.Add(new Neighbour(n));
		}
	}
}

[Serializable]
public struct MapWall
{
	public int ID;
	public ObjPos position;

	public MapWall(Wall w)
	{
		ID = w.ID;
		position = new ObjPos(w.transform);
	}
}

[Serializable]
public struct ObjPos
{
	public float x;
	public float y;
	public float rotation;
	public ObjPos (Transform t)
	{
		x = t.position.x;
		y = t.position.y;
		rotation = t.rotation.eulerAngles.z;
	}
}

[Serializable]
public struct Neighbour
{
	public int tile;
	public int wall;

	public Neighbour (int[] n)
	{
		tile = n[0];
		wall = n[1];
	}
}