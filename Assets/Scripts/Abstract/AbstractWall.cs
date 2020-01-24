using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractWall
{
    public int ID;
    public bool active;
    public int[] neighbours;

    public AbstractWall(int _ID, int neighbour1, int neighbour2)
    {
        ID = _ID;
        active = true;
        neighbours = new int[] { neighbour1, neighbour2 };
    }
}
