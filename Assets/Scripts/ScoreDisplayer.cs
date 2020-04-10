using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayer : MonoBehaviour
{
    public Text[] playerScores;

    void Start()
    {
        for (int i = 0; i < GameMaster.am.playerAmount; i++)
        {
            playerScores[i].color = GameMaster.playerColors[i + 1];
        }
    }

    void Update()
    {
        for (int i = 0; i < GameMaster.am.playerAmount; i++)
        {
            playerScores[i].text = GameMaster.am.GetPlayerScore(i + 1).ToString();
        }
    }
}
