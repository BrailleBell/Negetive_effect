using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using FMOD.Studio;
using FMODUnity;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnitySampleAssetsModified;
using Random = UnityEngine.Random;

public class TheThingNew : MonoBehaviour
{
    private GameObject Player;
    private Animator anim;
    private NavMeshAgent ghost;
    public float visibilityRange;
    public float DistanceToPlayer;
    private bool dying;
    private float killTimer;
    public bool dontFollow, dontRespawn;
    private StudioEventEmitter runningSound;
    public GameObject[] spawnPoints;
    private int spawnPointId;
    private float timetoDie;
    public float SpawnTimer;
    private float resettimer;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        ghost = GetComponent<NavMeshAgent>();
        ghost.speed = 0;
        
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.L))
        {
            dying = true;
        }
        
        if (!dontFollow)
        {
            DistanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
            if (DistanceToPlayer < visibilityRange)
            {
                resettimer = 0;
                anim.SetBool("Attack",true);
                ghost.SetDestination(Player.transform.position);
                ghost.speed = 15;
            }
            else
            {
                ghost.SetDestination(transform.position);
                
            }
            
        }
        
        // teleports until it finds player
        if (DistanceToPlayer > visibilityRange)
        {
            anim.SetBool("Attack",false);
            if (!dontRespawn)
            {
                resettimer += Time.deltaTime;
                if (resettimer > 30)
                {
                    dying = true;
                    resettimer = 0;
                }
                
            }
        }
        
        
        
        if (dying) // after taking picture of the ghost it dies after killtimer 
        {
            anim.SetBool("Death", true);
            GetComponent<BoxCollider>().enabled = false;
            killTimer += Time.deltaTime; // kill time must be over 0.2 secounds! 
            if (killTimer > 3f)
            {
                if (dontRespawn)
                {
                    gameObject.SetActive(false);
                }
                else if(!dontRespawn) // Teleports the monster to a new random locaiton
                {
                    timetoDie += Time.deltaTime;
                    spawnPointId = UnityEngine.Random.Range(1, spawnPoints.Length);
                    transform.position = spawnPoints[spawnPointId].transform.position;
                    if (DistanceToPlayer > 5)
                    {
                        spawnPointId = UnityEngine.Random.Range(1, spawnPoints.Length);
                        transform.position = spawnPoints[spawnPointId].transform.position;

                    }
                    
                    

                    //falsesafe that teleports the monster to new position if it has not been encountered for a while,
                    //also spawns the monster back after a certain amount of time
                    if (timetoDie > SpawnTimer)
                    {
                        dying = false;
                        Debug.Log("TimeToDie = " + timetoDie);
                        timetoDie = 0;
                        killTimer = 0;
                        anim.SetBool("Death",false);
                        anim.SetBool("Attack",false);
                        GetComponent<BoxCollider>().enabled = true;

                    }
                    
                }
            }
  
        }else
        {
            GetComponent<BoxCollider>().enabled = true;
        }
        

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,visibilityRange);
        if (ghost)
        {
            Gizmos.DrawRay(transform.position,ghost.destination);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CameraShoot")
        {
            UnityEngine.Debug.Log("monster hit");
            SpawnTimer = Random.Range(60, 120);
            dying = true;
            

        }

        if (other.CompareTag("Player"))
        {
           // GetComponent<BoxCollider>().enabled = false;
        }
    }
}
