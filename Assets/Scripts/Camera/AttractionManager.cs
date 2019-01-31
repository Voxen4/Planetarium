using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractionManager : MonoBehaviour {
    public static double SPEED = 0.0001;//6.67408*Math.Pow(10, -11);
    public double dt = 0.0000000000000000000001;
    public static List<Attractor> attractors;
	// Use this for initialization
	void Start () {
        attractors = new List<Attractor>();
        attractors.AddRange(FindObjectsOfType<Attractor>());
	}
    
	// Update is called once per frame
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

    private void setPositions()
    {
        foreach (Attractor attractor in attractors)
        {
            Rigidbody rb = attractor.GetComponent<Rigidbody>();
            rb.transform.position = (Vector3) attractor.GetComponent<PlanetModel>().position;
        }
    }


    private void setDiffEquations()
    {
        Vector3d[] tempPosition = new Vector3d[attractors.Count];
        Vector3d[] ka1 = new Vector3d[attractors.Count];
        Vector3d[] ka2 = new Vector3d[attractors.Count];
        Vector3d[] ka3 = new Vector3d[attractors.Count];
        Vector3d[] ka0 = new Vector3d[attractors.Count];
        Vector3d[] kv1 = new Vector3d[attractors.Count];
        Vector3d[] kv2 = new Vector3d[attractors.Count];
        Vector3d[] kv3 = new Vector3d[attractors.Count];
        Vector3d[] kv0 = new Vector3d[attractors.Count];

        rungeKutta1(kv0, ka0);
        rungeKutta2(kv0, kv1, ka0, tempPosition);
        rungeKutta3(kv1, kv2, ka1, tempPosition);
        rungeKutta4(kv2, kv3, ka2, ka3, tempPosition);
        calculateAll(kv0, kv1, kv2, kv3, ka0, ka1, ka2, ka3);


    }

    private void rungeKutta1(Vector3d[] kv0, Vector3d[] ka0)
    {

        calculateAcceleration();

        for (int i = 0; i < attractors.Count; i++)
        {
            PlanetModel o = attractors[i].GetComponent<PlanetModel>();
            kv0[i] = dt * o.velocity;
            ka0[i] = dt * o.acceleration;
        }
    }

    private void rungeKutta2(Vector3d[] kv0, Vector3d[] kv1, Vector3d[] ka0, Vector3d[] tempPosition)
    {

        for (int i = 0; i < attractors.Count; i++)
        {
            PlanetModel o = attractors[i].GetComponent<PlanetModel>();
            kv1[i] = dt * (o.velocity + ka0[i]/2);
        }
        for (int i = 0; i < attractors.Count; i++)
        {
            PlanetModel o = attractors[i].GetComponent<PlanetModel>();
            tempPosition[i] = o.position + kv0[i]/2;
        }
        calculateAccelerationAtPosition(tempPosition);
    }

    private void rungeKutta3(Vector3d[] kv1, Vector3d[] kv2, Vector3d[] ka1, Vector3d[] tempPosition)
    {
        for (int i = 0; i < attractors.Count; i++)
        {
            PlanetModel o = attractors[i].GetComponent<PlanetModel>();
            ka1[i] = dt * o.acceleration;
        }

        for (int i = 0; i < attractors.Count; i++)
        {
            PlanetModel o = attractors[i].GetComponent<PlanetModel>();
            kv2[i] = dt * (o.velocity + ka1[i]/2);
        }
        for (int i = 0; i < attractors.Count; i++)
        {
            PlanetModel o = attractors[i].GetComponent<PlanetModel>();
            tempPosition[i] = o.position + kv1[i]/2;
        }
        calculateAccelerationAtPosition(tempPosition);
    }

    private void rungeKutta4(Vector3d[] kv2, Vector3d[] kv3, Vector3d[] ka2, Vector3d[] ka3,Vector3d[] tempPosition)
    {
        for (int i = 0; i < attractors.Count; i++)
        {
            PlanetModel o = attractors[i].GetComponent<PlanetModel>();
            ka2[i] = dt * o.acceleration;
        }
        for (int i = 0; i < attractors.Count; i++)
        {
            PlanetModel o = attractors[i].GetComponent<PlanetModel>();
            kv3[i] = dt * (o.velocity + ka2[i]);
        }
        for (int i = 0; i < attractors.Count; i++)
        {
            PlanetModel o = attractors[i].GetComponent<PlanetModel>();
            tempPosition[i] = o.position + kv2[i];
        }
        calculateAccelerationAtPosition(tempPosition);

        for (int i = 0; i < attractors.Count; i++)
        {
            PlanetModel o = attractors[i].GetComponent<PlanetModel>();
            ka3[i] = dt * o.acceleration;
        }
    }

    private void calculateAll(Vector3d[] kv0, Vector3d[] kv1, Vector3d[] kv2, Vector3d[] kv3, Vector3d[] ka0, Vector3d[] ka1, Vector3d[] ka2, Vector3d[] ka3)
    {

        for (int i = 0; i < attractors.Count; i++)
        {
            PlanetModel o = attractors[i].GetComponent<PlanetModel>();
            o.position = o.position + 1/6.0 * (kv0[i] + 2 * kv1[i] + 2 * kv2[i] + kv3[i]);
            o.velocity = o.velocity + 1/6.0 * (ka0[i] + 2 * ka1[i] + 2 * ka2[i] + ka3[i]);
        }
    }

    private void setForceToZero()
    {
        foreach (Attractor attractor in attractors)
            attractor.GetComponent<PlanetModel>().force = Vector3d.zero;
    }

    void calculateAccelerationAtPosition(Vector3d[] position)
    {
        setForceToZero();
        for (int i = 0; i < attractors.Count; i++)
        {
            for (int j = i + 1; j < attractors.Count; j++)
            {
                PlanetModel a = attractors[i].GetComponent<PlanetModel>(), b = attractors[j].GetComponent<PlanetModel>();
                double distance = Vector3d.Distance(position[i], position[j]);
                double force = SPEED * (a.mass * b.mass) / (distance * distance);
                a.force += (position[j] - position[i]) * force / distance;
                b.force -= a.force;
            }
        }
        for (int i = 0; i < attractors.Count; i++)
        {
            attractors[i].GetComponent<PlanetModel>().acceleration = attractors[i].GetComponent<PlanetModel>().force /
            attractors[i].GetComponent<PlanetModel>().mass;
        }
    }

    void calculateAcceleration()
    {
        setForceToZero();

        for (int x = 0; x < attractors.Count; x++)
        {
            for (int y = x + 1; y < attractors.Count; y++)
            {
                PlanetModel a = attractors[x].GetComponent<PlanetModel>(), b = attractors[y].GetComponent<PlanetModel>();
                double distance = Vector3d.Distance(a.position, b.position);
                double F = SPEED * (a.mass * b.mass) / (distance * distance);
                a.force += (b.position - a.position) * F / distance;
                b.force -= a.force;
            }
        }
        for (int i = 0; i < attractors.Count; i++)
        {
            attractors[i].GetComponent<PlanetModel>().acceleration = attractors[i].GetComponent<PlanetModel>().force /
            attractors[i].GetComponent<PlanetModel>().mass;
        }
    }
    
}
