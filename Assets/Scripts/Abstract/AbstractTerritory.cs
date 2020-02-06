using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractTerritory
{
    public List<int> tiles;
    public int owner;
    public int strength;

    public AbstractTerritory()
    {
        tiles = new List<int>();
        owner = 0;
        strength = 0;
    }

    int WhoIsOwner()
    {
        if(tiles.Count != 0) return AbstractManager.board.tiles[tiles[0]].owner;
        return 0;
    }

    int WhatIsStrength()
    {
        int _strength = 0;
        for (int i = 0; i < tiles.Count; i++)
        {
            if (AbstractManager.board.tiles[tiles[0]].hasFigure) _strength++;
        }
        return _strength;
    }
}
