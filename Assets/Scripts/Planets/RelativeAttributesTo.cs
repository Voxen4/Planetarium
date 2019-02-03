using System.IO;
using UnityEngine;

public class RelativeAttributesTo : MonoBehaviour
{

    private float tagecounter = 0;
    private float tickDays = 0.183272672F;

    public PlanetData relativeTo;

    private Vector3 relativePos;
    private Vector3 relativeVel;

    private float apoapsisHeight;
    private float periapsisHeight;

    private float currentDistance;

    private float startTheta; //geographische Breite, Horizontale /Planebene
    private float startPhi; //geographische Länge, Vertikale / Höhe

    private float currentTheta;
    private float currentPhi;

    private float maxDistanceAngleTheta;
    private float maxDistanceAnglePhi;

    private float minDistanceAngleTheta;
    private float minDistanceAnglePhi;

    private float period;
    private float spin;

    private float lastPos;
    private float lastPhi;
    private float periodSum;

    private string fileName;
    private StreamWriter FileWriter;

    // Use this for initialization
    void Start()
    {
        fileName = this.gameObject.name + ".txt";
        if (!File.Exists(fileName))
        {
            FileWriter = File.CreateText(fileName);
            FileWriter.AutoFlush = true;
            FileWriter.WriteLine("RelativeTo	RelativePos	RelativeVel	ApoapsisHeight	PeriapsisHeight	CurrentDistance	StartTheta	StartPhi	currentTheta	currentPhi	maxDistanceAngleTheta	maxDistanceAnglePhi	minDistanceAngleTheta	minDistanceAnglePhi	period	spin	lastPos	lastPhi	periodSum");
        }
        else
        {
            FileWriter = File.AppendText(fileName);
            FileWriter.AutoFlush = true;
        }
        RelativePos = RelativeTo.gameObject.transform.position - this.transform.position;
        RelativeVel = RelativeTo.GetComponent<Rigidbody>().velocity - this.GetComponent<Rigidbody>().velocity;
        StartTheta = Mathf.Asin(RelativePos.y / RelativePos.magnitude);
        StartPhi = Mathf.Atan2(RelativePos.z, RelativePos.x);

        ApoapsisHeight = RelativePos.magnitude;
        MaxDistanceAngleTheta = StartTheta;
        MaxDistanceAnglePhi = StartPhi;

        PeriapsisHeight = RelativePos.magnitude;
        MinDistanceAngleTheta = StartTheta;
        MinDistanceAngleTheta = StartPhi;

    }


    // Update is called once per frame
    void FixedUpdate()
    {

        RelativePos = RelativeTo.gameObject.transform.position - this.transform.position;
        RelativeVel = RelativeTo.GetComponent<Rigidbody>().velocity - this.GetComponent<Rigidbody>().velocity;

        CurrentDistance = RelativePos.magnitude;

        CurrentTheta = Mathf.Asin(RelativePos.y / RelativePos.magnitude);
        CurrentPhi = Mathf.Atan2(RelativePos.z, RelativePos.x);

        if (RelativePos.magnitude > ApoapsisHeight)
        {
            ApoapsisHeight = RelativePos.magnitude;
            MaxDistanceAngleTheta = CurrentTheta;
            MaxDistanceAnglePhi = CurrentPhi;
        }
        else if (RelativePos.magnitude < PeriapsisHeight)
        {
            PeriapsisHeight = RelativePos.magnitude;
            MinDistanceAngleTheta = CurrentTheta;
            MinDistanceAnglePhi = CurrentPhi;
        }

        PeriodSum += Time.deltaTime;
        if (LastPhi > CurrentPhi)
        {
            Period = PeriodSum;
            PeriodSum = 0;
        }

        LastPhi = CurrentPhi;

        tagecounter += tickDays;

    }
    void PrintValues()
    {

        if ((tagecounter > (1 - (tickDays / 2))) && tagecounter < (1 + (tickDays / 2)))
        {
            tagecounter -= 1;
            if (FileWriter == null)
            {
                FileWriter = File.AppendText(fileName);
            }
            string s = this.gameObject.name + " " + RelativePos.ToString() + "	" + RelativeVel.ToString() + "	" + ApoapsisHeight.ToString()
                + "	" + PeriapsisHeight.ToString() + "	" + CurrentDistance.ToString() + "	" + StartTheta.ToString() + "	" + StartPhi.ToString() + "	" + currentTheta
                + "	" + currentPhi.ToString() + "	" + maxDistanceAngleTheta.ToString() + "	" + maxDistanceAnglePhi.ToString() + "	" + minDistanceAngleTheta.ToString()
                + "	" + minDistanceAnglePhi.ToString() + "	" + period.ToString() + "	" + spin.ToString() + "	" + lastPos.ToString() + "	" + lastPhi.ToString() + "	" + periodSum.ToString();
            FileWriter.WriteLine(s);
            //return s;
        }

    }

