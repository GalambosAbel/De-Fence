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
		inputs.Gameplay.QuickSave.performed += ctx => JsonManager.SaveState("Quicksave");
		inputs.Gameplay.Pause.performed += ctx => PauseResume(true);

		inputs.Menu.Resume.performed += ctx => PauseResume(false);

		SceneManager.sceneLoaded += LoadedScene;

		DontDestroyOnLoad(gameObject);
	}

	void Pause()
	{
		PauseResume(true);
	}
	void Resume()
	{
		PauseResume(false);
	}

	public void PauseResume(bool pause)
	{
		GameMaster.paused = pause;
		GameObject temp = GameObject.Find("SavePanel");
		if (temp != null) temp.SetActive(false);
		pauseMenu.SetActive(pause);
		GameObject.Find("PauseButton").GetComponent<Button>().interactable = !pause;
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

	public void SaveAs()
	{
		string saveName = GameObject.Find("SaveName").GetComponent<InputField>().text;
		if (saveName == "") saveName = "Quicksave";
		if (saveName == "Starting_Default") saveName += ".Nice_Try";
		JsonManager.SaveState(saveName);
		GameObject.Find("SavePanel").SetActive(false);
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

		GameObject.Find("ResumeButton").GetComponent<Button>().onClick.AddListener(Resume);
		GameObject.Find("PauseButton").GetComponent<Button>().onClick.AddListener(Pause);
		GameObject.Find("MenuButton").GetComponent<Button>().onClick.AddListener(ReturnToMenu);
		GameObject.Find("SaveButton").GetComponent<Button>().onClick.AddListener(SaveAs);

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

		GameMaster.UpdateControlButton();
	}
}
