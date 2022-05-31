using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignPostCejckPoints : MonoBehaviour
{
    private GameObject Player;
    public GameObject light;
    private float timer;
    private bool lightsOn;

    private int currentSignPost;
    // Start is called before the first frame update
    void Start()
    {
        light.SetActive(false);
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (lightsOn)
        {
            timer += Time.deltaTime;
            if (timer > 30)
            {
                light.SetActive(false);
                timer = 0;
                lightsOn = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lightsOn = true;
            PlayerManager.lastPostPos = transform.position;
            PlayerManagerTEST.lastPostPos = transform.position;
            FMODUnity.RuntimeManager.PlayOneShot("event:/Effects/Death",GetComponent<Transform>().transform.position);
            Debug.Log("CheckPointReached " + PlayerManager.lastPostPos);
            light.SetActive(true);
        }
    }
    
}
