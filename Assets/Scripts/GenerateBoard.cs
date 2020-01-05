using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBoard : MonoBehaviour
{
    public GameObject tile;
	public Transform tileParent;

	public GameObject wall;
	public Transform wallParent;

	List<Vector3> tilePositions = new List<Vector3>();
	public int radius = 3;
	private int noOfTiles;
	public float startDistanceOffset;
	public float startAngleOffset;

    int a = 1;

	void Start()
	{
		noOfTiles = radius * radius * 6;
		Vector3 startPos = new Vector3(Mathf.Sqrt(3) / 4, 0.5f, 0f);
		GameObject startTile = Instantiate(tile, startPos, Quaternion.identity, tileParent);
        tilePositions.Add(startPos);
        startTile.GetComponent<Tile>().data = new TileData(startPos, startDistanceOffset, startAngleOffset, 0);
		PlaceNextTile(startTile);
	}

	void PlaceNextTile (GameObject previous)
	{
		TileData previousData = previous.GetComponent<Tile>().data;
		Vector3 newPos = Quaternion.Euler(0, 0, previousData.angleOffset + previous.transform.rotation.eulerAngles.z) * new Vector3(0, previousData.distanceOffset, 0);
		newPos += previous.transform.position;
		if (IsPositionFree(newPos) && a < noOfTiles)
		{
            tilePositions.Add(newPos);
            Vector3 newRot = previous.transform.rotation.eulerAngles;
			newRot.z += previousData.angleOffset;
			GameObject placedTile = Instantiate(tile, newPos, Quaternion.Euler(newRot), tileParent);

			placedTile.GetComponent<Tile>().currentState = TileStates.red;

			TileData newData = new TileData(previousData);
			newData.position = placedTile.transform.position;
            newData.ID = tilePositions.IndexOf(newPos);
            newData.neighbours.Clear();
            newData.neighbours.Add(previousData.ID);
			placedTile.GetComponent<Tile>().data = newData;

			GameObject newWall = Instantiate(wall, wallParent);
			newWall.GetComponent<Wall>().neighbour1 = previous;
			newWall.GetComponent<Wall>().neighbour2 = placedTile;
			newWall.GetComponent<Wall>().extraRot = previousData.angleOffset;
			newWall.GetComponent<Wall>().Place();

			a++;
			PlaceNextTile(placedTile);
		}
        previous.GetComponent<Tile>().data.neighbours.Add(tilePositions.IndexOf(newPos));
		newPos = Quaternion.Euler(0, 0, -previousData.angleOffset + previous.transform.rotation.eulerAngles.z) * new Vector3(0, previousData.distanceOffset, 0);
		newPos += previous.transform.position;
		if (IsPositionFree(newPos) && a < noOfTiles)
		{
            tilePositions.Add(newPos);
			Vector3 newRot = previous.transform.rotation.eulerAngles;
			newRot.z -= previousData.angleOffset;
			GameObject placedTile = Instantiate(tile, newPos, Quaternion.Euler(newRot), tileParent);

			placedTile.GetComponent<Tile>().currentState = TileStates.blue;

			TileData newData = new TileData(previousData);
			newData.position = placedTile.transform.position;
            newData.ID = tilePositions.IndexOf(newPos); ;
            newData.neighbours.Clear();
            newData.neighbours.Add(previousData.ID);
            placedTile.GetComponent<Tile>().data = newData;

			GameObject newWall = Instantiate(wall, wallParent);
			newWall.GetComponent<Wall>().neighbour1 = previous;
			newWall.GetComponent<Wall>().neighbour2 = placedTile;
			newWall.GetComponent<Wall>().extraRot = -previousData.angleOffset;
			newWall.GetComponent<Wall>().Place();

			a++;
			PlaceNextTile(placedTile);
		}
        previous.GetComponent<Tile>().data.neighbours.Add(tilePositions.IndexOf(newPos));
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
