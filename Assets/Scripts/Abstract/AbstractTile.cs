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

}
