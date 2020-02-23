using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbstractManager : MonoBehaviour
{
	public static AbstractBoard board;
    public static int playerAmount = 2;
	public static int currentPlayer = 1;
    public static Color[] playerColors = new Color[] { Color.white, Color.red, Color.blue, Color.green, Color.black };
	public static Dropdown moveTypeSelector;

	void Awake()
	{
		moveTypeSelector = FindObjectOfType<Dropdown>();
	}

	public static List<AbstractTile> tilesClicked;

	public static void TookStep()
	{
		currentPlayer = currentPlayer % playerAmount + 1;
		board.LoadTerritorries();
		tilesClicked = new List<AbstractTile>();
        GameObject.Find("PassButton").GetComponent<Image>().color = playerColors[currentPlayer];
	}

    public void Pass()
    {
        TookStep();
    }

	public void ResetTurnNonStatic()
	{
		ResetTurn();
	}

	public static void ResetTurn()
	{
		tilesClicked = new List<AbstractTile>();
	}
}
