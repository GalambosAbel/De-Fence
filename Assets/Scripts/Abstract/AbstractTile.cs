using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractTile
{
    public int ID;
    public List<int[]> neighbours; //1st element is neighbour ID, 2nd element is wall ID
    public int state;

    public AbstractTile(int _ID)
    {
        ID = _ID;
        state = 0;
    }

}
