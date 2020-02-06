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
	}

	void OnEnable()
	{
		inputs.Enable();
	}

	void OnDisable()
	{
		inputs.Disable();
	}

	void Update()
	{
		/*if (Input.GetKeyDown(KeyCode.G))
		{
			gameObject.GetComponent<GenerateBoard>().GenerateBoardFc();
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			gameObject.GetComponent<BoardSaver>().Save(saveName);
		}
		if (Input.GetKeyDown(KeyCode.L))
		{
			gameObject.GetComponent<BoardLoader>().LoadBoard(loadName);
		}*/
	}
}
