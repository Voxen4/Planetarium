using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractionManager : MonoBehaviour {
    public static float SPEED = 1;

    public static List<Attractor> attractors;
	// Use this for initialization
	void Start () {
        attractors = new List<Attractor>();
        attractors.AddRange(FindObjectsOfType<Attractor>());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		foreach(Attractor attractor in attractors)
        {
            foreach(Attractor toAttract in attractors)
            {
                if(attractor != toAttract)
                {
                    attractor.Attract(toAttract);
                }
            }
        }
	}
}
