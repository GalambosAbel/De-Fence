using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputReciever : MonoBehaviour
{
    public string stateName;

	public GameObject gameEndPanel;
	GameObject pauseMenu;

	InputManager inputs;

	public static InputReciever instance;

	void Awake()
	{
		if (instance == null) instance = this;
		else if (instance != this)
		{
			instance.inputs.Dispose();
			Destroy(instance.gameObject);
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else return;

		inputs = new InputManager();
		inputs.Menu.Disable();
		inputs.Gameplay.Disable();

		inputs.Gameplay.ConfirmTurn.performed += ctx => GameMaster.controlButton.onClick.Invoke();
		inputs.Gameplay.QuickSave.performed += ctx => JsonManager.SaveState("Quciksave");
		inputs.Gameplay.Pause.performed += ctx => PauseResume(true);

		inputs.Menu.Resume.performed += ctx => PauseResume(false);

		SceneManager.sceneLoaded += LoadedScene;

		DontDestroyOnLoad(gameObject);
	}

	public void PauseResume(bool pause)
	{
		GameMaster.paused = pause;
		pauseMenu.SetActive(pause);
		if (pause)
		{
			inputs.Gameplay.Disable();
			inputs.Menu.Enable();
		}
		else
		{
			inputs.Gameplay.Enable();
			inputs.Menu.Disable();
		}
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

	public void NewGame(string _stateName)
	{
		stateName = _stateName;
		SceneManager.LoadScene("GameScene");
	}

	public void Rematch()
	{
		NewGame("Starting_" + GameMaster.currentMap);
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
		GameMaster.currentMap = "";

		// assigning lost references
		pauseMenu = GameObject.Find("PauseMenu");
		ChessClock clock = GameObject.Find("ChessClock").GetComponent<ChessClock>();
		
		GameMaster.tileParent = GameObject.Find("Tiles").transform;
		GameMaster.wallParent = GameObject.Find("Walls").transform;
		GameMaster.controlButton = GameObject.Find("ControlButton").GetComponent<Button>();
		GameMaster.controlButton.onClick.RemoveAllListeners();
		GameMaster.controlButton.onClick.AddListener(GameMaster.am.TakeStep);
		GameMaster.controlButton.onClick.AddListener(GameMaster.UpdateControlButton);
		GameMaster.controlButton.onClick.AddListener(clock.AddTime);

		if (!JsonManager.LoadState(stateName))
		{
			gameObject.GetComponent<GenerateBoard>().GenerateBoardFc();
			JsonManager.SaveState("Starting_Default");
		}

		clock.StartStop(true);
		PauseResume(false);
		GameMaster.gameEnded = false;
		GameMaster.am.lastPassed = false;

		GameMaster.UpdateControlButton();
	}
}
