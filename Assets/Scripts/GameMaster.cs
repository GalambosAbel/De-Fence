﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DeFenceAbstract;

public class GameMaster : MonoBehaviour
{
    public static AbstractManager am;

	public static string currentMap;

	public static Color[] playerColors = new Color[] { Color.white, Color.red, Color.blue};
	public static bool gameEnded;
	public static bool paused;

	public static bool clockEnabled;
	public static bool displayLastStep;
	public static bool showScores;

	public static Button controlButton;
	public static ChessClock clock;

	public GameObject _tilePrefab;
	public GameObject _wallPrefab;

	public static GameObject tilePrefab;
	public static GameObject wallPrefab;

	public static List<GameObject> tiles;
	public static List<GameObject> walls;

	public static Transform tileParent;
	public static Transform wallParent;

	public static List<int> tilesInLastStep;

	public GameObject contentBox;
	public GameObject displayerPrefab;

	void Awake()
	{
		am = new AbstractManager(GameEnded);

		tilePrefab = _tilePrefab;
		wallPrefab = _wallPrefab;

		gameEnded = false;

		clockEnabled = false;
		ChessClock.startingMin = 30;
		ChessClock.startingSec = 0;
		ChessClock.secToAdd = 8;

		playerColors[1] = Color.red;
		playerColors[2] = Color.blue;

		showScores = true;
		displayLastStep = true;

		SaveFileManager.Setup(contentBox, displayerPrefab);
		GameObject.Find("VersionText").GetComponent<Text>().text = "V " + Application.version;
	}

	public static void UpdateControlButton()
	{
		if (am.tilesClicked.Count == 0)
		{
			controlButton.GetComponentInChildren<Text>().text = "Pass";
		}
		else if (am.CanPlace())
		{
			controlButton.GetComponentInChildren<Text>().text = "Place";
		}
		else if (am.CanGroup())
		{
			controlButton.GetComponentInChildren<Text>().text = "Group";
		}
		else if (am.CanAttack())
		{
			controlButton.GetComponentInChildren<Text>().text = "Attack";
		}
		else
		{
			controlButton.GetComponentInChildren<Text>().text = "ResetTurn";
		}
		controlButton.GetComponent<Image>().color = playerColors[am.currentPlayer];
	}

	public static void StepTaken()
	{
		tilesInLastStep = new List<int>();
		foreach (AbstractTile t in am.tilesClicked)
		{
			tilesInLastStep.Add(t.ID);
		}
	}

	public static void PauseUpdate()
	{
		if(OnlineManager.instance.playerNumber == am.currentPlayer)
		{
			OnlineManager.instance.GetComponent<InputReciever>().PauseResume(GameObject.Find("PauseMenu") != null);
		}
	}

	public static void GameEnded(GameEndArgs e)
	{
		gameEnded = true;
		paused = true;
		GameObject.Find("BoardGenerator").GetComponent<InputReciever>().GameEnded(am.LeadingPlayer);
	}
}
