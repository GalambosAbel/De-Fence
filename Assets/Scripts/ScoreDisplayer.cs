using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayer : MonoBehaviour
{
    public Text[] playerScores;

    void Start()
    {
        if (!GameMaster.showScores)
        {
            for (int i = 0; i < GameMaster.am.playerAmount; i++)
            {
                playerScores[i].gameObject.SetActive(false);
            }
            return;
        }
        for (int i = 0; i < GameMaster.am.playerAmount; i++)
        {
            playerScores[i].color = GameMaster.playerColors[i + 1];
        }
    }

    void Update()
    {
        if (!GameMaster.showScores) return;
        for (int i = 0; i < GameMaster.am.playerAmount; i++)
        {
            playerScores[i].text = GameMaster.am.GetPlayerScore(i + 1).ToString();
        }
    }
}
