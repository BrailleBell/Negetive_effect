using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnCollision : MonoBehaviour
{
    public GameObject objectToAppear;
    void OnTriggerEnter(Collider target)
    {
        if(target.tag == "Player")
        {
            objectToAppear.SetActive(true);
        }
    }
}
