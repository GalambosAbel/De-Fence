using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractWall
{
    public int ID;
    public bool active;

    public AbstractWall(int _ID)
    {
        ID = _ID;
        active = true;
    }
}
