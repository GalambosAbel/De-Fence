using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBoard : MonoBehaviour
{
    public GameObject tile;

	List<Vector3> tilePositions = new List<Vector3>();
	public int radius = 3;
	public float startDistanceOffset;
	public float startAngleOffset;

    int a = 1;

	void Start()
	{
		radius = radius * radius * 6;
		Vector3 startPos = new Vector3(Mathf.Sqrt(3) / 4, 0.5f, 0f);
		GameObject startTile = Instantiate(tile, startPos, Quaternion.identity);
		tilePositions.Add(startPos);
		startTile.GetComponent<Tile>().data = new TileData(startPos, startDistanceOffset, startAngleOffset);
		PlaceNextTile(startTile);
	}

	void PlaceNextTile (GameObject previous)
	{
		TileData previousData = previous.GetComponent<Tile>().data;
		Vector3 newPos = Quaternion.Euler(0, 0, previousData.angleOffset + previous.transform.rotation.eulerAngles.z) * new Vector3(0, previousData.distanceOffset, 0);
		newPos += previous.transform.position;
		if (IsPositionFree(newPos) && a < radius)
		{
			tilePositions.Add(newPos);
			Vector3 newRot = previous.transform.rotation.eulerAngles;
			newRot.z += previousData.angleOffset;
			GameObject placedTile = Instantiate(tile, newPos, Quaternion.Euler(newRot));

			TileData newData = new TileData(previousData);
			newData.position = placedTile.transform.position;
			placedTile.GetComponent<Tile>().data = newData;
			a++;
			PlaceNextTile(placedTile);
		}
		newPos = Quaternion.Euler(0, 0, -previousData.angleOffset + previous.transform.rotation.eulerAngles.z) * new Vector3(0, previousData.distanceOffset, 0);
		newPos += previous.transform.position;
		if (IsPositionFree(newPos) && a < radius)
		{
			tilePositions.Add(newPos);
			Vector3 newRot = previous.transform.rotation.eulerAngles;
			newRot.z -= previousData.angleOffset;
			GameObject placedTile = Instantiate(tile, newPos, Quaternion.Euler(newRot));

			TileData newData = new TileData(previousData);
			newData.position = placedTile.transform.position;
			placedTile.GetComponent<Tile>().data = newData;
			a++;
			PlaceNextTile(placedTile);
		}
	}

	bool IsPositionFree (Vector3 newPos)
	{
		foreach (Vector3 pos in tilePositions)
		{
			if (pos == newPos) return false;
		}
		return true;
	}
}
