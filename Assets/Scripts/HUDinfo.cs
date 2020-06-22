using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore;

public class HUDinfo : MonoBehaviour
{
	public Text currentPlayerText;
	public Text playersText;
	string[] playerTexts;

	private void Awake()
	{
		playerTexts = new string[]
		{
			"<color=#" + ColorUtility.ToHtmlStringRGB(GameMaster.playerColors[1]) + "> Player 1 </color>",
			"<color=#" + ColorUtility.ToHtmlStringRGB(GameMaster.playerColors[2]) + "> Player 2 </color>"
		};
	}

	void Update()
	{
		currentPlayerText.text = "Current player is " + playerTexts[GameMaster.am.currentPlayer-1];
		if (!OnlineManager.instance.isOnline)
		{
			string player1Text = "<color=#" + ColorUtility.ToHtmlStringRGB(GameMaster.playerColors[1]) + ">This is Player 1's color</color>, ";
			string player2Text = "<color=#" + ColorUtility.ToHtmlStringRGB(GameMaster.playerColors[2]) + ">This is Player 2's color</color>.";
			playersText.text = player1Text + player2Text;
		}
		else
		{
			playersText.text = "You are " + playerTexts[OnlineManager.instance.playerNumber - 1];
		}
	}
}
