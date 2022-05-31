using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public GameObject Player;
    public static Vector3 lastPostPos;
    public int SceneToGoTo;
    public float respawnTimer;
    private float respawnTimerCounter;
    private bool DeathTimer;
    private Animator anim;
    private GameObject deathPP;
    private GameObject cS; 
    [HideInInspector]
    public bool PlayerDied;
    public GameObject[] spawnFilm;
    public GameObject film;


    private void Awake()
    { 
        GameObject.Find("XR Origin").transform.position = lastPostPos;
        deathPP = GameObject.Find("post_death");

        //attach a callback for every new scene that is loaded
        //it is fine to remove a callback that wasn't added so far
        //this makes sure that this callback is definitely only added once
        //SceneManager.sceneLoaded -= OnSceneLoaded;
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        Player = GameObject.Find("XR Origin");
        anim = deathPP.GetComponent<Animator>();
        cS = GameObject.Find("Canvases");
        
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
            
            for (int i = 0; i < spawnFilm.Length; i++)
            {
                Instantiate(film, spawnFilm[i].transform.position, Quaternion.identity);
            }
        }
        
        if (other.CompareTag("DeathBarrier"))
        {
            DeathTimer = true;
           // for (int i = 0; i < spawnFilm.Length; i++)
           // {
           //     Instantiate(film, spawnFilm[i].transform.position, Quaternion.identity);
           // }
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
        FMODUnity.RuntimeManager.PlayOneShot("event:/Effects/Death",GetComponent<Transform>().transform.position);
        // SceneManager.LoadScene(SceneToGoTo);
        cS.GetComponent<TimeSwap>().Show_again(Player.transform);


        PlayerDied = true;
            //This is last entry
        DeathTimer = false;

    }
}
