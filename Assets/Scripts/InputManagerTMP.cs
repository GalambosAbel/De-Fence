using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerTMP : MonoBehaviour
{
    public string loadName;
    public string saveName;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            gameObject.GetComponent<GenerateBoard>().GenerateBoardFc();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            gameObject.GetComponent<BoardSaver>().Save(saveName);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            gameObject.GetComponent<BoardLoader>().LoadBoard(loadName);
        }
    }
}
