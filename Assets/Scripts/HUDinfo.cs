using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDinfo : MonoBehaviour
{
	public Text currentPlayerText;
	public Text playersText;
	string[] playerTexts = new string[]
		{
			"<color=red> Player 1 </color>",
			"<color=blue> Player 2 </color>",
		};

	void Start()
	{
		if (!OnlineManager.instance.isOnline)
		{
			string player1Text = "<color=red>Player 1's color is red</color>, ";
			string player2Text = "<color=blue>Player 2's color is blue</color>.";
			playersText.text = player1Text + player2Text;
		}
		else
		{
			playersText.text = "You are " + playerTexts[OnlineManager.instance.playerNumber-1];
		}
	}

	void Update()
	{
		currentPlayerText.text = "Current player is " + playerTexts[GameMaster.am.currentPlayer-1];
	}
}
