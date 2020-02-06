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
    public List<GameObject> tiles = new List<GameObject>();
    public List<GameObject> walls = new List<GameObject>();
	public int radius = 3;
	private int noOfTiles;
	public float startDistanceOffset;
	public float startAngleOffset;

	List<AbstractTile> abstractTiles;
	List<AbstractWall> abstractWalls;

    int a = 1;

	public void GenerateBoardFc()
	{
		abstractTiles = new List<AbstractTile>();
		abstractWalls = new List<AbstractWall>();

		noOfTiles = radius * radius * 6;
		Vector3 startPos = new Vector3(Mathf.Sqrt(3) / 4, 0.5f, 0f);
        tiles.Add(Instantiate(tile, startPos, Quaternion.identity, tileParent));
		abstractTiles.Add(new AbstractTile(0));
		tilePositions.Add(startPos);
        tiles[0].GetComponent<Tile>().data = new TileData(startPos, startDistanceOffset, startAngleOffset, 0);
		PlaceNextTile(tiles[0]);
        PlaceWalls();
		AbstractManager.board = new AbstractBoard(abstractTiles, abstractWalls);
		Debug.Log("this is at the end of the start function, no of walls: " + walls.Count);
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
			tiles.Add(Instantiate(tile, newPos, Quaternion.Euler(newRot), tileParent));
			abstractTiles.Add(new AbstractTile(tilePositions.IndexOf(newPos)));
            GameObject newTile = tiles[tiles.Count - 1];
			//tiles[tiles.Count-1].GetComponent<Tile>().currentState = TileStates.red;

			TileData newData = new TileData(previousData);
			newData.position = tiles[tiles.Count - 1].transform.position;
            newData.ID = tilePositions.IndexOf(newPos);
            newData.neighbours.Clear();
            newData.neighbours.Add(previousData.ID);
            tiles[tiles.Count - 1].GetComponent<Tile>().data = newData;

			a++;
            PlaceNextTile(tiles[tiles.Count - 1]);
        }
        
        previous.GetComponent<Tile>().data.neighbours.Add(IdFromPos(newPos));
		newPos = Quaternion.Euler(0, 0, -previousData.angleOffset + previous.transform.rotation.eulerAngles.z) * new Vector3(0, previousData.distanceOffset, 0);
		newPos += previous.transform.position;
		if (IsPositionFree(newPos) && a < noOfTiles)
		{
            tilePositions.Add(newPos);
			Vector3 newRot = previous.transform.rotation.eulerAngles;
			newRot.z -= previousData.angleOffset;
			tiles.Add(Instantiate(tile, newPos, Quaternion.Euler(newRot), tileParent));
			abstractTiles.Add(new AbstractTile(tilePositions.IndexOf(newPos)));
            GameObject newTile = tiles[tiles.Count - 1];
            //tiles[tiles.Count - 1].GetComponent<Tile>().currentState = TileStates.blue;

			TileData newData = new TileData(previousData);
			newData.position = tiles[tiles.Count - 1].transform.position;
            newData.ID = tilePositions.IndexOf(newPos);
            newData.neighbours.Clear();
            newData.neighbours.Add(previousData.ID);
            tiles[tiles.Count - 1].GetComponent<Tile>().data = newData;

			a++;
			PlaceNextTile(tiles[tiles.Count - 1]);
		}
        previous.GetComponent<Tile>().data.neighbours.Add(IdFromPos(newPos));
    }

	bool IsPositionFree (Vector3 newPos)
	{
        foreach (Vector3 pos in tilePositions)
        {
            if (pos == newPos) return false;
        }
        return true;
	}

    int IdFromPos(Vector3 pos)
    {
        for (int i = 0; i < tilePositions.Count; i++)
        {
            if(tilePositions[i] == pos)
            {
                return i;
            }
        }
        return -1;
    }

    void PlaceWalls()
    {
		for (int i = 1; i < noOfTiles; i++)
		{
			for (int j = 0; j < i; j++)
			{
				if (tiles[i].GetComponent<Tile>().data.neighbours.Contains(j))
				{
					float extraRot = Mathf.Sign(tiles[i].transform.position.x - tiles[j].transform.position.x) * 60;

					PlaceWall(j, i);
				}
			}
		}
    }

    void PlaceWall(int _neighbour1, int _neighbour2)
    {
        GameObject newWall = Instantiate(wall, wallParent);
        newWall.GetComponent<Wall>().Place(_neighbour1, _neighbour2);
		newWall.GetComponent<Wall>().ID = walls.Count;
		abstractWalls.Add(new AbstractWall(walls.Count));
		abstractTiles[_neighbour1].neighbours.Add(new int[] { _neighbour2, walls.Count });
		abstractTiles[_neighbour2].neighbours.Add(new int[] { _neighbour1, walls.Count });
		walls.Add(newWall);
    }
}
