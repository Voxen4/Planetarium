using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    public Rigidbody projectile;
    public Camera camera;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Rigidbody clone;
            clone = (Rigidbody)Instantiate(projectile, camera.gameObject.transform.position, projectile.rotation);
            clone.velocity = ray.direction * 20;
            clone.GetComponent<Rigidbody>().mass = 1;
            clone.GetComponent<Attractor>().mass = 1;
            //GetComponents<AttractionManager>()[0].attractors.Add(clone.gameObject.GetComponent<Attractor>());
            AttractionManager.attractors.Add(clone.gameObject.GetComponent<Attractor>());
        }
    }

}
