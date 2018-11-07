using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractionManager : MonoBehaviour {

    public Attractor[] attractors;
	// Use this for initialization
	void Start () {
        attractors = FindObjectsOfType<Attractor>();
	}
	
	// Update is called once per frame
	void Update () {
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
