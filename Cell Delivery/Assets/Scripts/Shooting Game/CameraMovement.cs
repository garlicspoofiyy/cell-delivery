using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float cameraSpeed;
    private bool stopCamera = false;

    void Update()
    {
        if (!stopCamera)
        {
            transform.position += new Vector3(0, cameraSpeed * Time.deltaTime, 0);
        }
    }

    public void StopCamera()
    {
        stopCamera = true;
    }
}