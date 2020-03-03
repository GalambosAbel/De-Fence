using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputReciever : MonoBehaviour
{
    public string loadName;
    public string saveName;

	int loadMode = 0;
	public GameObject gameEndPanel;

	InputManager inputs;

	public static InputReciever instance;

	void Awake()
	{
		if (instance == null) instance = this;
		else
		{
			Destroy(instance.gameObject);
			instance = this;
		}
		inputs = new InputManager();
		inputs.Menu.Enable();
		inputs.Gameplay.Disable();

		inputs.Gameplay.ConfirmTurn.performed += ctx => AbstractManager.instance._controlButton.onClick.Invoke();

		/*inputs.Menu.Save.performed += ctx => gameObject.GetComponent<BoardSaver>().Save(saveName);
		inputs.Menu.Load.performed += ctx => gameObject.GetComponent<BoardLoader>().LoadBoard(loadName);
		inputs.Menu.GenerateBoard.performed += ctx => gameObject.GetComponent<GenerateBoard>().GenerateBoardFc();*/

		SceneManager.sceneLoaded += LoadedScene;

		DontDestroyOnLoad(gameObject);
	}

	public void GameEnded()
	{
		gameEndPanel.SetActive(true);

		inputs.Menu.Enable();
		inputs.Gameplay.Disable();

		if (AbstractManager.LeadingPlayer.Length == 1)
		{
			int winner = AbstractManager.LeadingPlayer[0];
			GameObject.Find("VictoryText").GetComponent<Text>().text = "Player " + winner + " won!";
			GameObject.Find("VictoryText").GetComponent<Text>().color = AbstractManager.playerColors[winner];
		}
		else
		{
			GameObject.Find("VictoryText").GetComponent<Text>().text = "Players " + ArrToString(AbstractManager.LeadingPlayer) + " have tied.";
			GameObject.Find("VictoryText").GetComponent<Text>().color = Color.black;
		}
	}

	static string ArrToString(int[] arr)
	{
		string returnString = "";
		if (arr.Length == 0) return returnString;
		returnString += arr[0];
		for (int i = 1; i < arr.Length; i++)
		{
			returnString += (i == arr.Length - 1) ? " and " : ", ";
			returnString += arr[i];
		}
		return returnString;
	}

	public void ReturnToMenu()
	{
		SceneManager.LoadScene("MenuScene");
	}

	public void NewGame()
	{
		loadMode = 0;
		SceneManager.LoadScene("GameScene");
	}

	public void LoadedScene(Scene scene, LoadSceneMode mode)
	{
		if (this == null) return;
		gameEndPanel.SetActive(false);
		if (scene.name != "GameScene") return;

		inputs.Menu.Disable();
		inputs.Gameplay.Enable();

		// assigning lost references
		GameObject tP = GameObject.Find("Tiles");
		GameObject wP = GameObject.Find("Walls");
		gameObject.GetComponent<BoardSaver>().tileParent = tP;
		gameObject.GetComponent<BoardLoader>().tileParent = tP;
		gameObject.GetComponent<GenerateBoard>().tileParent = tP.transform;
		gameObject.GetComponent<BoardSaver>().wallParent = wP;
		gameObject.GetComponent<BoardLoader>().wallParent = wP;
		gameObject.GetComponent<GenerateBoard>().wallParent = wP.transform;

		//what to load
		if(loadMode == 0)
		{
			gameObject.GetComponent<GenerateBoard>().GenerateBoardFc();
		}
		else if(loadMode == 1)
		{
			gameObject.GetComponent<BoardLoader>().LoadBoard(loadName);
		}
	}
}
