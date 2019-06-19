using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    public float distance = 20; //distance from the camera the object will be

    void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance); //gets the mouse input x and y and stores it in this vector3
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);//gets the new vector3 from the mousePosition
        transform.position = objPosition;//moves the object to the new position
    }
}
