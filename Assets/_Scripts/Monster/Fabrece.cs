using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fabrece : MonoBehaviour
{
    bool run;
    float movementSpeed = 5;
    public float killTime = 5;
    public Animator anim;
    public GameObject film;
    public GameObject gObject;
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Film"))
        {
        anim.SetBool("Run", true);
        run = true;
        film.transform.parent = null;
        Destroy(gObject, killTime);
        }
    }

    void Update()
    {
        if(run == true)
        {
            gObject.transform.position += transform.right * Time.deltaTime * movementSpeed;
        }
    }
}
