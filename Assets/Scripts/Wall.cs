using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
	public bool active = true;

	public GameObject neighbour1;
	public GameObject neighbour2;
	public float extraRot;

	public void Place()
	{
		Vector3 targetPos = (neighbour1.transform.position + neighbour2.transform.position) / 2;

		Quaternion targetRot = Quaternion.Euler(0f, 0f, extraRot + neighbour1.transform.rotation.eulerAngles.z);

		transform.position = targetPos;

		transform.rotation = targetRot;

	}
}
