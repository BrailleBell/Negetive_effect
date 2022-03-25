using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HeadCollider : MonoBehaviour
{
    public Camera camera; //this is supposed to be the camera for eyes
    CapsuleCollider collider;

    private void Start()
    {
        collider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        /*So this will take the position from the center of the camera (aka the eyes) and
         * subtract the position on the playercontroller
         * which means that when we move around it gets the position
         * of the camera relative to the playercontroller*/
        Vector3 temp = camera.transform.position - this.transform.position;
        temp.y = collider.transform.position.y;
        collider.center = temp;
    }
}
