﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	public TileStates currentState;

	public TileState state = TileState.GetStateByID(0);

	public TileData data;

    void Update()
    {
		state = TileState.GetStateByID((int)currentState);
		gameObject.GetComponent<SpriteRenderer>().color = state.color;
		transform.GetChild(0).gameObject.SetActive(state.hasFigure);
    }
}

public enum TileStates
{
	empty, blue, red, blueFigure, redfigure 
}

public class TileState
{
	public Color color;
	public bool hasFigure;

	TileState(Color color, bool hasFigure)
	{
		this.color = color;
		this.hasFigure = hasFigure;
	}

	static TileState[] AllStates
	{
		get
		{
			TileState[] returnArray = new TileState[5];
			returnArray[0] = new TileState(Color.white, false);
			returnArray[1] = new TileState(Color.blue, false);
			returnArray[2] = new TileState(Color.red, false);
			returnArray[3] = new TileState(Color.blue, true);
			returnArray[4] = new TileState(Color.red, true);
			return returnArray;
		}
	}

	public static TileState GetStateByID(int ID)
	{
		return AllStates[ID];
	}
}

public class TileData
{
    public int ID;
	public Vector3 position = Vector3.zero;
	public float distanceOffset = 2f;
    public float angleOffset = 60f;

    public List<int> neighbours = new List<int>();

	public TileData (Vector3 position,float distanceOffset, float angleOffset, int ID)
	{
		this.position = position;
		this.distanceOffset = distanceOffset;
		this.angleOffset = angleOffset;
        this.ID = ID;
	}

	public TileData(TileData other)
	{
		position = other.position;
		distanceOffset = other.distanceOffset;
		angleOffset = other.angleOffset;
	}

    public TileData()
    {
        ID = 0;
        neighbours = new List<int>();
    }
}