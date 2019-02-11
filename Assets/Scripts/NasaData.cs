using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Wrapper Klasse zum Parsen von Nasa Daten und bereitstellen von Funktionen um diese Abhängig vom Datum abzurufen.
/// </summary>
public class NasaData : MonoBehaviour
{
    public Planet planet;
    public TextAsset asset;
    private Dictionary<short, Parsed> parsedPositions = new Dictionary<short, Parsed>();
    // Use this for initialization
    /// <summary>
    /// Beim starten des Scripts werden die Daten des Text Assets geparst.
    /// </summary>
    private void Awake()
    {
        string content = asset.text;
        string[] lines = content.Split('\n');
        foreach (var item in lines)
        {
            string[] columns = item.Split(',');
            string[] date = columns[1].Split('-');
            if (date[1].Equals("01") && date[2].Equals("01"))
            {
                Parsed parsedLine = new Parsed()
                {
                    date = new Date(short.Parse(date[0]))
                    ,
                    X = double.Parse(columns[2])
                    ,
                    Z = double.Parse(columns[3])
                    ,
                    Y = double.Parse(columns[4])
                    ,
                    VX = double.Parse(columns[5])
                    ,
                    VY = double.Parse(columns[6])
                    ,
                    VZ = double.Parse(columns[7])
                };
                parsedPositions.Add(parsedLine.date.Year, parsedLine);
            }
        }
    }

    public class Date
    {
        private short day;
        private short year;
        private short month;

        public Date(short _year = 1959, short _month = 1, short _day = 1)
        {
            this.Year = _year;
            this.Month = _month;
            this.Day = _day;
        }


        public short Year
        {
            get
            {
                return year;
            }

            private set
            {
                year = value;
            }
        }

        public short Month
        {
            get
            {
                return month;
            }

            private set
            {
                month = value;
            }
        }

        public short Day
        {
            get
            {
                return day;
            }

            private set
            {
                day = value;
            }
        }
    }
    public class Parsed
    {
        public Date date;
        public double JDTDB, X, Y, Z, VX, VY, VZ, LT, RG, RR;
    }

    public Parsed GetParsed(Date date)
    {
        if (date == null)
        {
            date = new Date();
        }
        Parsed parsedValue;
        if (parsedPositions.ContainsKey(date.Year))
        {

            //return null;
            parsedPositions.TryGetValue(date.Year, out parsedValue);
            return parsedValue;
        }
        else
        {
            return null;
        }
    }

}
