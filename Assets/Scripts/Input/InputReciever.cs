using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReciever : MonoBehaviour
{
    public string loadName;
    public string saveName;

	InputManager inputs;

	void Awake()
	{
		inputs = new InputManager();
		inputs.Enable();

		inputs.Menu.Save.performed += ctx => gameObject.GetComponent<BoardSaver>().Save(saveName);
		inputs.Menu.Load.performed += ctx => gameObject.GetComponent<BoardLoader>().LoadBoard(loadName);
		inputs.Menu.GenerateBoard.performed += ctx => gameObject.GetComponent<GenerateBoard>().GenerateBoardFc();
		inputs.Gameplay.ConfirmTurn.performed += ctx => AbstractManager.instance._controlButton.onClick.Invoke();
	}

	void OnEnable()
	{
		inputs.Enable();
	}

	void OnDisable()
	{
		inputs.Disable();
	}
}
