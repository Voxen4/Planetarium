using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Attract(Attractor toAttract)
    {
        Rigidbody toAttractRb = toAttract.GetComponent<Rigidbody>();
        Vector3 direction = this.transform.position - toAttract.transform.position;
        float distance = direction.magnitude;

        float forceMagnitude = toAttractRb.mass * this.GetComponent<Rigidbody>().mass / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        toAttractRb.AddForce(force);
    }
}