    public PlanetData RelativeTo
    {
        get
        {
            return relativeTo;
        }

        set
        {
            relativeTo = value;
            PrintValues();
            //Debug.Log(PrintValues());
        }
    }

    public Vector3 RelativePos
    {
        get
        {
            return relativePos;
        }

        set
        {
            relativePos = value;
            PrintValues();
            //Debug.Log(PrintValues());
        }
    }

    public Vector3 RelativeVel
    {
        get
        {
            return relativeVel;
        }

        set
        {
            relativeVel = value;
            PrintValues();
            //Debug.Log(PrintValues());
        }
    }

    public float ApoapsisHeight
    {
        get
        {
            return apoapsisHeight;
        }

        set
        {
            apoapsisHeight = value;
            PrintValues();
            //Debug.Log(PrintValues());
        }
    }

    public float PeriapsisHeight
    {
        get
        {
            return periapsisHeight;
        }

        set
        {
            periapsisHeight = value;
            PrintValues();
            //Debug.Log(PrintValues());
        }
    }

    public float CurrentDistance
    {
        get
        {
            return currentDistance;
        }

        set
        {
            currentDistance = value;
            PrintValues();
            //Debug.Log(PrintValues());
        }
    }

    public float StartTheta
    {
        get
        {
            return startTheta;
        }

        set
        {
            startTheta = value;
            PrintValues();
            //Debug.Log(PrintValues());
        }
    }

    public float StartPhi
    {
        get
        {
            return startPhi;
        }

        set
        {
            startPhi = value;
            PrintValues();
            //Debug.Log(PrintValues());
        }
    }

    public float CurrentTheta
    {
        get
        {
            return currentTheta;
        }

        set
        {
            currentTheta = value;
            PrintValues();
            //Debug.Log(PrintValues());
        }
    }

    public float CurrentPhi
    {
        get
        {
            return currentPhi;
        }

        set
        {
            currentPhi = value;
            PrintValues();
            //Debug.Log(PrintValues());
        }
    }

    public float MaxDistanceAngleTheta
    {
        get
        {
            return maxDistanceAngleTheta;
        }

        set
        {
            maxDistanceAngleTheta = value;
            PrintValues();
            //Debug.Log(PrintValues());
        }
    }

    public float MaxDistanceAnglePhi
    {
        get
        {
            return maxDistanceAnglePhi;
        }

        set
        {
            maxDistanceAnglePhi = value;
            PrintValues();
            //Debug.Log(PrintValues());
        }
    }

    public float MinDistanceAngleTheta
    {
        get
        {
            return minDistanceAngleTheta;
        }

        set
        {
            minDistanceAngleTheta = value;
            PrintValues();
            //Debug.Log(PrintValues());
        }
    }

    public float MinDistanceAnglePhi
    {
        get
        {
            return minDistanceAnglePhi;
        }

        set
        {
            minDistanceAnglePhi = value;
            PrintValues();
            //Debug.Log(PrintValues());
        }
    }

    public float Period
    {
        get
        {
            return period;
        }

        set
        {
            period = value;
            PrintValues();
            //Debug.Log(PrintValues());
        }
    }

    public float Spin
    {
        get
        {
            return spin;
        }

        set
        {
            spin = value;
            PrintValues();
            //Debug.Log(PrintValues());
        }
    }

    public float LastPos
    {
        get
        {
            return lastPos;
        }

        set
        {
            lastPos = value;
            PrintValues();
            //Debug.Log(PrintValues());
        }
    }

    public float LastPhi
    {
        get
        {
            return lastPhi;
        }

        set
        {
            lastPhi = value;
            PrintValues();
            //Debug.Log(PrintValues());
        }
    }

    public float PeriodSum
    {
        get
        {
            return periodSum;
        }

        set
        {
            periodSum = value;
            PrintValues();
            //Debug.Log(PrintValues());
        }
    }
}
