using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetModel : MonoBehaviour {

    public double mass;
    public Vector3d velocity;
    public Vector3d acceleration;
    public Vector3d position;
    public Vector3d force;
    public Vector3 Position;
    public Vector3 Velocity;


	// Use this for initialization
	void Start () {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        Attractor attractor = gameObject.GetComponent<Attractor>();
        PlanetData pD = gameObject.GetComponent<PlanetData>();
        if (gameObject.name == "Meteor(Clone)")
            this.position = new Vector3d (Camera.main.transform.position);
        else if (gameObject.name == "Meteor")
        {
            this.position = new Vector3d(5000,5000,5000);
        } else this.position = new Vector3d(pD.startingPoint);
        this.mass = rb.mass;
        this.velocity = new Vector3d (rb.velocity);
        this.force = Vector3d.zero;
        this.acceleration = Vector3d.zero;
    }
	
	// Update is called once per frame
	void Update () {
        Position = (Vector3)position;
        Velocity = (Vector3)velocity;

    }
}
