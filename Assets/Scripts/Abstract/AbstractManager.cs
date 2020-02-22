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

	public static List<AbstractTile> tilesClicked;

	public static void TookStep()
	{
		currentPlayer = currentPlayer % playerAmount + 1;
		board.LoadTerritorries();
		tilesClicked = new List<AbstractTile>();
        FindObjectOfType<Canvas>().GetComponentInChildren<Image>().color = playerColors[currentPlayer];
	}

    public void Pass()
    {
        TookStep();
    }
}
