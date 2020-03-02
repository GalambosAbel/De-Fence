using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbstractManager : MonoBehaviour
{
	public static AbstractManager instance;
	public static AbstractBoard board;
    public static int playerAmount = 2;
	public static int currentPlayer = 1;
    public static Color[] playerColors = new Color[] { Color.white, Color.red, Color.blue, Color.green, Color.black };
	public static bool lastPassed = false;
	public static bool gameEnded = false;
	public static Button controlButton;
	public Button _controlButton;


	void Awake()
	{
		controlButton = _controlButton;
		tilesClicked = new List<AbstractTile>();
		instance = this;
		controlButton.onClick.AddListener(instance.Pass);

		lastPassed = false;
		currentPlayer = 1;
		gameEnded = false;
	}

	public static List<AbstractTile> tilesClicked;

	public static void UpdateControlButton()
	{
		controlButton.onClick.RemoveAllListeners();

		if (tilesClicked.Count == 0)
		{
			controlButton.onClick.AddListener(instance.Pass);
			controlButton.GetComponentInChildren<Text>().text = "Pass";
		}
		else if (CanPlace())
		{
			controlButton.onClick.AddListener(instance.Place);
			controlButton.GetComponentInChildren<Text>().text = "Place";
		}
		else if (CanGroup())
		{
			controlButton.onClick.AddListener(instance.Group);
			controlButton.GetComponentInChildren<Text>().text = "Group";
		}
		else if (CanAttack())
		{
			controlButton.onClick.AddListener(instance.Attack);
			controlButton.GetComponentInChildren<Text>().text = "Attack";
		}
		else
		{
			controlButton.onClick.AddListener(instance.ResetTurn);
			controlButton.GetComponentInChildren<Text>().text = "ResetTurn";
		}
	}

	public static void TookStep()
	{
		currentPlayer = currentPlayer % playerAmount + 1;
		board.LoadTerritorries();
		tilesClicked = new List<AbstractTile>();
        controlButton.GetComponent<Image>().color = playerColors[currentPlayer];
		lastPassed = false;
		UpdateControlButton();
	}

	// can do step functions
	public static bool CanPlace()
	{
		if (tilesClicked.Count != 1) return false;

		if (tilesClicked[0].owner == 0) return true;

		return false;
	}

	public static bool CanGroup()
	{
		if (tilesClicked.Count != 2) return false;

		foreach(AbstractTile tile in tilesClicked)
		{
			if (!tile.hasFigure) return false;
			if (tile.owner != currentPlayer) return false;
		}

		if (!tilesClicked[0].IsNeighbourOf(tilesClicked[1].ID)) return false;

		return tilesClicked[0].GetWallOfNeighbour(tilesClicked[1].ID).active;
	}

	public static bool CanAttack()
	{
		if (tilesClicked.Count < 2) return false;

		AbstractTile enemy = null;
		List<AbstractTile> friends = new List<AbstractTile>();

		foreach (AbstractTile tile in tilesClicked)
		{
			if (!tile.hasFigure) return false;

			if (tile.owner == currentPlayer) friends.Add(tile);
			else if (enemy != null) return false;
			else enemy = tile;
		}
		if (enemy == null) return false;
		int friendStrength = 0;

		foreach (AbstractTile tile in friends)
		{
			if (!tile.IsNeighbourOf(enemy.ID)) return false;
			friendStrength += tile.Strength;
		}


		return (friendStrength > enemy.Strength);
	}

	//do step functions

	public void Place()
	{
		if (!CanPlace()) return;

		tilesClicked[0].owner = currentPlayer;
		tilesClicked[0].hasFigure = true;

		TookStep();
	}

	public void Group()
	{
		if (!CanGroup()) return;

		tilesClicked[0].GetWallOfNeighbour(tilesClicked[1].ID).active = false;

		TookStep();
	}

	public void Attack()
	{
		if (!CanAttack()) return;

		AbstractTile enemy = null;
		List<AbstractTile> friends = new List<AbstractTile>();

		foreach (AbstractTile tile in tilesClicked)
		{
			if (tile.owner == currentPlayer) friends.Add(tile);
			else enemy = tile;
		}

		foreach (int[] neighbour in enemy.neighbours)
		{
			board.walls[neighbour[1]].active = true;
		}
		foreach (AbstractTile friend in friends)
		{
			enemy.GetWallOfNeighbour(friend.ID).active = false;
		}
		enemy.owner = currentPlayer;
		enemy.hasFigure = false;

		TookStep();
	}

	public void Pass()
	{
		if (lastPassed)
		{
			gameEnded = true;
			GameObject.Find("BoardGenerator").GetComponent<InputReciever>().GameEnded();
			return;
		}

		TookStep();
		lastPassed = true;
	}

	public void ResetTurn()
	{
		tilesClicked = new List<AbstractTile>();
		UpdateControlButton();
	}

	// scoring functions

	public static int[] LeadingPlayer
	{
		get
		{
			List<int> leaders = new List<int>();
			int maxSoFar = 0;
			for (int i = 1; i <= playerAmount; i++)
			{
				if (GetPlayerScore(i) > maxSoFar)
				{
					leaders = new List<int>();
					leaders.Add(i);
					maxSoFar = GetPlayerScore(i);
				}
				else if(GetPlayerScore(i) == maxSoFar)
				{
					leaders.Add(i);
				}
			}
				return leaders.ToArray();
		}
	}

	public static int GetPlayerScore(int player)
	{
		if (player < 1 || player > playerAmount) return 0;
		int score = 0;
		foreach (AbstractTerritory territory in board.territories)
		{
			if (territory.Owner == player) score++;
		}
		return score;
	}
}
