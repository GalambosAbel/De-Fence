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
			for (int i = 0; i < tiles.Count; i++)
			{
				if (AbstractManager.board.tiles[tiles[i]].hasFigure) _strength++;
			}
			return _strength;
		}
    }
}
