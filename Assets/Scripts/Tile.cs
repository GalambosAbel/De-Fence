using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeFenceAbstract;

public class Tile : MonoBehaviour
{
    public int ID;

	InputManager inputs;

	void Awake()
	{
		inputs = new InputManager();
		inputs.Disable();

		inputs.Tile.Click.performed += ctx => GameMaster.am.ClickTile(ID);
		inputs.Tile.Click.performed += ctx => GameMaster.UpdateControlButton();
	}


	void Update()
    {
		if (GameMaster.gameEnded || GameMaster.paused) inputs.Disable();
		Color c = GameMaster.playerColors[GameMaster.am.board.tiles[ID].owner];
		if (GameMaster.am.tilesClicked != null)
		{
			if (GameMaster.am.tilesClicked.Contains(GameMaster.am.board.tiles[ID]))
			{
				c.r /= 2;
				c.g /= 2;
				c.b /= 2;
			}
		}
		gameObject.GetComponent<SpriteRenderer>().color = c;
		transform.GetChild(0).gameObject.SetActive(GameMaster.am.board.tiles[ID].hasFigure);
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