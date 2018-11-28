using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetData : MonoBehaviour {
    //Wir können leider diese größen Zahlen nicht im Transform oder im Rigidbody einzeigen lassen.
    //Folgenden Umrechnungsfaktoren:
    //Strecken [km] -> 10^6 [km] => Umrechungsfaktor von 10^-6 (10^6 km in der realität werden zu der Einheit 1 im Transform)
    //Masse [kg] -> 10^22 [kg]
    public double mass;             //Masse des Körpers in kg
    public float diameter;          //Durchmesser des Körpers in km
    public double apohelHeight;     //Höhe der Apoapsis in km
    public double apohelSpeed;      //Geschwindigkeit an Apoapsis in km/s#

    public GameObject sphereOfInfluence;

    private float maxDistance;
    private float minDistance;

    public void Start()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, (float) apohelSpeed);

        maxDistance = float.MinValue;
        minDistance = float.MaxValue;
    }

    public void Update()
    {
        if (sphereOfInfluence != null)
        {
            float distance = Vector3.Magnitude(gameObject.transform.position - sphereOfInfluence.transform.position);
            if (distance < minDistance)
                minDistance = distance;
            else if (distance > maxDistance)
                maxDistance = distance;
        }
    }

    public float getMinDistance()
    {
        return minDistance;
    }

    public float getMaxDistance()
    {
        return maxDistance;
    }
}
