using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseEncounter : MonoBehaviour
{
    public float health = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health >= 100)
        {
            health = 100;
        }

        if (health <= 1)
        {
            Debug.Log("dead");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            drainingHealth();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        health += Time.deltaTime;
        
    }

    public void drainingHealth()
    {
        health -= Time.deltaTime;
    }
    
}
