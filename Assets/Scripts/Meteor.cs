using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Klasse zum erstellen von Asteroiden
/// </summary>
public class Meteor : MonoBehaviour
{

    public Rigidbody projectile;
    public Camera camera;

    // Use this for initialization
    void Start()
    {

    }

    /// <summary>
    /// Update wartet auf das drücken der Leertaste daraufhin wir ein Asteroid zu erzeugt, der Vektor zwischen der Kamera und dem Mauszeiger bildet die Richtung des Asteroiden.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Rigidbody clone;
            clone = (Rigidbody)Instantiate(projectile, camera.gameObject.transform.position, projectile.rotation);
            clone.velocity = ray.direction * 2;
            clone.GetComponent<Rigidbody>().mass = 1000;
            clone.GetComponent<Attractor>().mass = 1000;
            //GetComponents<AttractionManager>()[0].attractors.Add(clone.gameObject.GetComponent<Attractor>());
            AttractionManager.attractors.Add(clone.gameObject.GetComponent<Attractor>());
        }

    }

}
