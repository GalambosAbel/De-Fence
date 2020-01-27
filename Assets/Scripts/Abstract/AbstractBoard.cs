using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractBoard
{
    public List<AbstractTile> tiles;
    public List<AbstractWall> walls;
    public List<List<int>> graph;

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
    }
}
