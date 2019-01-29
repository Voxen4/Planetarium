using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetData : MonoBehaviour
{
    static public float masseUmrechnung = (float)5.9722f * Mathf.Pow(10, 24); //in kg, Masse Erde
    static float distanceUmrechnung = 1000000000;    //in km, Entfernung Erde,Sonne
    static float AE = 149597870700f;    //in km = 1 AE
    static float gravitationskonstante = 6.67408f * Mathf.Pow(10, -11);
    static float vUmrechnung = Mathf.Sqrt(996461570) / 50;
    //Alle Public Attribute sind angaben die vom Benutzer eingegeben werden müssen.
    //Das System in dem sich der Planet befindet.
    //Ist wichtig um die initale Geschwindigkeit des Planeten zu berrechnen.
    public PlanetData bezugssystem;
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
    //Starting Point in AU. Daten von NASA Horizon
    public Vector3 startingPoint;

    private Vector3 velocityDir;

    //DEBUG
    public float error;

    //Private Attribute sind umgerechnete public Attribute. Mit diese wird später gerechnet
    private float apoapsisHeightSim;
    private float periapsisHeightSim;
    private float semiMajorAxisSim;
    //private float massSim;

    private Vector3 periapsis;
    private Vector3 ascendingNode;

    /*
     *Die Awake Methode wird benutzt um alle Attribute (außer Velocity) des Planeten in die Simulationsdaten umzurechnen.
     * Sie wird vor der Start Methode aufgerufen.
     */
    public void Awake()
    {
        apoapsisHeightSim = semiMajorAxis * (1 + excentricity) * (AE / distanceUmrechnung);
        periapsisHeightSim = semiMajorAxis * (1 - excentricity) * (AE / distanceUmrechnung);
        semiMajorAxisSim = semiMajorAxis * (AE / distanceUmrechnung);
        Attractor attractor = this.gameObject.GetComponent<Attractor>();
        NasaData nasaData = this.gameObject.GetComponent<NasaData>();
        if (nasaData != null)
        {
            NasaData.Parsed parsed = nasaData.GetParsed(GameMgr.instance.getDate());
            if (parsed != null)
            {
                startingPoint.x = (float)parsed.X;
                startingPoint.y = (float)parsed.Y;
                startingPoint.z = (float)parsed.Z;
            }
        }

        //massSim = attractor.mass / masseUmrechnung;
        //argumentOfPeriapsis += 180;
        //this.transform.position = new Vector3(Mathf.Cos(inclination / 180f * Mathf.PI) * aphelHeightSim, Mathf.Sin(inclination / 180f * Mathf.PI) * aphelHeightSim, this.transform.position.z);

        //Bestimmen der Ascending Node. Die AscendingNode befindet sich auf der XZ ebene mit einem Winkel longitude Of Ascending Node von einem festen Bezugspunkt
        //Quaternion ist eine Drehung von longitudeOfAscendingNode Grad im den Vektor (0,1,0) (Y-Achse) des Vektors (1,0,0) mit der länge aphelHeightSim;
        ascendingNode = Quaternion.AngleAxis(longitudeOfAscendingNode, new Vector3(0, 1, 0)) * new Vector3(1, 0, 0) * periapsisHeightSim;
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

        //NEW
        //Das Zentrum der Ellipse befindet sich 1 * semiMajorAxis von der Periapsis entfernt in Richtung des Brennpunktes (also Sonne). Da Die Sonne sich bei 0/0/0 befindet, muss sie nicht aufgeschrieben werden.
        Vector3 ellipseCenter = periapsis - (periapsis.normalized * semiMajorAxis * (AE / distanceUmrechnung));

        //Starting Point ist in AE angegeben und muss umgerechnet werden.
        startingPoint = startingPoint * (AE / distanceUmrechnung);

        //NUn muss ein Vektor konstruiert werden, der orthogonal zu dem vektor ellipseCenter - startingPoint liegt. Zudem muss der Vektor in der Ebene des Orbits liegen d.h.
        //auch orthogonal zu dem Normalenvektor der Ebene sein.
        Vector3 centerToStartingPoint = startingPoint - ellipseCenter;
        //der andere Vektor der die Ebene aufspannt ist orthogonalToInclinationPlane
        velocityDir = new Vector3(orthogonalToInclinationPlane.y * centerToStartingPoint.z - orthogonalToInclinationPlane.z * centerToStartingPoint.y,
                                          orthogonalToInclinationPlane.z * centerToStartingPoint.x - orthogonalToInclinationPlane.x * centerToStartingPoint.z,
                                          orthogonalToInclinationPlane.x * centerToStartingPoint.y - orthogonalToInclinationPlane.y * centerToStartingPoint.x);


        this.transform.position = startingPoint;

        error = periapsis.magnitude - startingPoint.magnitude;

    }

    public void Start()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.mass = GetComponent<Attractor>().mass;
        /*float apoapsisSpeed = Mathf.Sqrt(AttractionManager.SPEED * bezugssystem.getMassSim() * ((2 / apoapsisHeightSim) - (1 / semiMajorAxisSim)));
        float periapsisSpeed = Mathf.Sqrt(AttractionManager.SPEED * bezugssystem.getMassSim() * ((2 / periapsisHeightSim) - (1 / semiMajorAxisSim)));
        //Wegen error im Debug.Log
        if (!float.IsNaN(periapsisSpeed))
        {
            //Richtung des Vektors anpassen um den Orbit im Uhrzeigersinn zu forcieren.
            //Wenn der Planet auf der linken Seite der Sonne startet, dann muss er sich nach "oben" wegbewegen.
            //Andererseits, wenn der Planet links startet muss er sich nach unten bewegen.
            if (this.transform.position.x < 0)
            {
                rb.velocity = new Vector3(-periapsis.z / periapsis.x, 0, 1).normalized * periapsisSpeed;
            } else
            {
                rb.velocity = new Vector3(periapsis.z / periapsis.x, 0, -1).normalized * periapsisSpeed;
            }
        }*/


        //NEW
        float startSpeed = Mathf.Sqrt((AttractionManager.SPEED * (bezugssystem.getMassSim() + GetComponent<Attractor>().mass)) * ((2 / startingPoint.magnitude) - (1 / semiMajorAxisSim)));
        //Wegen error im Debug.Log
        if (!float.IsNaN(startSpeed))
        {
            if (this.transform.position.z < 0)
            {
                rb.velocity = velocityDir.normalized * startSpeed;
            }
            else
            {
                rb.velocity = -velocityDir.normalized * startSpeed;
            }

        }
    }

    public float getMassSim()
    {
        return GetComponent<Attractor>().mass;
    }
}
