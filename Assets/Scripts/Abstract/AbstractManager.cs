using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractManager : MonoBehaviour
{
	public static AbstractBoard board;
    public static int playerAmount = 2;
	public static int currentPlayer = 1;

	public static List<AbstractTile> tilesClicked;

	public static void TookStep()
	{
		currentPlayer = currentPlayer % playerAmount + 1;
		board.LoadTerritorries();
		tilesClicked = new List<AbstractTile>();
	}
}
