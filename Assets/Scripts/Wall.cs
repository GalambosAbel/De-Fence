﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
	public int ID;

	public void Place(int neighbour1, int neighbour2)
	{
        Vector3 pos1 = FindObjectOfType<GenerateBoard>().tiles[neighbour1].transform.position;
        Vector3 pos2 = FindObjectOfType<GenerateBoard>().tiles[neighbour2].transform.position;

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
		GetComponent<SpriteRenderer>().enabled = AbstractManager.board.walls[ID].active;
	}
}
