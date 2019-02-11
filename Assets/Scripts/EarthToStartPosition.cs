using System.Globalization;
using UnityEngine;

/// <summary>
/// Klasse um die Startposition der einzelnen Planeten pro Jahr zu laden und zu erstellen
/// </summary>
public class EarthToStartPosition : MonoBehaviour {

    public GameObject earth;

	// Use this for initialization
	void Start () {
        string filename = "Planet Positions/planet_EARTH";
        TextAsset asset = Resources.Load(filename) as TextAsset;
        var zeilen = asset.text.Split('\n'); 
        var spalten = zeilen[1].Split('\t');
        float au = float.Parse(spalten[2], CultureInfo.InvariantCulture.NumberFormat);// r
        float elat = float.Parse(spalten[3], CultureInfo.InvariantCulture.NumberFormat);// phi
        float elon = float.Parse(spalten[4], CultureInfo.InvariantCulture.NumberFormat);// lambda
        float elatBogen = elat * Mathf.PI / 180;// in Bogenmaß
        float elonBogen = elon * Mathf.PI / 180;// in Bogenmaß
        var xInAu = au * Mathf.Cos(elatBogen) * Mathf.Cos(elonBogen) * 12;
        var yInAu = au * Mathf.Sin(elonBogen) * Mathf.Cos(elatBogen) * 12;
        var zInAu = au * Mathf.Sin(elatBogen) * 12;

        earth.transform.position = new Vector3(xInAu,yInAu,zInAu);
    }

  
}
