using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class sunClicked : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        
        EditorUtility.DisplayDialog("Sun", "Radius: 696.342 km\nVolume: 1,300,000 × Earth\nMass: 333,000 × Earth\nEscape Velocity: 617.7 km/s", "Ok");
        
    }
}
