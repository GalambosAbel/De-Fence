using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
	public int ID;

	public void Place(int neighbour1, int neighbour2)
	{
        Vector3 pos1 = GameMaster.tiles[neighbour1].transform.position;
        Vector3 pos2 = GameMaster.tiles[neighbour2].transform.position;

        Vector3 targetPos = (pos1 + pos2) / 2;
        Vector3 vector1 = (pos2 - pos1);
        Vector2 vector2 = new Vector3(vector1.y, -vector1.x);
        float angle = Vector2.SignedAngle(new Vector2(1, 0),vector2);
		Quaternion targetRot = Quaternion.Euler(0f, 0f, angle);

        transform.position = targetPos;
		transform.rotation = targetRot;
	}

	void Update()
	{
        transform.localScale = new Vector3(0.85f / (GameMaster.am.board.walls[ID].active ? 1f : 3f), 0.05f, 1f);
    }
}
