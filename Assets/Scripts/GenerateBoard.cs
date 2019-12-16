using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBoard : MonoBehaviour
{
    public GameObject tile;
    float scale;
    List<float> origoX = new List<float>();
    List<float> origoY = new List<float>();
    int a;
    float magassag;

    void Start()
    {
        scale = 1;
        for (int i = 0; i < 10; i++)
        {
            if(i == 0)
            {
                magassag = 0;
            }
            else if(i == 1)
            {
                magassag = 0.75f;
            }
            else if(i % 2 == 1)
            {
                magassag += 1;
            }
            else
            {
                magassag += 0.5f;
            }
            Origo(0, magassag);
        }
    }
    void Origo(float x, float y)
    {
        origoX.Add(x);
        origoY.Add(y);
        Instantiate(tile,new Vector3(x, y, 0), Quaternion.Euler(0, 0, 60 * (origoX.Count - 1)));
        Vector2 difference = new Vector2(-scale * Mathf.Sqrt(3) / 4, 0.25f * scale);
        difference = Quaternion.Euler(0, 0, 60 * ((origoX.Count - 1) % 2)) * difference;
        float newX = origoX[origoX.Count - 1] + difference.x;
        float newY = origoY[origoY.Count - 1] + difference.y;
        //if (!ContainsList(newX, newY))
        if(a < 6)
        {
            a++;
            Origo(newX, newY);
        }
        a = 0;
    }
    /*bool ContainsList(float x, float y)
    {
        for (int i = 0; i < origoX.Count; i++)
        {
            if (x == origoX[i] && y == origoY[i])
            { return false; }
        }
        return true;
    }*/
}
