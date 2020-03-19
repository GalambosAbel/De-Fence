using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractTerritory
{
    public List<int> tiles;

    public AbstractTerritory()
    {
        tiles = new List<int>();
    }

    public int Owner
    {
		get
		{
			return AbstractManager.board.tiles[tiles[0]].owner;
		}
    }

    public int Strength
    {
		get
		{
			int _strength = 0;
			foreach (int tile in tiles)
			{ 
				if (AbstractManager.board.tiles[tile].hasFigure) _strength++;
			}
			return _strength;
		}
    }


    public void CleanUp()
    {
        if(Strength == 0)
        {
            foreach(int tile in tiles)
            {
                AbstractManager.board.tiles[tile].owner = 0;
                foreach (int[] neighbour in AbstractManager.board.tiles[tile].neighbours)
                {
                    AbstractManager.board.walls[neighbour[1]].active = true;
                }
            }
        }
        else
        {
            for (int i = 1; i < tiles.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (AbstractManager.board.tiles[tiles[i]].IsNeighbourOf(tiles[j]))
                    {
                        AbstractManager.board.tiles[tiles[i]].GetWallOfNeighbour(tiles[j]).active = false;
                    }
                }
            }
        }
    }
}
