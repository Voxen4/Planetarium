using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement: MonoBehaviour {
   
    public float camZoomSpeed = 200f;
    public float camDragSpeed = 2f;
    private bool firstLookAround = false;
    private Vector2 rotation = new Vector2(0, 0);
    public float rotationSpeed = 3;
    public static bool cammed;

    private void Start()
    {
        cammed = false;
    }

    private void Update()
    {
        
        //Scroll the MouseWheel to zoom
        transform.Translate(0, 0, Input.GetAxis("Mouse ScrollWheel") * camZoomSpeed, Space.Self);

        //Press the middle MouseButton to drag the cam
        if (Input.GetMouseButton(2))
        {
            transform.Translate(-Input.GetAxisRaw("Mouse X")  * camDragSpeed, -Input.GetAxisRaw("Mouse Y") * camDragSpeed, 0);
            
        } 

        //Hold the right MouseButton to look around
        if (Input.GetMouseButton(1))
        {
            if (!firstLookAround)
            {
                rotation.x = -90;
                firstLookAround = true;
            } else
            {
            rotation.y += Input.GetAxis("Mouse X");
            rotation.x -= Input.GetAxis("Mouse Y");
            transform.eulerAngles = (Vector2)rotation * rotationSpeed;
            }
           
        }
    }


}
