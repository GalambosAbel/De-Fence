using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBoard : MonoBehaviour
{
    public GameObject tile;
	public Transform tileParent;

	public GameObject wall;
	public Transform wallParent;

	Dictionary<int, Vector3> tilePositions = new Dictionary<int, Vector3>();
	public int radius = 3;
	private int noOfTiles;
	public float startDistanceOffset;
	public float startAngleOffset;

    int a = 1;
    int i = 0;

	void Start()
	{
		noOfTiles = radius * radius * 6;
		Vector3 startPos = new Vector3(Mathf.Sqrt(3) / 4, 0.5f, 0f);
		GameObject startTile = Instantiate(tile, startPos, Quaternion.identity, tileParent);
        AddNewTile(startPos);
		startTile.GetComponent<Tile>().data = new TileData(startPos, startDistanceOffset, startAngleOffset, i-1);
		PlaceNextTile(startTile);
	}

	void PlaceNextTile (GameObject previous)
	{
		TileData previousData = previous.GetComponent<Tile>().data;
		Vector3 newPos = Quaternion.Euler(0, 0, previousData.angleOffset + previous.transform.rotation.eulerAngles.z) * new Vector3(0, previousData.distanceOffset, 0);
		newPos += previous.transform.position;
		if (IsPositionFree(newPos) && a < noOfTiles)
		{
            AddNewTile(newPos);
            Vector3 newRot = previous.transform.rotation.eulerAngles;
			newRot.z += previousData.angleOffset;
			GameObject placedTile = Instantiate(tile, newPos, Quaternion.Euler(newRot), tileParent);

			placedTile.GetComponent<Tile>().currentState = TileStates.red;

			TileData newData = new TileData(previousData);
			newData.position = placedTile.transform.position;
            newData.ID = i-1;
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
        if(!previousData.neighbours.Contains(GetIdFromPos(newPos)))
        {
            previous.GetComponent<Tile>().data.neighbours.Add(GetIdFromPos(newPos));
        }
		newPos = Quaternion.Euler(0, 0, -previousData.angleOffset + previous.transform.rotation.eulerAngles.z) * new Vector3(0, previousData.distanceOffset, 0);
		newPos += previous.transform.position;
		if (IsPositionFree(newPos) && a < noOfTiles)
		{
            AddNewTile(newPos);
			Vector3 newRot = previous.transform.rotation.eulerAngles;
			newRot.z -= previousData.angleOffset;
			GameObject placedTile = Instantiate(tile, newPos, Quaternion.Euler(newRot), tileParent);

			placedTile.GetComponent<Tile>().currentState = TileStates.blue;

			TileData newData = new TileData(previousData);
			newData.position = placedTile.transform.position;
            newData.ID = i-1;
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
        if (!previousData.neighbours.Contains(GetIdFromPos(newPos)))
        {
            previous.GetComponent<Tile>().data.neighbours.Add(GetIdFromPos(newPos));
        }
    }

	bool IsPositionFree (Vector3 newPos)
	{
        if (tilePositions.ContainsValue(newPos)) return false;
		return true;
	}

    void AddNewTile(Vector3 pos)
    {
        tilePositions.Add(i, pos);
        i++;
    }

    int GetIdFromPos(Vector3 pos)
    {
        foreach (var keyValuePair in tilePositions)
        {
            if (ReferenceEquals(keyValuePair.Value, pos))
            {
                return keyValuePair.Key; 
            }
        }
        return 0;
    }
}
