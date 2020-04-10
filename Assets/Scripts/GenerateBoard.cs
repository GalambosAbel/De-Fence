using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeFenceAbstract;

public class GenerateBoard : MonoBehaviour
{
	List<Vector3> tilePositions = new List<Vector3>();
    List<GameObject> tiles = new List<GameObject>();
    List<GameObject> walls = new List<GameObject>();
	public int radius = 3;
	private int noOfTiles;
	public float distanceOffset;
	public float angleOffset;

	List<AbstractTile> abstractTiles;
	List<AbstractWall> abstractWalls;

	List<List<int>> neighbourArray;

	public void GenerateBoardFc()
	{
		abstractTiles = new List<AbstractTile>();
		abstractWalls = new List<AbstractWall>();
		neighbourArray = new List<List<int>>();
		tilePositions = new List<Vector3>();
		tiles = GameMaster.tiles = new List<GameObject>();
		walls = GameMaster.walls = new List<GameObject>();

		noOfTiles = radius * radius * 6;
		Vector3 startPos = new Vector3(Mathf.Sqrt(3) / 4, 0.5f, 0f);
        tiles.Add(Instantiate(GameMaster.tilePrefab, startPos, Quaternion.identity, GameMaster.tileParent));
		abstractTiles.Add(new AbstractTile(0, GameMaster.am));
		neighbourArray.Add(new List<int>());
		tiles[0].GetComponent<Tile>().ID = 0;
		tilePositions.Add(startPos);
		PlaceNextTile(tiles[0]);
        PlaceWalls();
		GameMaster.am.board = new AbstractBoard(abstractTiles, abstractWalls, GameMaster.am);
		GameMaster.am.board.LoadTerritorries();
		GameMaster.tiles = tiles;
		GameMaster.walls = walls;
		GameMaster.currentMap = "Default";

		Debug.Log("this is at the end of the generate function, no of walls: " + walls.Count);
	}

	void PlaceNextTile (GameObject previous)
	{
		Vector3 newPos = Quaternion.Euler(0, 0, angleOffset + previous.transform.rotation.eulerAngles.z) * new Vector3(0, distanceOffset, 0);
		newPos += previous.transform.position;
		if (IsPositionFree(newPos) && tiles.Count < noOfTiles)
		{
            tilePositions.Add(newPos);
            Vector3 newRot = previous.transform.rotation.eulerAngles;
			newRot.z += angleOffset;
			tiles.Add(Instantiate(GameMaster.tilePrefab, newPos, Quaternion.Euler(newRot), GameMaster.tileParent));

			abstractTiles.Add(new AbstractTile(tiles.Count - 1, GameMaster.am));
			neighbourArray.Add(new List<int>());
			neighbourArray[tiles.Count - 1].Add(previous.GetComponent<Tile>().ID);
            tiles[tiles.Count - 1].GetComponent<Tile>().ID = tiles.Count - 1;

            PlaceNextTile(tiles[tiles.Count - 1]);
        }
        
        neighbourArray[previous.GetComponent<Tile>().ID].Add(IdFromPos(newPos));
		newPos = Quaternion.Euler(0, 0, -angleOffset + previous.transform.rotation.eulerAngles.z) * new Vector3(0, distanceOffset, 0);
		newPos += previous.transform.position;
		if (IsPositionFree(newPos) && tiles.Count < noOfTiles)
		{
            tilePositions.Add(newPos);
			Vector3 newRot = previous.transform.rotation.eulerAngles;
			newRot.z -= angleOffset;
			tiles.Add(Instantiate(GameMaster.tilePrefab, newPos, Quaternion.Euler(newRot), GameMaster.tileParent));

			abstractTiles.Add(new AbstractTile(tiles.Count - 1, GameMaster.am));
			neighbourArray.Add(new List<int>());
			neighbourArray[tiles.Count - 1].Add(previous.GetComponent<Tile>().ID);
			tiles[tiles.Count - 1].GetComponent<Tile>().ID = tiles.Count - 1;

			PlaceNextTile(tiles[tiles.Count - 1]);
		}
        neighbourArray[previous.GetComponent<Tile>().ID].Add(IdFromPos(newPos));
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
		for (int i = 1; i < tiles.Count; i++)
		{
			for (int j = 0; j < i; j++)
			{
				if (neighbourArray[i].Contains(j))
				{
					float extraRot = Mathf.Sign(tiles[i].transform.position.x - tiles[j].transform.position.x) * 60;

					PlaceWall(j, i);
				}
			}
		}
    }

    void PlaceWall(int _neighbour1, int _neighbour2)
    {
        GameObject newWall = Instantiate(GameMaster.wallPrefab, GameMaster.wallParent);
        newWall.GetComponent<Wall>().Place(_neighbour1, _neighbour2);
		newWall.GetComponent<Wall>().ID = walls.Count;
		abstractWalls.Add(new AbstractWall(walls.Count));
		abstractTiles[_neighbour1].neighbours.Add(new int[] { _neighbour2, walls.Count });
		abstractTiles[_neighbour2].neighbours.Add(new int[] { _neighbour1, walls.Count });
		walls.Add(newWall);
    }
}
