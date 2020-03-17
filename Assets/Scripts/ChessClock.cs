using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessClock : MonoBehaviour
{
    public int startingTime = 50;
    public int[] timeLeft = new int[AbstractManager.playerAmount];
    public Text[] texts = new Text[AbstractManager.playerAmount];

    void Start()
    {
        for (int i = 0; i < timeLeft.Length; i++)
        {
            timeLeft[i] = startingTime;
        }
    }

    void Update()
    {
        int curPlayer = AbstractManager.currentPlayer;
        timeLeft[curPlayer] -= (int)Time.deltaTime*1000;
        texts[curPlayer].text = ConvertTime(timeLeft[curPlayer]);
    }

    string ConvertTime(int ms)
    {
        int sec = ms / 1000;
        int min = sec / 60;
        sec %= 60;
        return min + " : " + sec;
    }
}
