using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnitySampleAssetsModified;

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
        if (!dontFollow)
        {
            DistanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
            if (DistanceToPlayer < visibilityRange)
            {
                anim.SetBool("Attack",true);
                ghost.SetDestination(Player.transform.position);
                ghost.speed = 15;
            }
            else
            {
                ghost.SetDestination(transform.position);
                
            }
            
        }

        
        if (dying) // after taking picture of the ghost it dies after killtimer 
        {
            anim.SetBool("Death", true);
            GetComponent<BoxCollider>().enabled = false;
            timetoDie += Time.deltaTime;
            killTimer += Time.deltaTime; // kill time must be over 0.2 secounds! 
            if (killTimer > 3f)
            {
                if (dontRespawn)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    anim.SetBool("Death",false);
                    anim.SetBool("Attack", false);
                    killTimer = 0;
                    if (timetoDie > SpawnTimer)
                    {
                        spawnPointId = UnityEngine.Random.Range(1, spawnPoints.Length);
                        transform.position = spawnPoints[spawnPointId].transform.position;
                        
                    
                        
                    }
                    
                }
            }
  
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
            dying = true;

        }
    }
}
