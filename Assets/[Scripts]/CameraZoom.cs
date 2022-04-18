using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    int zoom = 20;
    int reset = 60;
    float smooth = 5;
    private bool isZoomed = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))        
            isZoomed = !isZoomed;
        
        if (isZoomed)
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, zoom, Time.deltaTime * smooth);
        else
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, reset, Time.deltaTime * smooth);
    }
}
//Vector3 direction = Point - transform.position;
//Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);
//transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.time);