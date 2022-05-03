using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidFreezeLocal : MonoBehaviour
{
    Rigidbody rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(rigid.velocity);
        localVelocity.x = 0;
        localVelocity.z = 0;
         
        rigid.velocity = transform.TransformDirection(localVelocity);
    }
}
