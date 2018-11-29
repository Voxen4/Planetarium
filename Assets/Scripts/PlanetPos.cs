using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetPos : MonoBehaviour {
    public readonly int planet;
    private Dictionary<Date,Parsed> parsedPositions = new Dictionary<Date,Parsed>();

    public PlanetPos(int planet,TextAsset textAsset)
    {
        this.planet = planet;
        string[] lines = textAsset.text.Split('\n');
        for(int i = 1; i < lines.Length - 1; i++)
        {
            string line = lines[i].Trim(new char[]{'\t',' '});
            string[] columns = line.Split('\t');             
            Parsed p = new Parsed();
            p.date._year = short.Parse(columns[0]);
            p.date._day = short.Parse(columns[1]);
            p.AU = double.Parse(columns[2]);
            p.ELAT = double.Parse(columns[3]);
            p.ELON = double.Parse(columns[4]);
            p.HG_LAT = double.Parse(columns[5]);
            p.HG_LON = double.Parse(columns[6]);
            p.HGI_LON = double.Parse(columns[7]);
            parsedPositions.Add(p.date,p);
            
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public class Date{    
        public short _year,_day;
     }
    public class Parsed
    {
        public Date date;
        public double AU, ELAT, ELON, HG_LAT, HG_LON, HGI_LON;
    }

     int GetPlanet()
    {
        return this.planet;
    }

    public Parsed GetParsed(Date date)
    {
        Parsed parsedValue;
        if (parsedPositions.ContainsKey(date))
        {
            parsedPositions.TryGetValue(date,out parsedValue);
            return parsedValue;
        }
        else
        {
            return null;
        }
    }

}
