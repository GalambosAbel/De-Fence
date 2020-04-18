using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineManager : MonoBehaviourPunCallbacks
{
	[HideInInspector]
	public bool isOnline = false;

	public static OnlineManager instance;

	public void Start()
	{
		instance = this;
	}

	#region ConectingToNet

	public void ConnectToInternet()
	{
		PhotonNetwork.GameVersion = StateUpgrader.Version.ToString();
		PhotonNetwork.ConnectUsingSettings();
	}

	public override void OnConnectedToMaster()
	{
		Debug.Log("connected");
		isOnline = true;
	}

	public void CreateRoom()
	{
		string roomName = "a";
		RoomOptions options = new RoomOptions() { IsVisible = false, MaxPlayers = 2 };
		PhotonNetwork.CreateRoom(roomName, options);
	}

	#endregion

	#region Inputs

	public void Pause() => InputRecived(PlayerAction.Pause);

	public void Resume() => InputRecived(PlayerAction.Resume);
	public void CtrlButton() => InputRecived(PlayerAction.ControllButtonPressed);

	public void InputRecived(PlayerAction action, int tileId = 0)
	{
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