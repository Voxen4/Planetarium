using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlanet : MonoBehaviour {


    bool clicked;
    public Transform planet;
    public float smoothTime = 0.8f;
    public Vector3 smoothSpeed = Vector3.zero;
    public Vector3 offset;

    private void Start()
    {
        clicked = false;
    }

    public void ButtonAction()
    {
        if (clicked)
        {
            clicked = false;
        }
            
        else clicked = true;
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
