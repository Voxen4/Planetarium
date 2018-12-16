using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlanet : MonoBehaviour {

    public Transform planet;
    public float smoothTime = 0.8f;
    public Vector3 smoothSpeed = Vector3.zero;
    public Vector3 offset;
    bool planetHitted = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10000))
            {
                planet = hit.transform;
                planetHitted = true;
            }
            else planetHitted = false;
        }
    }
    
    void LateUpdate()
    {
        if (planetHitted)
        {
            Vector3 targetPosition = planet.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref smoothSpeed, smoothTime);
            transform.LookAt(planet);
        }
        
        
    }

    
}
