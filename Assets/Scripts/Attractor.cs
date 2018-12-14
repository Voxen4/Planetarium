﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour {

    public void Attract(Attractor toAttract)
    {
        PlanetData toAttractPD = toAttract.GetComponent<PlanetData>();
        PlanetData attractorPD = gameObject.GetComponent<PlanetData>();
        Rigidbody toAttractRB = toAttract.GetComponent<Rigidbody>();
        Rigidbody attractorRB = gameObject.GetComponent<Rigidbody>();

        Vector3 direction = this.transform.position - toAttract.transform.position;
        float distance = direction.magnitude;

        float forceMagnitude = (float) (AttractionManager.SPEED * ((toAttractPD.getMassSim() * attractorPD.getMassSim()) / (distance * distance)));
        Vector3 force = direction.normalized * forceMagnitude;
        toAttractRB.AddForce(force);
    }
}
