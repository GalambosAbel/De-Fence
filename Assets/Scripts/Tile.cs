using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int ID;


    void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().color = GetColor();
    }

    Color GetColor()
    {
        int x = AbstractManager.board.tiles[ID].owner;
        if (x == 0) return Color.white;
        if (x == 1) return Color.red;
        if (x == 2) return Color.blue;
        if (x == 3) return Color.green;
        return new Color(((243 - x) *(123 + x) - 3 * x + 22) % 256, (x * x * x + 12 * x - 17) % 256, (x * 2 * x + 11 * x + 5) % 256);
    }
}
//distanceOffset = 2f  angleOffset = 60f