using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessClock : MonoBehaviour
{
    public int startingTime = 50;
    public int[] timeLeft = new int[AbstractManager.playerAmount];
    public Text[] texts = new Text[AbstractManager.playerAmount];
    public int timeToAdd = 8000;

    void Start()
    {
        int curPlayer = AbstractManager.currentPlayer - 1;
        for (int i = 0; i < timeLeft.Length; i++)
        {
            timeLeft[i] = startingTime;
            texts[i].text = ConvertTime(timeLeft[i]);
        }
    }

    void Update()
    {
        int curPlayer = AbstractManager.currentPlayer-1;
        timeLeft[curPlayer] -= (int)(Time.deltaTime*1000);
        texts[curPlayer].text = ConvertTime(timeLeft[curPlayer]);

        if(timeLeft[curPlayer] < 0)
        {
            List<int> winners = new List<int>();
            for (int i = 0; i < AbstractManager.playerAmount; i++)
            {
                winners.Add(i+1);
            }
            winners.Remove(curPlayer+1);
            GameObject.Find("BoardGenerator").GetComponent<InputReciever>().GameEnded(winners.ToArray());
        }
    }

    string ConvertTime(int ms)
    {
        int sec = ms / 1000;
        int min = sec / 60;
        sec %= 60;
        if(sec > 9) return min + " : " + sec;
       else return min + " : 0" + sec;
    }

    public void AddTime(int player)
    {
        player--;
        Debug.Log("asd");
        timeLeft[player] += timeToAdd;
        texts[player].text = ConvertTime(timeLeft[player]);
    }
}
