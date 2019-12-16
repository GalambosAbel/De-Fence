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
    float verticalOffset;
	float horizontalOffset;
	int amountInRow;

    void Start()
    {
        scale = 1;
		for (int i = 0; i < 4; i++)
        {
			if(i % 2 == 1)
			{
				origoX.Add(1);
				origoY.Add(1);
			}
			amountInRow = Mathf.FloorToInt(7 - (Mathf.Abs(1.5f - i) - 0.5f) * 2f);
			//ez meg nincs meg rendesen
			verticalOffset = 1f;
			if (Mathf.Abs(1.5f - i) == 0.5f) verticalOffset = 0.25f;
			verticalOffset *= Mathf.Sign(1.5f - i);
			//ez se
			horizontalOffset = 3 * Mathf.Sqrt(3) / 4;
			if (Mathf.Abs(1.5f - i) == 1.5f) horizontalOffset = Mathf.Sqrt(3) / 2;
			Origo(horizontalOffset, verticalOffset, amountInRow);
        }
	}
	void Origo(float x, float y, int rowLenght)
    {
        origoX.Add(x);
        origoY.Add(y);
        Instantiate(tile,new Vector3(x, y, 0), Quaternion.Euler(0, 0, 60 * (origoX.Count - 1)));
        Vector2 difference = new Vector2(-scale * Mathf.Sqrt(3) / 4, 0.25f * scale);
        difference = Quaternion.Euler(0, 0, 60 * ((origoX.Count - 1) % 2)) * difference;
        float newX = origoX[origoX.Count - 1] + difference.x;
        float newY = origoY[origoY.Count - 1] + difference.y;
        //if (!ContainsList(newX, newY))
        if(a < rowLenght - 1)
        {
            a++;
            Origo(newX, newY, rowLenght);
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
