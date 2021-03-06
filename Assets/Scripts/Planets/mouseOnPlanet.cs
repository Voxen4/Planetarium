﻿using UnityEngine;


/// <summary>
/// Anzeige des Namens, wenn Maus über den Planeten gehalt wird
/// </summary>
public class mouseOnPlanet : MonoBehaviour
{
    private string text;

    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private string currentToolTipText = "";
    private GUIStyle frontStyle;
    private GUIStyle backStyle;
    private Font myFont;

    PlanetData pd;


    
    void Start()
    {
        text = this.gameObject.name;
        myFont = (Font)Resources.Load("Fonts/trench100free", typeof(Font));
        frontStyle = new GUIStyle();
        backStyle = new GUIStyle();
        frontStyle.fontSize = 30;
        backStyle.fontSize = 30;
        frontStyle.font = myFont;
        backStyle.font = myFont;
        frontStyle.normal.textColor = Color.white;
        frontStyle.alignment = TextAnchor.UpperCenter;
        frontStyle.wordWrap = true;
        backStyle.normal.textColor = Color.blue;
        backStyle.alignment = TextAnchor.UpperCenter;
        backStyle.wordWrap = true;

        pd = gameObject.GetComponent<PlanetData>();
    }

    void OnMouseEnter()
    {
        currentToolTipText = text;
    }

    void OnMouseExit()
    {
        currentToolTipText = "";
    }

    void OnGUI()
    {
        if (currentToolTipText != "")
        {
            currentToolTipText = text;// + '\n' + ay.ToString();
            var x = Event.current.mousePosition.x;
            var y = Event.current.mousePosition.y;
            GUI.Label(new Rect(x - 148, y + 40, 500, 60), currentToolTipText, backStyle);
            GUI.Label(new Rect(x - 150, y + 40, 500, 60), currentToolTipText, frontStyle);
        }
    }
}

