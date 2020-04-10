using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DeFenceAbstract;

public class GameMaster : MonoBehaviour
{
    public static AbstractManager am;

	public static Color[] playerColors = new Color[] { Color.white, Color.red, Color.blue, Color.green, Color.black };
	public static bool gameEnded;

	public static Button controlButton;
	public Button _controlButton;


	void Awake()
	{
		am = new AbstractManager(GameEnded);
		controlButton = _controlButton;
		controlButton.onClick.AddListener(am.TakeStep);
		controlButton.onClick.AddListener(UpdateControlButton);

		gameEnded = false;
		UpdateControlButton();
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
		GameObject.Find("BoardGenerator").GetComponent<InputReciever>().GameEnded(am.LeadingPlayer);
	}
}
