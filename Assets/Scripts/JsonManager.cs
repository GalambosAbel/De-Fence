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
		State state = JsonUtility.FromJson<State>(json);

		if (state.mapName != GameMaster.currentMap)
		{
			LoadMap(state.mapName);
		}

		GameMaster.am.playerAmount = state.playerAmount;
		GameMaster.am.currentPlayer = state.currentPlayer;

		for (int i = 0; i < state.tiles.Count; i++)
		{
			GameMaster.am.board.tiles[i].owner = state.tiles[i].owner;
			GameMaster.am.board.tiles[i].hasFigure = state.tiles[i].hasFigure;
		}
		for (int i = 0; i < state.walls.Count; i++)
		{
			GameMaster.am.board.walls[i].active = state.walls[i].active;
		}
		GameMaster.UpdateControlButton();
		GameMaster.am.board.LoadTerritorries();
	}

	public static void SaveMap()
	{
		string outputFileName = GameMaster.currentMap;
		if (File.Exists(outputFileName))
		{
			int nameFix = 0;
			while (File.Exists(outputFileName + nameFix.ToString()))
			{
				nameFix++;
			}
			outputFileName += nameFix.ToString();
		}

		Map map = new Map(GameMaster.am.board, GameMaster.tiles, GameMaster.walls);

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

		string json = File.ReadAllText(inputFileName);

		Map map = JsonUtility.FromJson<Map>(json);

		GameMaster.am.board.tiles = new List<AbstractTile>();
		GameMaster.tiles = new List<GameObject>();
		foreach (MapTile t in map.tiles)
		{
			Vector3 pos = new Vector3(t.position.x, t.position.y, 0);
			GameObject tile = Instantiate(GameMaster.tilePrefab, pos, Quaternion.Euler(0, 0, t.position.rotation), GameMaster.tileParent);
			tile.GetComponent<Tile>().ID = t.ID;
			GameMaster.tiles.Add(tile);

			AbstractTile aTile = new AbstractTile(t.ID, GameMaster.am);
			foreach (Neighbour n in t.neighbours)
			{
				aTile.neighbours.Add(new int[] { n.tile, n.wall });
			}
			GameMaster.am.board.tiles.Add(aTile);
		}
		GameMaster.am.board.walls = new List<AbstractWall>();
		GameMaster.walls = new List<GameObject>();
		foreach (MapWall w in map.walls)
		{
			Vector3 pos = new Vector3(w.position.x, w.position.y, 0);
			GameObject wall = Instantiate(GameMaster.wallPrefab, pos, Quaternion.Euler(0, 0, w.position.rotation), GameMaster.wallParent);
			wall.GetComponent<Tile>().ID = w.ID;
			GameMaster.tiles.Add(wall);

			GameMaster.am.board.walls.Add(new AbstractWall(w.ID));
		}
		GameMaster.currentMap = inputFileName;
	}
}



[Serializable]
public struct State
{
	public string mapName;
	public int playerAmount;
	public int currentPlayer;
	public List<StateTile> tiles;
	public List<StateWall> walls;

	public State (AbstractManager am)
	{
		mapName = GameMaster.currentMap;
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
		for (int i = 0; i < _tiles.Count; i++)
		{
			tiles.Add(new MapTile(board.tiles[i], _tiles[i].GetComponent<Tile>()));
		}
		walls = new List<MapWall>();
		for (int i = 0; i < _walls.Count; i++)
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