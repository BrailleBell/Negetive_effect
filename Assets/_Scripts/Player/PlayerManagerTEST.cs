using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManagerTEST : MonoBehaviour
{
    public GameObject Player;
    public static Vector3 lastPostPos;
    public int SceneToGoTo;
    private float respawnTimer = 1 ;
    private float respawnTimerCounter;
    private bool DeathTimer;
    private GameObject deathPP;
    private Animator anim;
    public GameObject cS;
    [HideInInspector]
    public bool PlayerDied;


    private void Awake()
    { 
        GameObject.Find("FPSPlayer!").transform.position = lastPostPos;
        deathPP = GameObject.Find("post_death");
        cS = GameObject.Find("Canvases");
    }

    void Start()
    {
        Player = GameObject.Find("FPSPlayer!");
        anim = deathPP.GetComponent<Animator>();

    }

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
            PlayerDied = false;
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
        FMODUnity.RuntimeManager.PlayOneShot("event:/Effects/NewHourEffect",GetComponent<Transform>().transform.position);
        // SceneManager.LoadScene(SceneToGoTo);
//        cS.GetComponent<TimeSwap>().Show_again(Player.transform);



        PlayerDied = true;
        //This is last entry
        DeathTimer = false;

    }
}
