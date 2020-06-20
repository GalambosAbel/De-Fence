using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositioner : MonoBehaviour
{
    void Update()
    {
        Camera.main.orthographicSize = 2.65f / Camera.main.aspect;
        if (Camera.main.orthographicSize < 2.26f) Camera.main.orthographicSize = 2.26f;
    }
}
