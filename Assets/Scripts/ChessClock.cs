using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessClock : MonoBehaviour
{
	public static int startingMin;
	public static int startingSec;
	public static int secToAdd;

	public bool isActive = true;
	public int startingTime;
	public int timeToAdd;

	public float[] timeLeft;
	public Text[] texts;

	public void StartStop(bool start)
	{
		startingTime = (startingMin * 60 + startingSec) * 1000;
		timeToAdd = secToAdd * 1000;

		gameObject.SetActive(start);
		isActive = start;
		GameMaster.clockEnabled = start;

		if (start) 
		{
			for (int i = 0; i < timeLeft.Length; i++)
			{
				timeLeft[i] = startingTime;
				texts[i].text = ConvertTime(timeLeft[i]);
			}
		}
	}

	void Update()
	{
		if (!isActive) return;
		int curPlayer = GameMaster.am.currentPlayer-1;
		timeLeft[curPlayer] -= Time.deltaTime*1000;
		for (int i = 0; i < timeLeft.Length; i++)
		{
			texts[i].text = ConvertTime(timeLeft[i]);
		}

		if (timeLeft[curPlayer] < 0)
		{
			List<int> winners = new List<int>();
			for (int i = 0; i < GameMaster.am.playerAmount; i++)
			{
				winners.Add(i+1);
			}
			winners.Remove(curPlayer+1);
			GameObject.Find("BoardGenerator").GetComponent<InputReciever>().GameEnded(winners.ToArray());
		}
	}

	string ConvertTime(float ms)
	{
		int sec = (int)ms / 1000;
		int min = sec / 60;
		sec %= 60;
		if(sec > 9) return min + " : " + sec;
		else return min + " : 0" + sec;
	}

	public void AddTime()
	{
		int player = GameMaster.am.currentPlayer % GameMaster.am.playerAmount;
		timeLeft[player] += timeToAdd;
		texts[player].text = ConvertTime(timeLeft[player]);
	}
}
