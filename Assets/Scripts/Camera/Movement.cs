using System;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
   
    public static float movementSpeed = 1f;

    public float lookAroundSensitivity = 3f;
    
    public float camZoomSpeed = 200f;
    
    void Update()
    {

        // Mouse Zoom
        transform.Translate(0, 0, Input.GetAxis("Mouse ScrollWheel") * camZoomSpeed, Space.Self);

        // Zoom in with 'W'
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = transform.position + (transform.forward * movementSpeed);
        }

        // Move left with 'A'
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position + (-transform.right * movementSpeed);
        }

        // Zoom out with 'S'
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = transform.position + (-transform.forward * movementSpeed);
        }

        // Move right with 'D'
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position + (transform.right * movementSpeed);
        }

        // Look around with Right Mouse
        if (Input.GetMouseButton(1))
        {
            float newRotationX = transform.eulerAngles.y + Input.GetAxis("Mouse X") * lookAroundSensitivity;
            float newRotationY = transform.eulerAngles.x - Input.GetAxis("Mouse Y") * lookAroundSensitivity;
            transform.localEulerAngles = new Vector3(newRotationY, newRotationX, 0f);
            
        } 



    }
    
}