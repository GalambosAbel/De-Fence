using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    public int player;
    public Image display;

    float r;
    float g;
    float b;

    public Text rT;
    public Text gT;
    public Text bT;

    void Start()
    {
        if (player == 1) r = 255f;
        if (player == 2) b = 255f;
        ChangeColor();
    }

    public void SetR(float _r)
    {
        r = _r;
        rT.text = ((int)r).ToString();
        ChangeColor();
    }

    public void SetG(float _g)
    {
        g = _g;
        gT.text = ((int)g).ToString();
        ChangeColor();
    }

    public void SetB(float _b)
    {
        b = _b;
        bT.text = ((int)b).ToString();
        ChangeColor();
    }

    public void ChangeColor()
    {
        display.color = new Color(r / 255f, g / 255f, b / 255f);
        GameMaster.playerColors[player] = new Color((float)r / 255f, (float)g / 255f, (float)b / 255f);
    }
}
