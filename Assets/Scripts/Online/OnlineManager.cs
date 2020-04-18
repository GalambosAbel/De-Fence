using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlineManager : MonoBehaviourPunCallbacks
{
	[HideInInspector]
	public bool isOnline = false;
	[HideInInspector]
	public int playernumber;

	private string roomName = "f";

	public static OnlineManager instance;

	public void Start()
	{
		isOnline = false;
		playernumber = 0;
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
		PhotonNetwork.GameVersion = StateUpgrader.Version.ToString();
		PhotonNetwork.ConnectUsingSettings();
	}

	public override void OnConnectedToMaster()
	{
		isOnline = true;
		CreateRoom();
	}

	public void CreateRoom()
	{
		playernumber = 1;
		RoomOptions options = new RoomOptions() { IsVisible = false, MaxPlayers = 2 };
		PhotonNetwork.CreateRoom(roomName, options);
	}

	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		playernumber = 2;
		PhotonNetwork.JoinRoom(roomName);
	}

	public override void OnJoinedRoom()
	{
		GameObject.Find("StatusText").GetComponent<Text>().text = "Waiting for other player to join";
		if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
		{
			GetComponent<InputReciever>().NewGame("Starting_Default");
		}
	}

	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
		{
			GetComponent<InputReciever>().NewGame("Starting_Default");
		}
	}

	public void Cancel()
	{
		PhotonNetwork.Disconnect();
		isOnline = false;
	}

	#endregion

	#region Inputs

	public void Pause() => InputRecived(PlayerAction.Pause);

	public void Resume() => InputRecived(PlayerAction.Resume);
	public void CtrlButton() => InputRecived(PlayerAction.ControllButtonPressed);

	public void InputRecived(PlayerAction action, int tileId = 0)
	{
		if (isOnline && playernumber != GameMaster.am.currentPlayer) return;
		if (isOnline)
		{
			photonView.RPC("DoInput", RpcTarget.All, action, tileId);
		}
		else
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
				GameMaster.am.TakeStep();
				GameMaster.UpdateControlButton();
				GameMaster.clock.AddTime();
				break;
			case PlayerAction.Pause:
				GetComponent<InputReciever>().PauseResume(true);
				break;
			case PlayerAction.Resume:
				GetComponent<InputReciever>().PauseResume(false);
				break;

			default:
				break;
		}
	}

#endregion
}

public enum PlayerAction
{
	TileClicked, ControllButtonPressed, Pause, Resume
}