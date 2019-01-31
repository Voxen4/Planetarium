using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NasaData : MonoBehaviour
{
    public Planet planet;
    public TextAsset asset;
    private Dictionary<Date, Parsed> parsedPositions = new Dictionary<Date, Parsed>();
    // Use this for initialization

    private void Awake()
    {
        string content = asset.text;
        string[] lines = content.Split('\n');
        foreach (var item in lines)
        {
            string[] columns = item.Split(',');
            string[] date = columns[1].Split('-');
            Parsed parsedLine = new Parsed()
            {
                date = new Date(short.Parse(date[0]), short.Parse(date[1]), short.Parse(date[2]) )
                ,
                X = double.Parse(columns[2])
                ,
                Y = double.Parse(columns[3])
                ,
                Z = double.Parse(columns[4])
                ,
                VX = double.Parse(columns[5])
                ,
                VY = double.Parse(columns[6])
                ,
                VZ = double.Parse(columns[7])
            };
            parsedPositions.Add(parsedLine.date,parsedLine);
        }
    }

    public class Date : IEqualityComparer<Date>
    {
        private short day;
        private short year;
        private short month;

        public Date(short _year = 1959, short _month = 1, short _day = 1)
        {
            this.Year=_year;
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

        public bool Equals(Date x, Date y)
        {
            return x.Year == y.Year && x.Month == y.Month && x.Day == y.Day;
        }

        public int GetHashCode(Date obj)
        {
            return obj.Year.GetHashCode() ^  obj.Month.GetHashCode() ^ obj.Day.GetHashCode();
        }
    }
    public class Parsed
    {
        public Date date;
        public double JDTDB, X, Y, Z, VX, VY, VZ, LT, RG, RR;
    }

        public Parsed GetParsed(Date date)
    {
        if(date == null)
        {
            date = new Date();
        }
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
