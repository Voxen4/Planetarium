using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsHandler : MonoBehaviour {
 public PlanetPos.Date date = new PlanetPos.Date();
    
 public Dropdown dayDrop;
 public Dropdown yearDrop;
	// Use this for initialization
	void Start () {
		dayDrop.onValueChanged.AddListener(delegate{dayDropValueChangedHandler(dayDrop); });
		yearDrop.onValueChanged.AddListener(delegate{yearDropValueChangedHandler(yearDrop); });
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
    public void yearDropValueChangedHandler(Dropdown yearDrop)
    {
        date._year= (short)(1998+yearDrop.value);
        GameMgr.instance.setPositions(date);
    }

    public void dayDropValueChangedHandler(Dropdown dayDrop)
    {
        date._day= (short)(dayDrop.value+1);
        GameMgr.instance.setPositions(date);
    }
}
