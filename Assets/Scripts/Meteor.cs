using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour {

public Rigidbody projectile;
public Camera camera;

// Use this for initialization
void Start () {

}

// Update is called once per frame
    void Update () 
    {
        if(Input.GetKeyDown(KeyCode.Space)){
    Rigidbody clone;
    clone = (Rigidbody)Instantiate(projectile, camera.gameObject.transform.position, projectile.rotation);

    clone.velocity = (camera.transform.forward)*20;
    }
}

}
