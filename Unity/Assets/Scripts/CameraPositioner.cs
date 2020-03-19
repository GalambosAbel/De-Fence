using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositioner : MonoBehaviour
{
    void Awake()
    {
        Camera.main.orthographicSize = 2.8f;
    }
}
