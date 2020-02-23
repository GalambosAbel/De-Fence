using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractTile
{
    public int ID;
    public List<int[]> neighbours; //1st element is neighbour ID, 2nd element is wall ID
    public int owner;
    public bool hasFigure;

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

/*
		//támadások első fele vagy összevonás
		if (owner == AbstractManager.currentPlayer) 
		{
			// összevonás vagy támadások 1.fele
			if (AbstractManager.tilesClicked.Count == 0) 
			{
				AbstractManager.tilesClicked.Add(this);
				return;
			}
			//összevonás 2.fele (későb: ösz vont tám 1.5 fele)
			if (AbstractManager.tilesClicked.Count == 1) 
			{
				//osszevont tamadashoz meg kell valtoztatni
				if (!IsNeighbourOf(AbstractManager.tilesClicked[0].ID)) 
				{
					AbstractManager.tilesClicked = new List<AbstractTile>();
					AbstractManager.tilesClicked.Add(this);
					return;
				}
				// öszevonás 2.fele
				else
				{
					if (!GetWallOfNeighbour(AbstractManager.tilesClicked[0].ID).active)
					{
						AbstractManager.tilesClicked = new List<AbstractTile>();
						AbstractManager.tilesClicked.Add(this);
						return;
					}
					GetWallOfNeighbour(AbstractManager.tilesClicked[0].ID).active = false;
					AbstractManager.TookStep();
					return;
				}
			}
			//kell ha 3-as övt van
		}
		//támadások 2.fele
		if (owner != AbstractManager.currentPlayer)
		{
			// invalid esetek (majd később)
			if (AbstractManager.tilesClicked.Count < 1 || AbstractManager.tilesClicked.Count > neighbours.Count)
			{
				AbstractManager.tilesClicked = new List<AbstractTile>();
				AbstractManager.tilesClicked.Add(this);
				return;
			}
			// invalid esetek 2.0
			for (int i = 0; i < AbstractManager.tilesClicked.Count; i++)
			{
				if (!IsNeighbourOf(AbstractManager.tilesClicked[i].ID))
				{
					AbstractManager.tilesClicked = new List<AbstractTile>();
					AbstractManager.tilesClicked.Add(this);
					return;
				}
			}
			//sima támadás (kell még az erő)
			if(AbstractManager.tilesClicked.Count == 1)
			{
				foreach(int[] neighbour in neighbours)
				{
					AbstractManager.board.walls[neighbour[1]].active = true;
				}
				GetWallOfNeighbour(AbstractManager.tilesClicked[0].ID).active = false;
				hasFigure = false;
				owner = AbstractManager.currentPlayer;
				AbstractManager.TookStep();
			}
		}
		*/
