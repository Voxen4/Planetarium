using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractionManager : MonoBehaviour {

    public static double SPEED = 0.01;            // 6.67408*Math.Pow(10, -11); eigentliche Gravitationskonstante, Gibt die Geschwindigkeit der Simulation an
    private double dt = 0.1;                      // timestep für die Berechnung der einzelnen Positionen
    public static List<Attractor> attractors;     // Objekte, die sich gegenseitig beeinflussen

    // Use this for initialization
    void Start()
    {
        attractors = new List<Attractor>();
        attractors.AddRange(FindObjectsOfType<Attractor>());
    }

    // Alternative Variante mit der UnityEngine (mgl. Euler-Verfahren)
    /*void FixedUpdate () {
		foreach(Attractor attractor in attractors)
        {
            foreach(Attractor toAttract in attractors)
            {
                if(attractor != toAttract)
                {
                    attractor.Attract(toAttract);
                }
            }
        }
	}*/

    
    private void FixedUpdate()
    {
        setDiffEquations();
        setPositions();
    }


    // Überträgt die Positionen aus dem Modell von Double in Float zur Darstellung
    private void setPositions()
    {
        foreach (Attractor attractor in attractors)
        {
            Rigidbody rb = attractor.GetComponent<Rigidbody>();
            rb.transform.position = (Vector3)attractor.GetComponent<PlanetModel>().position;
        }
    }


    private void setDiffEquations()
    {
        Vector3d[] tempPosition = new Vector3d[attractors.Count];
        Vector3d[] ka0 = new Vector3d[attractors.Count];
        Vector3d[] ka1 = new Vector3d[attractors.Count];
        Vector3d[] ka2 = new Vector3d[attractors.Count];
        Vector3d[] ka3 = new Vector3d[attractors.Count];
        Vector3d[] kv0 = new Vector3d[attractors.Count];
        Vector3d[] kv1 = new Vector3d[attractors.Count];
        Vector3d[] kv2 = new Vector3d[attractors.Count];
        Vector3d[] kv3 = new Vector3d[attractors.Count];
        

        // Euler-Verfahren angewandt
        rungeKutta1(kv0, ka0);
        // 2te Positionsbestimmung durch Mid-Point-Verfahren
        rungeKutta2(kv0, kv1, ka0, tempPosition);
        // 3te Positionsbestimmung durch Mid-Point-Verfahren
        rungeKutta3(kv1, kv2, ka1, tempPosition);
        // 4te Positionsbestimmung 
        rungeKutta4(kv2, kv3, ka2, ka3, tempPosition);


        // Neue Position durch den Durchschnitt berechnen
        for (int i = 0; i < attractors.Count; i++)
        {
            PlanetModel planet = attractors[i].GetComponent<PlanetModel>();
            planet.velocity = planet.velocity + 1.0 / 6.0 * (ka0[i] + 2 * ka1[i] + 2 * ka2[i] + ka3[i]);
            planet.position = planet.position + 1.0 / 6.0 * (kv0[i] + 2 * kv1[i] + 2 * kv2[i] + kv3[i]);
            
        }
    }

    // Euler-Verfahren
    private void rungeKutta1(Vector3d[] kv0, Vector3d[] ka0)
    {
        // Initiale Berechnung der Beschleunigung
        calculateAcceleration(null);

        for (int i = 0; i < attractors.Count; i++)
        {
            PlanetModel planet = attractors[i].GetComponent<PlanetModel>();
            // Berechnet die Veränderung der Position
            kv0[i] = planet.velocity * dt;
            // Berechnet die Veränderung der Geschwindigkeit
            ka0[i] = planet.acceleration * dt;
        }
    }


    private void rungeKutta2(Vector3d[] kv0, Vector3d[] kv1, Vector3d[] ka0, Vector3d[] tempPosition)
    {
        // Berechnet die Veränderung der Position an kv1 + neue temporäre Position
        for (int i = 0; i < attractors.Count; i++)
        {
            PlanetModel planet = attractors[i].GetComponent<PlanetModel>();
            kv1[i] = (planet.velocity + ka0[i] / 2) * dt;
            tempPosition[i] = planet.position + kv0[i] / 2;
        }

        // Berechnen der Beschleunigung an der temporären Position
        calculateAcceleration(tempPosition);
    }

    private void rungeKutta3(Vector3d[] kv1, Vector3d[] kv2, Vector3d[] ka1, Vector3d[] tempPosition)
    {
        // Berechnen der Änderung der Geschwindigkeit und Position sowie neue temporäre Position
        for (int i = 0; i < attractors.Count; i++)
        {
            PlanetModel planet = attractors[i].GetComponent<PlanetModel>();
            ka1[i] = planet.acceleration * dt;
            kv2[i] = (planet.velocity + ka1[i] / 2) * dt;
            tempPosition[i] = planet.position + kv1[i] / 2;

        }

        // Berechnen der Beschleunigung an der temporären Position
        calculateAcceleration(tempPosition);
    }

    private void rungeKutta4(Vector3d[] kv2, Vector3d[] kv3, Vector3d[] ka2, Vector3d[] ka3, Vector3d[] tempPosition)
    {
        // Berechnen der Änderung der Geschwindigkeit und Position sowie neue temporäre Position
        for (int i = 0; i < attractors.Count; i++)
        {
            PlanetModel planet = attractors[i].GetComponent<PlanetModel>();
            ka2[i] = planet.acceleration * dt;
            kv3[i] = (planet.velocity + ka2[i]) * dt;
            tempPosition[i] = planet.position + kv2[i];
        }

        // Berechnen der Beschleunigung an der temporären Position
        calculateAcceleration(tempPosition);

        // Berechnen der letzten Geschwindigkeitsänderung
        for (int i = 0; i < attractors.Count; i++)
        {
            PlanetModel planet = attractors[i].GetComponent<PlanetModel>();
            ka3[i] = planet.acceleration * dt;
        }
    }

    // Setzen der Kraft auf 0
    private void setForceToZero()
    {
        foreach (Attractor attractor in attractors)
            attractor.GetComponent<PlanetModel>().force = Vector3d.zero;
    }

    void calculateAcceleration(Vector3d[] position)
    {
        // Vor der Berechnung muss die Kraft zurückgesetzt werden
        setForceToZero();

        for (int i = 0; i < attractors.Count; i++)
        {
            PlanetModel planet1 = attractors[i].GetComponent<PlanetModel>();
            for (int j = i + 1; j < attractors.Count; j++)
            {
                PlanetModel planet2 = attractors[j].GetComponent<PlanetModel>();

                // Falls eine temporäre Position existiert
                if (position != null)
                {
                    // Berechnen der Distanz zwischen den Positionen der Planeten
                    double distance = Vector3d.Distance(position[i], position[j]);
                    // Berechnen der Kraft durch das Gravitationsgesetz, SPEED ist Ersatz für Gravitationskonstante
                    double F = SPEED * (planet1.mass * planet2.mass) / (Math.Pow(distance, 2));
                    // Planet1 erfährt neue Kraft
                    planet1.force += (position[j] - position[i]) * F / distance;
                }
                else
                {
                    double distance = Vector3d.Distance(planet1.position, planet2.position);
                    double F = SPEED * (planet1.mass * planet2.mass) / (Math.Pow(distance, 2));
                    planet1.force += (planet2.position - planet1.position) * F / distance;
                }
                // Actio und Reactio, drittes newtonsches Axiom
                planet2.force -= planet1.force;
            }
            // Berechnen der korrekten Beschleunigung, zweites newtonsches Axiom
            attractors[i].GetComponent<PlanetModel>().acceleration = attractors[i].GetComponent<PlanetModel>().force / attractors[i].GetComponent<PlanetModel>().mass;

        }
    }
    
}
