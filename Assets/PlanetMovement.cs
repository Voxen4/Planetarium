using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Rigidbody body = GetComponent<Rigidbody>();
        body.AddForce(new Vector3(10,0,0));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
