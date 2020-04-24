using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public void ToMenu()
    {
        Destroy(GameObject.Find("BoardGenerator").GetComponent<PhotonView>());
        SceneManager.LoadScene("MenuScene");
    }
}
