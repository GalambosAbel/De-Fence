using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBoard : MonoBehaviour
{
    float scale;
    List<float> origoX;
    List<float> origoY;

    void Start()
    {
        scale = Mathf.Sqrt(3) * 2;
        
            scale = 100;

        void Origo(float x, float y)
        {
            origoX.Add(x);
            origoY.Add(y);

            float newX = origoX[origoX.Count-1] - 50;
            float newY = origoY[origoY.Count - 1] + Mathf.Sqrt(3) / 3 * 50;
                if (!ContainsList(newX, newY))
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
}
