using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour {
    
 public Dropdown dayDrop;
 public PlanetPos.Date date;
 public Dropdown yearDrop;
	    public UIManager UI;
	// Use this for initialization
	void Start () {
		UI.GetComponentInChildren<Canvas>().enabled = false;
         dayDrop.onValueChanged.AddListener(delegate {
         dayDropValueChangedHandler(dayDrop);
     });
         yearDrop.onValueChanged.AddListener(delegate {
         yearDropValueChangedHandler(yearDrop);
     });
	}

    private void yearDropValueChangedHandler(Dropdown yearDrop)
    {
        date._year= (Int16)(1998+yearDrop.value);
        setPositions(date);
    }

    private void dayDropValueChangedHandler(Dropdown dayDrop)
    {
        date._day= (Int16)(dayDrop.value+1);
        setPositions(date);
    }

    private void setPositions(PlanetPos.Date date)
    {
       
        foreach (int i in System.Enum.GetValues(typeof(Planet)))
        {
            Planet planet = (Planet)Enum.ToObject(typeof(Planet),i);
            PlanetPos pos = StartUp.getInitPosition(planet);
            PlanetPos.Parsed parsed = pos.GetParsed(date);
            //TODO Set Pos from parsed Positions
        };
    }

    public void TogglePauseMenu()
    {
         // not the optimal way but for the sake of readability
        if (UI.GetComponentInChildren<Canvas>().enabled)
        {
            UI.GetComponentInChildren<Canvas>().enabled = false;
            Time.timeScale = 1.0f;
        }
        else
        {
            UI.GetComponentInChildren<Canvas>().enabled = true;
            Time.timeScale = 0f;
        }
    }
    
    // Update is called once per frame
    void Update () {
		
	}
}
