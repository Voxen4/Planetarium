using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earthRotation : MonoBehaviour {

    float PlanetRotateSpeed = -25.0F;
    float OrbitSpeed = 10.0F;

	// Use this for initialization
	void Start () {

       
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(Vector3.up, Space.Self);

        //transform.Rotate(transform.up * PlanetRotateSpeed * Time.deltaTime);
        //transform.RotateAround(Vector3.zero, Vector3.up, OrbitSpeed * Time.deltaTime);



    }
}
