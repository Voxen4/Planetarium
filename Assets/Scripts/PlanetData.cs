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
    //Longitude Of Ascending Node in deg (mit Uhrzeigersinn), Winkel zwischen Ascending Node des Orbits und einer festen Referenzrichtung entlang der Referenzebene.
    public float longitudeOfAscendingNode;
    //Argument of Periapsis in deg (mit Uhrzeigersinn): Winkel zwischen Ascending Node und Periapsis des Orbits entlang der Bahnebene.
    public float longitudeOfPeriapsis;

    //Private Attribute sind umgerechnete public Attribute. Mit diese wird später gerechnet
    private float apoapsisHeightSim;
    private float periapsisHeightSim;
    private float semiMajorAxisSim;
    private float massSim;

    private Vector3 periapsis;
    private Vector3 ascendingNode;
    public void Awake()
    {
        apoapsisHeightSim = semiMajorAxis * (1 + excentricity) * (149597870700 / distanceUmrechnung);
        periapsisHeightSim = semiMajorAxis *(1 - excentricity) * (149597870700 / distanceUmrechnung);
        semiMajorAxisSim = semiMajorAxis * (149597870700 / distanceUmrechnung);
        massSim = mass / masseUmrechnung;
        //argumentOfPeriapsis += 180;
        //this.transform.position = new Vector3(Mathf.Cos(inclination / 180f * Mathf.PI) * aphelHeightSim, Mathf.Sin(inclination / 180f * Mathf.PI) * aphelHeightSim, this.transform.position.z);

        //Bestimmen der Ascending Node. Die AscendingNode befindet sich auf der XZ ebene mit einem Winkel longitude Of Ascending Node von einem festen Bezugspunkt
        //Quaternion ist eine Drehung von longitudeOfAscendingNode Grad im den Vektor (0,1,0) (Y-Achse) des Vektors (1,0,0) mit der länge aphelHeightSim;
        ascendingNode = Quaternion.AngleAxis(longitudeOfAscendingNode, new Vector3(0, 1, 0)) * new Vector3(1,0,0) * periapsisHeightSim;
        //Vektor der auf der Bahnebene des Planeten Liegt. Er ist orthogonal zu ascendingNode.
        Vector3 ascendingPlanePoint = new Vector3(-ascendingNode.z / ascendingNode.x, 0, 1);
        //der um inclination um den Vektor ascendingNode gedreht wird.
        ascendingPlanePoint = Quaternion.AngleAxis(inclination, ascendingNode) * ascendingPlanePoint;
        //Bestimmung eines Orthogonalen Vektors zu der Ebene die von ascendingNode und ascendingPlanePoint aufgespannt wird.
        Vector3 orthogonalToInclinationPlane = new Vector3(ascendingNode.y * ascendingPlanePoint.z - ascendingNode.z * ascendingPlanePoint.y,
                                                           ascendingNode.z * ascendingPlanePoint.x - ascendingNode.x * ascendingPlanePoint.z,
                                                           ascendingNode.x * ascendingPlanePoint.y - ascendingNode.y * ascendingPlanePoint.x);
        //Drehung des Punktes ascendingNode um den Winkel argumentOfPeriapsis um die Axe orthogonalToInclinationPlane. Hier befindet sich die Periapsis des Punktes
        //in unserem Fall die Apoapsis weil diese 180° von der periapsis entfernt ist und wir argumentOfPeriapsis += 180 rechenen.
        periapsis = Quaternion.AngleAxis(longitudeOfPeriapsis, orthogonalToInclinationPlane) * ascendingNode;
        this.transform.position = periapsis;
    }

    public void Start()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.mass = massSim;
        float apoapsisSpeed = Mathf.Sqrt(AttractionManager.SPEED * bezugssystem.getMassSim() * ((2 / apoapsisHeightSim) - (1 / semiMajorAxisSim)));
        float periapsisSpeed = Mathf.Sqrt(AttractionManager.SPEED * bezugssystem.getMassSim() * ((2 / periapsisHeightSim) - (1 / semiMajorAxisSim)));
        //Wegen error im Debug.Log
        if (!float.IsNaN(periapsisSpeed))
        {
            rb.velocity = new Vector3(-periapsis.z / periapsis.x, 0, 1).normalized * periapsisSpeed;
        }
    }

    public float getMassSim()
    {
        return massSim;
    }
}
