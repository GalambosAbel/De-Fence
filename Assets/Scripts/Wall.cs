using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
	public bool active = true;

	public int neighbour1;
	public int neighbour2;
	public float extraRot;

	public void Place()
	{
        Vector3 pos1 = FindObjectOfType<GenerateBoard>().tiles[neighbour1].transform.position;
        Vector3 pos2 = FindObjectOfType<GenerateBoard>().tiles[neighbour2].transform.position;

        Vector3 targetPos = (pos1 + pos2) / 2;

		Quaternion targetRot = Quaternion.Euler(0f, 0f, extraRot + FindObjectOfType<GenerateBoard>().tiles[neighbour2].transform.rotation.eulerAngles.z);

		transform.position = targetPos;

		transform.rotation = targetRot;

	}
}
