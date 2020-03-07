using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class TextureManager : MonoBehaviour
{
    public static Sprite[] tileSprites = new Sprite[AbstractManager.playerAmount + 1];
    public static Sprite[] figureSprites = new Sprite[AbstractManager.playerAmount + 1];
    public static Sprite[] wallSprites = new Sprite[2];

	public Sprite[] tS = new Sprite[AbstractManager.playerAmount * 2 + 1];
	public Sprite[] fS = new Sprite[AbstractManager.playerAmount * 2 + 1];
	public Sprite[] wS = new Sprite[2];

	public string texturesFolder;
    public string texturePackName;
    public string[] tileNames = new string[AbstractManager.playerAmount * 2 + 1];
    public string[] figureNames = new string[AbstractManager.playerAmount * 2 + 1];
    public string[] wallNames = new string[2];

    void Start()
    {
        ReloadTextures();
    }

    public void ReloadTextures()
    {
		if (texturePackName == "" || texturePackName == " ") return;
		for (int i = 0; i < AbstractManager.playerAmount + 1; i++)
		{
			string savePath = "Assets\\p" + i + "_tile";
			string loadPath = texturesFolder + "\\" + texturePackName + "\\" + tileNames[i];

			SaveTexture(savePath, LoadTexture(loadPath));
		}

		for (int i = 0; i < AbstractManager.playerAmount + 1; i++)
		{
			string savePath = "Assets\\p" + i + "_figure";
			string loadPath = texturesFolder + "\\" + texturePackName + "\\" + figureNames[i];

			SaveTexture(savePath, LoadTexture(loadPath));
		}
	}
	
	Texture2D LoadTexture(string path)
	{
		byte[] raw = File.ReadAllBytes(path);
		Texture2D retTex = new Texture2D(1024, 1024);
		retTex.LoadImage(raw);
		return retTex;
	}

	void SaveTexture(string path, Texture2D texture)
	{
		byte[] saveImage = texture.EncodeToPNG();
		File.WriteAllBytes(path, saveImage);
	}
}

// még messze nincs kész, nem fog az agyam