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
    private Animator anim;
    private GameObject deathPP;


    private void Awake()
    { 
        GameObject.Find("XR Origin").transform.position = lastPostPos;
        deathPP = GameObject.Find("post_death");
    }

    void Start()
    {
        Player = GameObject.Find("XR Origin");
        anim = deathPP.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("DeathTimerBool is " + DeathTimer);
        if (DeathTimer) // A bool, checks if the player has died
        {
            respawnTimerCounter += Time.deltaTime;
            if (respawnTimerCounter > respawnTimer)
            {
                respawnTimerCounter = 0;
                //Write here, everything that should happen before death
                
                
                
                
                Dying();
            }
        }
        else
        {
            anim.SetBool("Dead", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            anim.SetBool("Dead",true);
            DeathTimer = true;
            Debug.Log("DeathCounter " + respawnTimerCounter);
        }
        
        if (other.CompareTag("DeathBarrier"))
        {
            DeathTimer = true;
        }
    }

    //  private void OnTriggerExit(Collider other)
    //  {
    //      if (other.CompareTag("Monster"))
    //      {
    //          Dying();
    //          //DeathTimer = true;
    //          //Debug.Log("DeathCounter " + respawnTimerCounter);
    //          
    //      }
    //      
    //      if (other.CompareTag("DeathBarrier"))
    //      {
    //          DeathTimer = true;
    //          Debug.Log("DeathCounter " + respawnTimerCounter);
    //          
    //      }
    //      
    //      
    //  }


    public void Dying()
    {
        // Write everything that happens during death
        transform.position = lastPostPos;
        // SceneManager.LoadScene(SceneToGoTo);
        
        
        
        
        //This is last entry
        DeathTimer = false;

    }
}
