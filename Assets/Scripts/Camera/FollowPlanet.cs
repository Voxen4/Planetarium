using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlanet : MonoBehaviour {


    bool clicked;
    public Transform planet;
    float smoothTime = 0.8f;
    Vector3 smoothSpeed = Vector3.zero;
    Vector3 offset;
    Vector3 look;

    private void Start()
    {
        clicked = false;
        
    }

    public void ButtonAction()
    {
        UIManager manager = FindObjectOfType<UIManager>();
        if (clicked && manager.trackedPlanet == this.planet.gameObject)
        {
            clicked = false;
            manager.trackedPlanet = null;
        }
        else if(!clicked && manager.trackedPlanet == null)
        {
            manager.trackedPlanet = this.planet.gameObject;
            clicked = true;
        }
    }

    void LateUpdate()
    {
        if (clicked)
        {
            Vector3 targetPosition = planet.position + new Vector3 (20,20,20);
            Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, targetPosition, ref smoothSpeed, smoothTime);
            Camera.main.transform.LookAt(planet);
        }

    }


}
