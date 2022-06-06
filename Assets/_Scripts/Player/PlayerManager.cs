using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public GameObject Player;
    public static Vector3 lastPostPos = new Vector3(1817.61f,42.515f,253.88f);
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
    private float currentTime = GameManager.getTimer;

    private static Watch watch;

    private void Awake()
    { 
        GameObject.Find("XR Origin").transform.position = lastPostPos;
        deathPP = GameObject.Find("post_death");

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
        if(Input.GetKeyDown(KeyCode.R))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Effects/TrueDeath",GetComponent<Transform>().transform.position);
        }
       // Debug.Log("DeathTimerBool is " + DeathTimer);
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
          //  FMODUnity.RuntimeManager.PlayOneShot("event:/Effects/TrueDeath",GetComponent<Transform>().transform.position);
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
            FMODUnity.RuntimeManager.PlayOneShot("event:/Effects/TrueDeath",GetComponent<Transform>().transform.position);
            anim.SetBool("Dead",true);
            DeathTimer = true;
            for (int i = 0; i < spawnFilm.Length; i++)
            {
                Instantiate(film, spawnFilm[i].transform.position, Quaternion.identity);
            }
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
       // FMODUnity.RuntimeManager.PlayOneShot("event:/Effects/Death",GetComponent<Transform>().transform.position);
        // SceneManager.LoadScene(SceneToGoTo);

        // The death pp transform
        cS.GetComponent<TimeSwap>().Show_again(Player.transform);

        currentTime = 0;
      //  watch.OnSceneLoaded(SceneManager.GetSceneByBuildIndex(2), LoadSceneMode.Single);

        PlayerDied = true;
            //This is last entry
        DeathTimer = false;

    }
}
