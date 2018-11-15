using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoom : MonoBehaviour {

 float camZoom = -10f;
 float camZoomSpeed = 2f;
 Transform Cam; 
 
 void Start (){
 Cam = this.transform;
        camZoom = this.transform.position.x;
 }
 
 void Update (){
        if( Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            camZoom += Input.GetAxis("Mouse ScrollWheel") * camZoomSpeed;
            transform.position = new Vector3(camZoom, Cam.position.y, Cam.position.z );   // use localPosition if parented to another GameObject.
        }
     } 
}
