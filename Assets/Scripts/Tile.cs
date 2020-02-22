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
		Color c = AbstractManager.playerColors[AbstractManager.board.tiles[ID].owner];
		if (AbstractManager.tilesClicked != null)
		{
			if (AbstractManager.tilesClicked.Contains(AbstractManager.board.tiles[ID]))
			{
				c.r /= 2;
				c.g /= 2;
				c.b /= 2;
			}
		}
		gameObject.GetComponent<SpriteRenderer>().color = c;
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