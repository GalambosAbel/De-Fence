using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int ID;

	InputManager inputs;

	void Awake()
	{
		inputs = new InputManager();
		inputs.Disable();

		inputs.Tile.Click.performed += ctx => AbstractManager.board.tiles[ID].ClickedTile();
	}


	void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().color = AbstractManager.playerColors[AbstractManager.board.tiles[ID].owner];
		transform.GetChild(0).gameObject.SetActive(AbstractManager.board.tiles[ID].hasFigure);
    }

	void OnMouseEnter()
	{
		inputs.Enable();
	}

	void OnMouseExit()
	{
		inputs.Disable();
	}
}
//distanceOffset = 2f  angleOffset = 60f