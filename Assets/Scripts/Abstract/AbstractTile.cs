using System.Collections;
using System.Collections.Generic;

namespace DeFenceAbstract
{
	public class AbstractTile
	{
		private AbstractManager abstractManager;

		internal int ID;
		internal List<int[]> neighbours; //1st element is neighbour ID, 2nd element is wall ID
		internal int owner;
		internal bool hasFigure;
		internal int Strength
		{
			get
			{
				return abstractManager.board.GetTerritoryOfTile(ID).Strength;
			}
		}

		internal void ClickedTile()
		{
			foreach (AbstractTile tile in abstractManager.tilesClicked)
			{
				if (tile.ID == ID)
				{
					abstractManager.tilesClicked.Remove(tile);
					return;
				}
			}

			foreach (AbstractTile tile in abstractManager.tilesClicked)
			{
				if (abstractManager.board.GetTerritoryOfTile(tile.ID).tiles.Contains(ID)) return;
			}

			abstractManager.tilesClicked.Add(this);
		}

		internal bool IsNeighbourOf(int neighbourID)
		{
			foreach (int[] neighbour in neighbours)
			{
				if (neighbour[0] == neighbourID) return true;
			}
			return false;
		}

		internal AbstractWall GetWallOfNeighbour(int neighbourID)
		{
			foreach (int[] neighbour in neighbours)
			{
				if (neighbour[0] == neighbourID) return abstractManager.board.walls[neighbour[1]];
			}
			return null;
		}

		// ------------------------------------------ functions to use for interaction ------------------------------------------

		public AbstractTile(int _ID, AbstractManager _abstractManager)
		{
			abstractManager = _abstractManager;
			ID = _ID;
			owner = 0;
			hasFigure = false;
			neighbours = new List<int[]>();
		}

		public AbstractTile(int _ID, List<int[]> _neighbours, AbstractManager _abstractManager)
		{
			abstractManager = _abstractManager;
			ID = _ID;
			neighbours = new List<int[]>();
			neighbours.AddRange(_neighbours);
			owner = 0;
			hasFigure = false;
		}
	}
}
