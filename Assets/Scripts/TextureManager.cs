using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Drawing;

public class TextureManager : MonoBehaviour
{
    public static Sprite[] tileSprites = new Sprite[AbstractManager.playerAmount * 2 + 1];
    public static Sprite[] wallSprites = new Sprite[2];
    public string texturesFolder;
    public string texturePackName;
    public string[] tileNames = new string[AbstractManager.playerAmount * 2 + 1];
    public string[] wallNames = new string[2];

    void Start()
    {
        ReloadTextures();
    }

    public void ReloadTextures()
    {
        for (int i = 0; i < AbstractManager.playerAmount * 2 + 1; i++)
        {
            string fileName = texturesFolder + "\\" + texturePackName + "\\" + tileNames[i];
            if (File.Exists("Assets\\Resources\\" + fileName + ".png"))
                tileSprites[i] = Resources.Load<Sprite>(fileName);
        }

        for (int i = 0; i < 2; i++)
        {
            string fileName = texturesFolder + "\\" + texturePackName + "\\" + wallNames[i];
            if (File.Exists("Assets\\Resources\\" + fileName + ".png"))
                wallSprites[i] = Resources.Load<Sprite>(fileName);
        }
    }
}
