using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class StartUp : MonoBehaviour {
    public static List<PlanetPos> initalPositions = new List<PlanetPos>();
    static StartUp()
    {
        Debug.Log("Up and running");
        foreach (int i in System.Enum.GetValues(typeof(Planet)))
{
            string filename = "Planet Positions/planet_"+System.Enum.GetName(typeof(Planet),i);
        TextAsset asset = Resources.Load(filename) as TextAsset;
        PlanetPos pos = new PlanetPos(i,asset);
        initalPositions.Add(pos);
}
    }
	
	// Update is called once per frame
	static void Update () {
		
	}

    public static PlanetPos getInitPosition(Planet planet)
    {
      foreach (var pos in initalPositions) {
        if((Planet)System.Enum.ToObject(typeof(Planet),pos.planet) == planet)
            {
                return pos;
            }
        }
      return null;
    }
}

public enum Planet
{
    EARTH,JUPITER,MARS,MERCURY,NEPTUNE,SATURN,URANUS,VENUS
}
