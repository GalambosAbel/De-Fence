﻿using System.Collections;
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
	public int neighbourAmount;
	public Neighbour[] neighbours;
}

[Serializable]
public struct MapWall
{
	public int ID;
	public ObjPos position;
}

[Serializable]
public struct ObjPos
{
	public float x;
	public float y;
	public float rotation;
}

[Serializable]
public struct Neighbour
{
	public int tile;
	public int wall;
}