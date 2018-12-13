using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativeAttributesTo : MonoBehaviour {
    public PlanetData relativeTo;

    public Vector3 relativePos;
    public Vector3 relativeVel;

    public float maxDistance;
    public float minDistance;

    public float startTheta; //geographische Breite, Horizontale /Planebene
    public float startPhi; //geographische Länge, Vertikale / Höhe

    public float currentTheta;
    public float currentPhi;

    public float maxDistanceAngleTheta;
    public float maxDistanceAnglePhi;

    public float minDistanceAngleTheta;
    public float minDistanceAnglePhi;

    public float period;
    public float spin;


    // Use this for initialization
    void Start () {
        relativePos = relativeTo.gameObject.transform.position - this.transform.position;
        relativeVel = relativeTo.GetComponent<Rigidbody>().velocity - this.GetComponent<Rigidbody>().velocity;
        startTheta = Mathf.Asin(relativePos.y / relativePos.magnitude);
        startPhi = Mathf.Atan2(relativePos.z, relativePos.x);

        maxDistance = relativePos.magnitude;
        maxDistanceAngleTheta = startTheta;
        maxDistanceAnglePhi = startPhi;

        minDistance = relativePos.magnitude;
        minDistanceAngleTheta = startTheta;
        minDistanceAngleTheta = startPhi;

    }

    private float lastPos;
    private float lastPhi;
    private float periodSum;
	// Update is called once per frame
	void Update () {
        relativePos = relativeTo.gameObject.transform.position - this.transform.position;
        relativeVel = relativeTo.GetComponent<Rigidbody>().velocity - this.GetComponent<Rigidbody>().velocity;

        currentTheta = Mathf.Asin(relativePos.y / relativePos.magnitude);
        currentPhi = Mathf.Atan2(relativePos.z, relativePos.x);

        if (relativePos.magnitude > maxDistance)
        {
            maxDistance = relativePos.magnitude;
            maxDistanceAngleTheta = currentTheta;
            maxDistanceAnglePhi = currentPhi;
        }
        else if (relativePos.magnitude < minDistance)
        {
            minDistance = relativePos.magnitude;
            minDistanceAngleTheta = currentTheta;
            minDistanceAnglePhi = currentPhi;
        }

        periodSum += Time.deltaTime;
        if (lastPhi > currentPhi)
        {
            period = periodSum;
            periodSum = 0;
        }

            lastPhi = currentPhi;
    }
}
