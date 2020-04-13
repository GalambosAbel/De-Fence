using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DeFenceAbstract;

public class GameMaster : MonoBehaviour
{
    public static AbstractManager am;

	public static string currentMap;

	public static Color[] playerColors = new Color[] { Color.white, Color.red, Color.blue, Color.green, Color.black };
	public static bool gameEnded;
	public static bool paused;

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

	public GameObject contentBox;
	public GameObject displayerPrefab;

	void Awake()
	{
		am = new AbstractManager(GameEnded);

		tilePrefab = _tilePrefab;
		wallPrefab = _wallPrefab;

		gameEnded = false;
		SaveFileManager.Setup(contentBox, displayerPrefab);
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

	public static void GameEnded(GameEndArgs e)
	{
		gameEnded = true;
		paused = true;
		GameObject.Find("BoardGenerator").GetComponent<InputReciever>().GameEnded(am.LeadingPlayer);
	}
}
