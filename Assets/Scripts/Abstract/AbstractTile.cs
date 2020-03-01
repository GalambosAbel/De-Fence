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
		foreach (AbstractTile tile in AbstractManager.tilesClicked)
		{
			if (tile.ID == ID)
			{
				AbstractManager.tilesClicked.Remove(tile);
				AbstractManager.UpdateControlButton();
				return;
			}
		}

		foreach (AbstractTile tile in AbstractManager.tilesClicked)
		{
			if (AbstractManager.board.GetTerritoryOfTile(tile.ID).tiles.Contains(ID)) return;
		}

		AbstractManager.tilesClicked.Add(this);
		AbstractManager.UpdateControlButton();
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
