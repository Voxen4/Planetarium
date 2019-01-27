using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RelativeAttributesTo : MonoBehaviour {
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
    void Start () {
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
    void Update () {
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
    }
    string PrintValues()
    {
        
        if(FileWriter == null)
        {
             FileWriter = File.AppendText(fileName);
        }
        string s =  this.gameObject.name + " " +  RelativePos.ToString() + "	" +  RelativeVel.ToString()+ "	" +  ApoapsisHeight.ToString()
            + "	" +  PeriapsisHeight.ToString()+ "	" +  CurrentDistance.ToString()+ "	" +  StartTheta.ToString()+ "	" +  StartPhi.ToString()+ "	" +  currentTheta
            + "	" +  currentPhi.ToString()+ "	" +  maxDistanceAngleTheta.ToString()+ "	" +   maxDistanceAnglePhi.ToString()+ "	" +  minDistanceAngleTheta.ToString()
            + "	" +  minDistanceAnglePhi.ToString()+ "	" +  period.ToString()+ "	" +  spin.ToString()+ "	" +  lastPos.ToString()+ "	" +  lastPhi.ToString()+ "	" +  periodSum.ToString();
                FileWriter.WriteLine(s);
        return s; 
        
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
            Debug.Log(PrintValues());
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
            Debug.Log(PrintValues());
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
            Debug.Log(PrintValues());
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
            Debug.Log(PrintValues());
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
            Debug.Log(PrintValues());
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
            Debug.Log(PrintValues());
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
            Debug.Log(PrintValues());
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
            Debug.Log(PrintValues());
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
            Debug.Log(PrintValues());
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
            Debug.Log(PrintValues());
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
            Debug.Log(PrintValues());
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
            Debug.Log(PrintValues());
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
            Debug.Log(PrintValues());
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
            Debug.Log(PrintValues());
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
            Debug.Log(PrintValues());
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
            Debug.Log(PrintValues());
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
            Debug.Log(PrintValues());
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
            Debug.Log(PrintValues());
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
            Debug.Log(PrintValues());
        }
    }
}
