using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetData : MonoBehaviour {
    static float masseUmrechnung = (float)  5.9722f * Mathf.Pow(10,24); //in kg, Masse Erde
    static float distanceUmrechnung = 1000000000;    //in km, Entfernung Erde,Sonne
    static float gravitationskonstante =6.67408f * Mathf.Pow(10, -11);
    static float vUmrechnung = Mathf.Sqrt(996461570) / 50;
    //Alle Public Attribute sind angaben die vom Benutzer eingegeben werden müssen.
    //Das System in dem sich der Planet befindet.
    //Ist wichtig um die initale Geschwindigkeit des Planeten zu berrechnen.
    public PlanetData bezugssystem;
    //Die Masse des Planeten in kg;
    public float mass;             
    //Der Durchmesser des Planeten.
    public float diameter;        
    //LargeSemiAxis (in AE)
    public float semiMajorAxis;     
    //Excetricity
    public float excentricity;
    //Inclination in Grad
    public float inclination;

    //Private Attribute sind umgerechnete public Attribute. Mit diese wird später gerechnet
    private float aphelHeightSim;
    private float semiMajorAxisSim;
    private float massSim;


    public void Awake()
    {
        aphelHeightSim = semiMajorAxis * (1 + excentricity) * (149597870700 / distanceUmrechnung);
        semiMajorAxisSim = semiMajorAxis * (149597870700 / distanceUmrechnung);
        massSim = mass / masseUmrechnung;
        this.transform.position = new Vector3(Mathf.Cos(inclination / 180f * Mathf.PI) * aphelHeightSim, Mathf.Sin(inclination / 180f * Mathf.PI) * aphelHeightSim, this.transform.position.z);
    }

    public void Start()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        //this.transform.position = new Vector3(Mathf.Cos(inclination/180f*Mathf.PI) * aphelHeightSim, Mathf.Sin(inclination / 180f * Mathf.PI) * aphelHeightSim, this.transform.position.z); 
        rb.mass = massSim;
        float apohelSpeed = Mathf.Sqrt(bezugssystem.getMassSim() * ((2 / aphelHeightSim) - (1 / semiMajorAxisSim)));
        //Wegen error im Debug.Log
        if(!float.IsNaN(apohelSpeed))
        {
            rb.velocity = new Vector3(0, 0, apohelSpeed);
        }
    }

    public float getMassSim()
    {
        return massSim;
    }
}
