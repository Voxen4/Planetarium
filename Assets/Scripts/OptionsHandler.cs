using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsHandler : MonoBehaviour {
 public NasaData.Date date = new NasaData.Date();
    
 public Dropdown dayDrop;
 public Dropdown monthDrop;
 public Dropdown yearDrop;
	// Use this for initialization
	void Start () {
		dayDrop.onValueChanged.AddListener(delegate{dayDropValueChangedHandler(dayDrop); });
		monthDrop.onValueChanged.AddListener(delegate{monthDropValueChangedHandler(monthDrop); });
		yearDrop.onValueChanged.AddListener(delegate{yearDropValueChangedHandler(yearDrop); });
        ((GameMgr)GameMgr.Instance).setDate(new NasaData.Date());
	}
	    
    public void yearDropValueChangedHandler(Dropdown yearDrop)
    {
        short year = (short)(1959+yearDrop.value);
        date = new NasaData.Date(year,date.Month,date.Day);
        ((GameMgr)GameMgr.Instance).setDate(date);
    }

    
    public void monthDropValueChangedHandler(Dropdown monthDrop)
    {
        short month =  (short)(monthDrop.value);
        date = new NasaData.Date(date.Year,month,date.Day);
        ((GameMgr)GameMgr.Instance).setDate(date);
    }

    public void dayDropValueChangedHandler(Dropdown dayDrop)
    {
        short day = (short)(dayDrop.value+1);
        date = new NasaData.Date(date.Year,date.Month,day);
        ((GameMgr)GameMgr.Instance).setDate(date);
    }
}
