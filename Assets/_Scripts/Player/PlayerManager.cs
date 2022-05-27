using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public GameObject Player;
    public static Vector3 lastPostPos = new Vector3(-3,0,-3);
    public int SceneToGoTo;
    public float respawnTimer;
    private float respawnTimerCounter;
    private bool DeathTimer;


    private void Awake()
    { 
        GameObject.Find("XR Origin").transform.position = lastPostPos;
    }

    void Start()
    {
        Player = GameObject.Find("XR Origin");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DeathTimer)
        {
            respawnTimerCounter += Time.deltaTime;
            if (respawnTimerCounter > respawnTimer)
            {
                Dying();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            DeathTimer = true;
            Debug.Log("DeathCounter " + respawnTimerCounter);
        }
        
        if (other.CompareTag("DeathBarrier"))
        {
            DeathTimer = true;
        }
    }


    public void Dying()
    {
        transform.position = lastPostPos;
        // SceneManager.LoadScene(SceneToGoTo);
        
        
        
        DeathTimer = false;
        respawnTimerCounter = 0;

    }
}
