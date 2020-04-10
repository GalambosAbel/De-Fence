using System.Collections;
using System.Collections.Generic;

namespace DeFenceAbstract
{
	public class AbstractTerritory
	{
		private AbstractManager abstractManager;

		internal List<int> tiles;

		internal AbstractTerritory(AbstractManager _abstractManager)
		{
			abstractManager = _abstractManager;
			tiles = new List<int>();
		}

		internal int Owner
		{
			get
			{
				return abstractManager.board.tiles[tiles[0]].owner;
			}
		}

		internal int Strength
		{
			get
			{
				int _strength = 0;
				foreach (int tile in tiles)
				{
					if (abstractManager.board.tiles[tile].hasFigure) _strength++;
				}
				return _strength;
			}
		}

		internal void CleanUp()
		{
			if (Strength == 0)
			{
				foreach (int tile in tiles)
				{
					abstractManager.board.tiles[tile].owner = 0;
					foreach (int[] neighbour in abstractManager.board.tiles[tile].neighbours)
					{
						abstractManager.board.walls[neighbour[1]].active = true;
					}
				}
			}
			else
			{
				for (int i = 1; i < tiles.Count; i++)
				{
					for (int j = 0; j < i; j++)
					{
						if (abstractManager.board.tiles[tiles[i]].IsNeighbourOf(tiles[j]))
						{
							abstractManager.board.tiles[tiles[i]].GetWallOfNeighbour(tiles[j]).active = false;
						}
					}
				}
			}
		}
	}
}
