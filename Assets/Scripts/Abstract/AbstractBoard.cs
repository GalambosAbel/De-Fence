using System.Collections;
using System.Collections.Generic;

namespace DeFenceAbstract
{
	public class AbstractBoard
	{
		private AbstractManager abstractManager;

		internal List<AbstractTile> tiles;
		internal List<AbstractWall> walls;

		private List<bool> tilesChecked;

		#region territorries
		internal List<AbstractTerritory> territories;

		internal void LoadTerritorries()
		{
			territories = new List<AbstractTerritory>();
			tilesChecked = new List<bool>();
			for (int i = 0; i < tiles.Count; i++)
			{
				tilesChecked.Add(false);
			}

			int territoyIndex = 0;
			for (int i = 0; i < tilesChecked.Count; i++)
			{
				if (!tilesChecked[i])
				{
					territories.Add(new AbstractTerritory(abstractManager));
					CheckTile(i, territoyIndex);
					territoyIndex++;
				}
			}

			foreach (AbstractTerritory territory in territories)
			{
				territory.CleanUp();
			}
		}

		internal void CheckTile(int tileID, int territoryIndex)
		{
			territories[territoryIndex].tiles.Add(tileID);

			tilesChecked[tileID] = true;
			foreach (int[] neighbour in tiles[tileID].neighbours)
			{
				if (!walls[neighbour[1]].active && !tilesChecked[neighbour[0]])
				{
					CheckTile(neighbour[0], territoryIndex);
				}
			}
		}

		internal AbstractTerritory GetTerritoryOfTile(int ID)
		{
			foreach (AbstractTerritory territory in territories)
			{
				foreach (int tile in territory.tiles)
				{
					if (tile == ID) return territory;
				}
			}
			return null;
		}
		#endregion

		// ------------------------------------------ functions to use for interaction ------------------------------------------
		//tiles must be ordered by the tile's ID in ascending order
		public AbstractBoard(List<AbstractTile> _tiles, List<AbstractWall> _walls, AbstractManager _abstractManager)
		{
			abstractManager = _abstractManager;
			tiles = _tiles;
			walls = _walls;

			for (int i = 0; i < tiles.Count; i++)
			{
				List<int> tmp = new List<int>();
				for (int j = 0; j < tiles[i].neighbours.Count; j++)
				{
					tmp.Add(tiles[i].neighbours[j][0]);
				}
			}
		}
	}
}
