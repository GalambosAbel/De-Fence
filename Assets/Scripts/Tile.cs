using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public State currentState = State.EMPTY;


    void Update()
    {
        switch (currentState)
        {
            case State.EMPTY:
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                transform.GetChild(0).gameObject.SetActive(false);
                break;
            case State.BLUE:
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                transform.GetChild(0).gameObject.SetActive(false);
                break;
            case State.BLUEFIGURE:
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                transform.GetChild(0).gameObject.SetActive(true);
                break;
            case State.RED:
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                transform.GetChild(0).gameObject.SetActive(false);
                break;
            case State.REDFIGURE:
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                transform.GetChild(0).gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
}

public enum State {RED, REDFIGURE, BLUE, BLUEFIGURE, EMPTY}