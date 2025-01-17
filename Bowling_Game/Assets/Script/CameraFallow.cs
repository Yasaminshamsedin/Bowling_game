using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    public Transform ball; 
    public Vector3 offset; 
    public float smoothSpeed = 0.125f; 

    void Start()
    {

    }

    void LateUpdate()
    {
        Vector3 desiredPosition = ball.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(ball);
    }
}
