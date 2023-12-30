using System;
using System.Collections;

using System.Collections.Generic;

using UnityEngine;


public class DragObject : MonoBehaviour

{
    private Vector3 lastPos;
    private Vector3 actualPos;
    private float forceMultiplier = 300;
    private Rigidbody rb;

    private bool isShoot;
    
    
    private Vector3 mOffset;
    private float mZCoord;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMouseDown()

    {

        mZCoord = Camera.main.WorldToScreenPoint(

            gameObject.transform.position).z;


        // Store offset = gameobject world pos - mouse world pos

        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();

        actualPos = transform.position;

    }
    

    private void OnMouseUp()
    {
        
        Shoot(actualPos - lastPos);
    }


    private Vector3 GetMouseAsWorldPoint()

    {

        // Pixel coordinates of mouse (x,y)

        Vector3 mousePoint = Input.mousePosition;


        // z coordinate of game object on screen

        mousePoint.z = mZCoord;


        // Convert it to world points

        return Camera.main.ScreenToWorldPoint(mousePoint);

    }

    void OnMouseDrag()

    {
        lastPos = actualPos;
        actualPos = transform.position;
        transform.position = GetMouseAsWorldPoint() + mOffset;
    }
     
    void Shoot(Vector3 force)
    {
        print("Not Normalized: "+ force);
        force.Normalize();
        print("Normalized: "+force);
        rb.AddForce(new Vector3(force.x, force.y, force.z) * forceMultiplier);
    }

}