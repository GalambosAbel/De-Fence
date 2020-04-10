using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputReciever : MonoBehaviour
{
    public string stateName;
    public string mapName;

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

		inputs.Gameplay.ConfirmTurn.performed += ctx => GameMaster.controlButton.onClick.Invoke();

		inputs.Gameplay.temp.performed += ctx => JsonManager.SaveMap(stateName);
		inputs.Gameplay.templ.performed += ctx => JsonManager.LoadState(stateName);

		inputs.Menu.Save.performed += ctx => JsonManager.SaveState(stateName);
		/*inputs.Menu.Load.performed += ctx => gameObject.GetComponent<BoardLoader>().LoadBoard(loadName);
		inputs.Menu.GenerateBoard.performed += ctx => gameObject.GetComponent<GenerateBoard>().GenerateBoardFc();*/

		SceneManager.sceneLoaded += LoadedScene;

		DontDestroyOnLoad(gameObject);
	}

	public void GameEnded(int[] winners)
	{
		gameEndPanel.SetActive(true);

		inputs.Menu.Enable();
		inputs.Gameplay.Disable();

		if (winners.Length == 1)
		{
            int winner = winners[0];
			GameObject.Find("VictoryText").GetComponent<Text>().text = "Player " + winner + " won!";
			GameObject.Find("VictoryText").GetComponent<Text>().color = GameMaster.playerColors[winner];
		}
		else
		{
			GameObject.Find("VictoryText").GetComponent<Text>().text = "Players " + ArrToString(winners) + " have tied.";
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

	public void QuitGame()
	{
		Application.Quit();
	}

	public void NewGame()
	{
		loadMode = 0;
		SceneManager.LoadScene("GameScene");
	}

    public void Tutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }

	public void LoadedScene(Scene scene, LoadSceneMode mode)
	{
		if (this == null) return;
		gameEndPanel.SetActive(false);
		if (scene.name != "GameScene") return;

		inputs.Menu.Disable();
		inputs.Gameplay.Enable();

		// assigning lost references
		GameMaster.controlButton = GameObject.Find("ControlButton").GetComponent<Button>();
		GameMaster.controlButton.onClick.AddListener(GameMaster.am.TakeStep);
		GameMaster.controlButton.onClick.AddListener(GameMaster.UpdateControlButton);
		GameMaster.UpdateControlButton();

		GameMaster.tileParent = GameObject.Find("Tiles").transform;
		GameMaster.wallParent = GameObject.Find("Walls").transform;

		//what to load
		if(loadMode == 0)
		{
			gameObject.GetComponent<GenerateBoard>().GenerateBoardFc();
		}
		else if(loadMode == 1)
		{
			JsonManager.LoadMap(mapName);
			JsonManager.LoadState(stateName);
		}
	}
}
