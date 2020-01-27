using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractBoard
{
    public List<AbstractTile> tiles;
    public List<AbstractWall> walls;
    public List<List<int>> graph;
    public List<List<int>> territories;

	private List<bool> tilesChecked;

    public AbstractBoard()
    {
        tiles = new List<AbstractTile>();
        walls = new List<AbstractWall>();
        graph = new List<List<int>>();
    }

    //tiles must be ordered by the tile's ID in ascending order
    public AbstractBoard(List<AbstractTile> _tiles, List<AbstractWall> _walls)
    {
        tiles = _tiles;
        walls = _walls;
        graph = new List<List<int>>();

        for (int i = 0; i < tiles.Count; i++)
        {
            List<int> tmp = new List<int>();
            for (int j = 0; j < tiles[i].neighbours.Count; j++)
            {
                tmp.Add(tiles[i].neighbours[j][0]);
            }
            graph.Add(tmp);
        }

		LoadTerritorries();
    }

	public void LoadTerritorries ()
	{
		territories = new List<List<int>>();
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
				territories.Add(new List<int>());
				CheckTile(i, territoyIndex);
				territoyIndex++;
			}
		}
	}

	public void CheckTile (int tileID, int territoryIndex)
	{
		territories[territoryIndex].Add(tileID);

		tilesChecked[tileID] = true;
		foreach (int[] neighbour in tiles[tileID].neighbours)
		{
			if (!walls[neighbour[1]].active && !tilesChecked[neighbour[0]])
			{
				CheckTile(neighbour[0], territoryIndex);
			}
		}
	}
}
