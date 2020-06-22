using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class OnlineManager : MonoBehaviourPunCallbacks
{
	[HideInInspector]
	public bool isOnline = false;
	[HideInInspector]
	public int playerNumber;

	private string roomName = "f";

	public static OnlineManager instance;

	void Awake()
	{
		if (instance == this) Destroy(gameObject.GetComponent<PhotonView>());
	}

	void Start()
	{
		isOnline = false;
		playerNumber = 0;
		instance = this;
	}

	public void SetRoomName(string newName)
	{
		roomName = newName;
	}

	#region ConectingToNet

	public void ConnectToInternet()
	{
		GameObject.Find("StatusText").GetComponent<Text>().text = "Connecting to internet";
		PhotonNetwork.GameVersion = Application.version;
		PhotonNetwork.ConnectUsingSettings();
	}

	public override void OnConnectedToMaster()
	{
		isOnline = true;
		CreateRoom();
	}

	public void CreateRoom()
	{
		playerNumber = 1;
		RoomOptions options = new RoomOptions() { IsVisible = false, MaxPlayers = 2 };
		PhotonNetwork.CreateRoom(roomName, options);
	}

	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		playerNumber = 2;
		PhotonNetwork.JoinRoom(roomName);
	}

	public override void OnJoinedRoom()
	{
		GameObject.Find("StatusText").GetComponent<Text>().text = "Waiting for other player to join";
	}

	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
		{
			if (InputReciever.instance.stateName == "Starting_Default")
			{
				photonView.RPC("StartGame", RpcTarget.All, "Starting_Default", JsonUtility.ToJson(new Options(false)));
				return;
			}
			string json = File.ReadAllText(SaveFileManager.SaveStatefolder + InputReciever.instance.stateName);
			photonView.RPC("StartGame", RpcTarget.All, json, JsonUtility.ToJson(new Options(true)));
		}
	}

	public override void OnPlayerLeftRoom(Player otherPlayer)
	{
		Disconnect();
	}

	[PunRPC]
	public void StartGame(string gameName, string options)
	{
		InputReciever.instance.stateName = gameName;
		Options o = JsonUtility.FromJson<Options>(options);
		o.ApplyOptions();
		GetComponent<InputReciever>().NewGame(o.isJson);
	}

	public void Disconnect()
	{
		PhotonNetwork.Disconnect();
		isOnline = false;
	}

	#endregion

	#region Inputs

	public void Rematch()
	{
		if (isOnline) photonView.RPC("RematchRPC", RpcTarget.All);
		else RematchRPC();
	}

	[PunRPC]
	public void RematchRPC() 
	{
		InputReciever.instance.stateName = "Starting_Default";
		GetComponent<InputReciever>().NewGame(false); 
	}

	public void CtrlButton() => InputRecived(PlayerAction.ControllButtonPressed);

	public void InputRecived(PlayerAction action, int tileId = 0)
	{
		if (isOnline && playerNumber == GameMaster.am.currentPlayer)
		{
			photonView.RPC("DoInput", RpcTarget.All, action, tileId);
		}
		else if(!isOnline)
		{
			DoInput(action, tileId);
		}
	}

	[PunRPC]
	public void DoInput(PlayerAction action, int tileId)
	{
		switch (action)
		{
			case PlayerAction.TileClicked:
				GameMaster.am.ClickTile(tileId);
				GameMaster.UpdateControlButton();
				break;
			case PlayerAction.ControllButtonPressed:
				if (GameMaster.am.IsValidStep())
				{ 
					GameMaster.StepTaken();
					GameMaster.clock.AddTime();
				}
				GameMaster.am.TakeStep();
				GameMaster.UpdateControlButton();
				break;
			default:
				break;
		}
	}

#endregion
}

public enum PlayerAction
{
	TileClicked, ControllButtonPressed
}

[Serializable]
public struct Options
{
	public bool isJson;
	
	public bool clockEnabled;
	public int startingMin;
	public int startingSec;
	public int timeToAdd;

	public bool showScore;
	public bool displayLastStep;

	public Options(bool json)
	{
		isJson = json;

		clockEnabled = GameMaster.clockEnabled;
		startingMin = ChessClock.startingMin;
		startingSec = ChessClock.startingSec;
		timeToAdd = ChessClock.secToAdd;
		showScore = GameMaster.showScores;
		displayLastStep = GameMaster.displayLastStep;
	}

	public void ApplyOptions()
	{
		GameMaster.clockEnabled = clockEnabled;
		ChessClock.startingMin = startingMin;
		ChessClock.startingSec = startingSec;
		ChessClock.secToAdd = timeToAdd;
		GameMaster.showScores = showScore;
		GameMaster.displayLastStep = displayLastStep;
	}
}