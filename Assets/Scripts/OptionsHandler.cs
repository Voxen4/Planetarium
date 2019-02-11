using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Options Handler Klasse verwaltet die Dropdowns für die Datums Auswahl.
/// </summary>
public class OptionsHandler : MonoBehaviour
{
    public NasaData.Date date = new NasaData.Date();

    public Dropdown dayDrop;
    public Dropdown monthDrop;
    public Dropdown yearDrop;
    /// <summary>
    /// Fügt Listener auf die Dropdowns hinzu so das die Werte Verarbeitet werden können und als initial Datum im GameMgr singelton gesetzt werden
    /// </summary>
    void Start()
    {
        dayDrop.onValueChanged.AddListener(delegate { dayDropValueChangedHandler(dayDrop); });
        monthDrop.onValueChanged.AddListener(delegate { monthDropValueChangedHandler(monthDrop); });
        yearDrop.onValueChanged.AddListener(delegate { yearDropValueChangedHandler(yearDrop); });
        ((GameMgr)GameMgr.Instance).setDate(new NasaData.Date());
    }

    public void yearDropValueChangedHandler(Dropdown yearDrop)
    {
        short year = (short)(1959 + yearDrop.value);
        date = new NasaData.Date(year, date.Month, date.Day);
        ((GameMgr)GameMgr.Instance).setDate(date);
    }


    public void monthDropValueChangedHandler(Dropdown monthDrop)
    {
        short month = (short)(monthDrop.value);
        date = new NasaData.Date(date.Year, month, date.Day);
        ((GameMgr)GameMgr.Instance).setDate(date);
    }

    public void dayDropValueChangedHandler(Dropdown dayDrop)
    {
        short day = (short)(dayDrop.value + 1);
        date = new NasaData.Date(date.Year, date.Month, day);
        ((GameMgr)GameMgr.Instance).setDate(date);
    }
}
