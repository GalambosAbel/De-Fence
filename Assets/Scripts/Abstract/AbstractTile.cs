using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractTile
{
    public int ID;
    public List<int[]> neighbours; //1st element is neighbour ID, 2nd element is wall ID
    public int owner;
    public bool hasFigure;
	public int Strength
	{
		get
		{
			return AbstractManager.board.GetTerritoryOfTile(ID).Strength;
		}
	}

    public AbstractTile(int _ID)
    {
        ID = _ID;
        owner = 0;
        hasFigure = false;
		neighbours = new List<int[]>();
	}

	public AbstractTile(int _ID, List<int[]> _neighbours)
	{
		ID = _ID;
		neighbours = new List<int[]>();
		neighbours.AddRange(_neighbours);
		owner = 0;
        hasFigure = false;
    }

	public void ClickedTile() 
	{
		int moveType = AbstractManager.moveTypeSelector.value;
		int currP = AbstractManager.currentPlayer;

		foreach (AbstractTile tile in AbstractManager.tilesClicked)
		{
			if (tile.ID == ID)
			{
				AbstractManager.tilesClicked.Remove(tile);
				return;
			}
		}

		if (moveType == 0) 
		{
			if (owner == 0)
			{
				owner = currP;
				hasFigure = true;
				AbstractManager.TookStep();
				return;
			}
			else
			{
				Debug.LogError("invalid move attempt");
				return;
			}
		}

		if (moveType == 1)
		{
			if (owner == currP)
			{
				if (!hasFigure) return;
				if(AbstractManager.tilesClicked.Count == 0)
				{
					AbstractManager.tilesClicked.Add(this);
				}
				else if (!IsNeighbourOf(AbstractManager.tilesClicked[0].ID))
				{
					AbstractManager.tilesClicked = new List<AbstractTile>();
					AbstractManager.tilesClicked.Add(this);
				}
				else if (GetWallOfNeighbour(AbstractManager.tilesClicked[0].ID).active)
				{
					GetWallOfNeighbour(AbstractManager.tilesClicked[0].ID).active = false;
					AbstractManager.TookStep();
				}
				return;
			}
			else
			{
				Debug.LogError("invalid move attempt");
				return;
			}
		}

		if (moveType == 2)
		{
			if (!hasFigure) return;
			if (AbstractManager.tilesClicked.Count == 0)
			{
				AbstractManager.tilesClicked.Add(this);
			}
			else if (AbstractManager.tilesClicked[0].owner != currP && owner != currP)
			{
				AbstractManager.tilesClicked = new List<AbstractTile>();
				AbstractManager.tilesClicked.Add(this);
			}
			else if (AbstractManager.tilesClicked[0].owner == owner)
			{
				AbstractManager.tilesClicked = new List<AbstractTile>();
				AbstractManager.tilesClicked.Add(this);
			}
			else if (IsNeighbourOf(AbstractManager.tilesClicked[0].ID))
			{
				AbstractTile enemy = owner == currP ? AbstractManager.tilesClicked[0] : this;
				AbstractTile friend = owner != currP ? AbstractManager.tilesClicked[0] : this;

				if (friend.Strength <= enemy.Strength) return;

				foreach (int[] neighbour in enemy.neighbours)
				{
					AbstractManager.board.walls[neighbour[1]].active = true;
				}
				enemy.GetWallOfNeighbour(friend.ID).active = false;
				enemy.hasFigure = false;
				enemy.owner = currP;
				AbstractManager.TookStep();
			}
			return;
		}

		if (moveType == 3)
		{
			if (!hasFigure) return;

			if (owner == currP)
			{
				foreach(AbstractTile tile in AbstractManager.tilesClicked)
				{
					if (AbstractManager.board.GetTerritoryOfTile(tile.ID).tiles.Contains(ID)) return;
				}
				AbstractManager.tilesClicked.Add(this);
				return;
			}
			else
			{
				if (AbstractManager.tilesClicked.Count < 1) return;

				int friendStrength = 0;
				foreach (AbstractTile tile in AbstractManager.tilesClicked)
				{
					if (!IsNeighbourOf(tile.ID))
					{
						AbstractManager.tilesClicked = new List<AbstractTile>();
						return;
					}
					friendStrength += tile.Strength;
				}

				if (friendStrength > Strength)
				{
					foreach (int[] neighbour in neighbours)
					{
						AbstractManager.board.walls[neighbour[1]].active = true;
					}
					foreach (AbstractTile tile in AbstractManager.tilesClicked)
					{
						GetWallOfNeighbour(tile.ID).active = false;
					}
					owner = currP;
					hasFigure = false;
					AbstractManager.TookStep();
					return;
				}
			}
			return;
		}
	}
	
	public bool IsNeighbourOf (int neighbourID)
	{
		foreach (int[] neighbour in neighbours)
		{
			if (neighbour[0] == neighbourID) return true;
		}
		return false;
	}

	public AbstractWall GetWallOfNeighbour (int neighbourID)
	{
		foreach (int[] neighbour in neighbours)
		{
			if (neighbour[0] == neighbourID) return AbstractManager.board.walls[neighbour[1]];
		}
		return null;
	}
}
