using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayer : MonoBehaviour
{
    public Text[] playerScores;

    void Start()
    {
        for (int i = 0; i < AbstractManager.playerAmount; i++)
        {
            playerScores[i].color = AbstractManager.playerColors[i + 1];
        }
    }

    void Update()
    {
        for (int i = 0; i < AbstractManager.playerAmount; i++)
        {
            playerScores[i].text = AbstractManager.GetPlayerScore(i + 1).ToString();
        }
    }
}
