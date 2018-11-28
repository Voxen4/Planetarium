using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour {
    static double GRAVITATIONAL_CONSTANT = 6.67408 / 1.9 * Mathf.Pow(10, -4);
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Attract(Attractor toAttract)
    {
        PlanetData toAttractPD = toAttract.GetComponent<PlanetData>();
        PlanetData attractorPD = gameObject.GetComponent<PlanetData>();
        Rigidbody toAttractRb = toAttract.GetComponent<Rigidbody>();
        Vector3 direction = this.transform.position - toAttract.transform.position;
        double distance = direction.magnitude;

        float forceMagnitude = (float) ((toAttractPD.mass * attractorPD.mass) / Mathf.Pow((float) distance, 2) * GRAVITATIONAL_CONSTANT);
        Vector3 force = direction.normalized * forceMagnitude;
        toAttractRb.AddForce(force);
    }
}
