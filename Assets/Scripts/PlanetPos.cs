using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetPos : MonoBehaviour {
    private readonly int planet;
    private List<Parsed> parsedPositions = new List<Parsed>();

    public PlanetPos(int planet,TextAsset textAsset)
    {
        this.planet = planet;
        string[] lines = textAsset.text.Split('\n');
        for(int i = 1; i < lines.Length - 1; i++)
        {
            string line = lines[i].Trim(new char[]{'\t',' '});
            string[] columns = line.Split('\t');             
            Parsed p = new Parsed
            {
                year = short.Parse(columns[0]),
                day = short.Parse(columns[1]),
                AU = double.Parse(columns[2]),
                ELAT = double.Parse(columns[3]),
                ELON = double.Parse(columns[4]),
                HG_LAT = double.Parse(columns[5]),
                HG_LON = double.Parse(columns[6]),
                HGI_LON = double.Parse(columns[7])
            };
            parsedPositions.Add(p);
            
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    class Parsed
    {
        public short year,day;
        public double AU, ELAT, ELON, HG_LAT, HG_LON, HGI_LON;
    }

     int GetPlanet()
    {
        return this.planet;
    }


}
