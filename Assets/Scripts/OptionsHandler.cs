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
        GameMgr.instance.setDate(new NasaData.Date());
	}
	    
    public void yearDropValueChangedHandler(Dropdown yearDrop)
    {
        date._year= (short)(1998+yearDrop.value);
        GameMgr.instance.setDate(date);
    }

    
    public void monthDropValueChangedHandler(Dropdown monthDrop)
    {
        date.month= (short)(monthDrop.value);
        GameMgr.instance.setDate(date);
    }

    public void dayDropValueChangedHandler(Dropdown dayDrop)
    {
        date._day= (short)(dayDrop.value+1);
        GameMgr.instance.setDate(date);
    }
}
