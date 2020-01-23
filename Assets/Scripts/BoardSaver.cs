using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BoardSaver : MonoBehaviour
{
    List<GameObject> tiles = new List<GameObject>();
    List<GameObject> walls = new List<GameObject>();
    List<string> output = new List<string>();

    public GameObject tileParent;
    public GameObject wallParent;

    public void Save(string outputFileName)
    {
        Debug.Log("Saving");
        foreach (Transform child in tileParent.transform)
        {
            tiles.Add(child.gameObject);
        }
        foreach (Transform child in wallParent.transform)
        {
            walls.Add(child.gameObject);
        }

        output.Add(tiles.Count.ToString());
        output.Add(walls.Count.ToString());

        for (int i = 0; i < tiles.Count; i++)
        {
            WriteTile(i);
        }
        for (int i = 0; i < walls.Count; i++)
        {
            WriteWall(i);
        }
        if (File.Exists(outputFileName))
        {
            int nameFix = 0;
            while (File.Exists(outputFileName + nameFix.ToString()))
            {
                nameFix++;
            }
            outputFileName += nameFix.ToString();
        }
        File.WriteAllLines(outputFileName, output);
        Debug.Log("saving complete");
    }

    public void WriteTile(int i)
    {
        output.Add(tiles[i].transform.position.x.ToString());
        output.Add(tiles[i].transform.position.y.ToString());
        output.Add(tiles[i].transform.rotation.eulerAngles.z.ToString());
        output.Add(tiles[i].GetComponent<Tile>().data.neighbours.Count.ToString());
        for (int j = 0; j < tiles[i].GetComponent<Tile>().data.neighbours.Count; j++)
        {
            output.Add(tiles[i].GetComponent<Tile>().data.neighbours[j].ToString());
        }

    }
    public void WriteWall(int i)
    {
        output.Add(walls[i].transform.position.x.ToString());
        output.Add(walls[i].transform.position.y.ToString());
        output.Add(walls[i].transform.rotation.eulerAngles.z.ToString());
        output.Add(walls[i].GetComponent<Wall>().neighbour1.ToString());
        output.Add(walls[i].GetComponent<Wall>().neighbour2.ToString());
    }
}
