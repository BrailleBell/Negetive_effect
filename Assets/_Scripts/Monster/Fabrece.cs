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
    void OnTriggerExit()
    {
        anim.SetBool("Run", true);
        run = true;
        film.transform.parent = null;
        Destroy(gameObject, killTime);
    }

    void Update()
    {
        if(run == true)
        {
            transform.position += transform.forward * Time.deltaTime * movementSpeed;
        }
    }
}
