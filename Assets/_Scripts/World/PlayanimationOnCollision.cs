using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayanimationOnCollision : MonoBehaviour
{
    public Animator anim;
    public GameObject wheel1, wheel2;
    bool taskDone;
    
    void OnTriggerEnter(Collider target)
    {
        Debug.Log("uhm...");
        if(target.tag == "DoorCollider")
        {
            if(taskDone == false)
            {
                anim.SetBool("Open", true);
                taskDone = false;
                wheel2.SetActive(true);
                wheel1.SetActive(false);
                FMODUnity.RuntimeManager.PlayOneShot("event:/Effects/DoorOpening",GetComponent<Transform>().position);
            }
        }
    }
}