using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBoard : MonoBehaviour
{
    public GameObject tile;
    float scale;
    List<float> origoX = new List<float>();
    List<float> origoY = new List<float>();

    void Start()
    {
        scale = 1;
        Origo(0, 0);
    }
    void Origo(float x, float y)
    {
        origoX.Add(x);
        origoY.Add(y);
        Instantiate(tile,new Vector3(x, y, 0), Quaternion.Euler(0, 0, 60 * (origoX.Count - 1)));
        Vector2 difference = new Vector2(-scale * Mathf.Sqrt(3) / 4, 0.25f * scale);
        difference = Quaternion.Euler(0, 0, 60 * origoX.Count) * difference;
        float newX = origoX[origoX.Count - 1] + difference.x;
        float newY = origoY[origoY.Count - 1] + difference.y;
        //if (!ContainsList(newX, newY))
        if(Mathf.Abs(x) < 60)
        {
            Origo(newX, newY);
        }
    }
    bool ContainsList(float x, float y)
    {
        for (int i = 0; i < origoX.Count; i++)
        {
            if (x == origoX[i] && y == origoY[i])
            { return false; }
        }
        return true;
    }
}
