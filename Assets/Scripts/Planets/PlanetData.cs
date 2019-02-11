using UnityEngine;

public class PlanetData : MonoBehaviour
{
    static public float masseUmrechnung = (float)5.9722f * Mathf.Pow(10, 24); //in kg, Masse Erde
    static public float distanceUmrechnung = 1000000000;    //in km, Entfernung Erde,Sonne
    static public float AE = 149597870700f;    //in km = 1 AE
    static public float gravitationskonstante = 6.67408f * Mathf.Pow(10, -11);
    public static float startSpeed;
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

    /*
     *Die Awake Methode wird benutzt um alle Attribute (außer Velocity) des Planeten in die Simulationsdaten umzurechnen.
     * Sie wird vor der Start Methode aufgerufen.
     */
    public void Awake()
    {
        float semiMajorAxisSim = semiMajorAxis * (AE / distanceUmrechnung);
        NasaData nasaData = this.gameObject.GetComponent<NasaData>();
        if (nasaData != null)
        {
            NasaData.Parsed parsed = nasaData.GetParsed(((GameMgr)GameMgr.Instance).getDate());
            if (parsed != null)
            {
                //Debug.Log(this.name + ": " + (float)parsed.X + "," + (float)parsed.Y + ","+ (float)parsed.Z);
                startingPoint.x = (float)parsed.X;
                startingPoint.y = (float)parsed.Y;
                startingPoint.z = (float)parsed.Z;
            }
        }
        startingPoint = startingPoint * (AE / distanceUmrechnung);
        //Berechnung einer Ebene die die Schnittgrade ascendingNodeDir und einen Winkel inclination mit der Referenzebene hat.
        Vector3 ascendingNodeDir = Quaternion.AngleAxis(longitudeOfAscendingNode,Vector3.up) * Vector3.right;
        Vector3 inclinationPoint = Quaternion.AngleAxis(90, Vector3.up) * ascendingNodeDir;
        inclinationPoint = Quaternion.AngleAxis(inclination, ascendingNodeDir) * inclinationPoint;
        Vector3 orthogonalToPlane = new Vector3(ascendingNodeDir.y * inclinationPoint.z - ascendingNodeDir.z * inclinationPoint.y,
                                                   ascendingNodeDir.z * inclinationPoint.x - ascendingNodeDir.x * inclinationPoint.z,
                                                   ascendingNodeDir.x * inclinationPoint.y - ascendingNodeDir.y * inclinationPoint.x);

        //Die Periapsis die sich auf der Major-Axis der Ellipse befindet, wird durch den Winkel longitudeOfPeriapsis angegeben.
        //Dieser beschreibt den eingeschlossenen Winkel zwischen ascendingNodeDir und der Richtung der Periapsis
        //Also drehen wir einfach den Vector ascendingNodeDir um den Winkel longitudeOfPeriapsis um den Normalenvektor der Ebene.
        Vector3 periapsisDir = Quaternion.AngleAxis(longitudeOfPeriapsis, orthogonalToPlane) * ascendingNodeDir;
        //Da sich der Brennpunkt 1 im Ursprung befindet können wir einfach das Zentrum der Ellipse und den zweiten Brennpunkt berechnen.
        //Periapsis, Ursprung, Zentrum der Ellipse und der zweite Brennpunkt befindet sich alle auf einer Graden.
        Vector3 ellipseCenter = -periapsisDir.normalized * excentricity * semiMajorAxisSim;
        Vector3 focus2 = ellipseCenter + -periapsisDir.normalized * excentricity * semiMajorAxis;

        //Jetzt muss nur noch die Richtung und Stärke der initialen geschwindigkeit berechnet werden.
        //Die Richtung der Bewegung ist eine Tangente der Ellipse.
        //dazu berechnen wir erst zwei Vektoren: vec1 ist die relative Position der Startposition zum Brennpunkt1 (also dem Ursprung) und vec2 ist die relative Position der Startposition zum Brennpunkt 2.
        //Die Orthogonale der Winkelhalbierenden zwischen diesen beiden Vectoren ist der Richtungsvector der Tangente der Ellipse und damit die Richtung der initialen Bewegung.
        Vector3 vec1 = startingPoint;
        Vector3 vec2 = startingPoint - focus2;
        float angle = Vector3.Angle(vec1, vec2);
        Vector3 mitteldiagonale = Vector3.RotateTowards(vec1, vec2, angle / 2 / 360 * 2 * Mathf.PI, 1f);
        Vector3 velDir = Quaternion.AngleAxis(90, orthogonalToPlane) * mitteldiagonale;

        this.transform.position = startingPoint;

        //Im letzten schritt wird nur noch die Sträke der Anfangsgeschwindigkeit bestimmt.
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.mass = GetComponent<Attractor>().mass;
        startSpeed = Mathf.Sqrt(((float)AttractionManager.SPEED * (bezugssystem.getMassSim() + GetComponent<Attractor>().mass)) * ((2 / startingPoint.magnitude) - (1 / semiMajorAxisSim)));
        if (!float.IsNaN(startSpeed))
        {
            rb.velocity = -velDir.normalized * startSpeed;
        }

    }

    public float getMassSim()
    {
        return GetComponent<Attractor>().mass;
    }
}
