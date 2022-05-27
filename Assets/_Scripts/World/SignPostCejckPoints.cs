using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignPostCejckPoints : MonoBehaviour
{
    private GameObject Player;

    private int currentSignPost;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager.lastPostPos = transform.position;
            PlayerManagerTEST.lastPostPos = transform.position;
            Debug.Log("CheckPointReached " + PlayerManager.lastPostPos);
        }
    }
}
