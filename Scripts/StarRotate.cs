using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarRotate : MonoBehaviour
{
    // if true - the boost, if false - the ammo
    public bool objType;
    public float rotationSpeed;
    public Vector3 rotationDirection;

    // Update is called once per frame
    void Update()
    {
        if (objType)
        {
            transform.Rotate(rotationDirection, rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }
}