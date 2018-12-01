using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetData : MonoBehaviour {
    //Die Masse des Planeten in Erdenmassen. (Das reale >Gewicht in kg muss durch die Masse der Erde geteilt werden)
    public double mass;             
    //Der Durchmesser des Planeten. Sind bis jetzt nur platzhalter.
    public float diameter;        
    //Höhe des höchsten Punkts des Orbits in 10 Mio km. (147.000.000 -> 14,7)
    public double apohelHeight;     
    //Geschwidigkeit des Planeten. BIs jetzt nur Platzhalter (29290m -> 2,9m)
    public double apohelSpeed;      //Geschwindigkeit an Apoapsis in km/s#
    //Der Planet um der das Objekt kreist. Im Sonnensystem ist das die Sonne (solange der Planet kein Mond ist)
    public GameObject sphereOfInfluence;
    //Maximale und Minimale Distanz die der Planet auf seinem Orbit um den Stern erreicht hat. Wird im Mouse-Over angezeigt.
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
