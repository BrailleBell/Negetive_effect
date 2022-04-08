using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class TheMaiden : MonoBehaviour
{
    private GameObject Player;
    private NavMeshAgent ghost;
    public float DistanceToPlayer;
    public float HearingRange;
    private Animator anim;
    private bool ghostDying;
    public bool DontFollow;
    public bool SpawnAfterKilled = true;
    public bool RespawnsOn;
    public int RespawnsLeft;
    private float killTimer;
    public GameObject[] spawnPoints;
    private int spawnpointId;

    // Start is called before the first frame update
    void Start()
    {

        Player = GameObject.FindWithTag("Player");
        ghost = GetComponent<NavMeshAgent>();
        if (GetComponentInChildren<Animator>())
        {
            anim = GetComponent<Animator>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!DontFollow)
        {
            ghost.SetDestination(Player.transform.position);
            
        }
        else
        {
            ghost.SetDestination(ghost.transform.position);
        }
        
        
        DistanceToPlayer = Vector3.Distance(ghost.transform.position, Player.transform.position);

        if (DistanceToPlayer < HearingRange)
        {
            ghost.speed = DistanceToPlayer / 3;
        }
        else
        {
            ghost.speed = 5;
        }
        
        if (ghostDying) // after taking picture of the ghost it dies after killtimer 
        {
            //  anim.SetBool("Death",true);
            ghost.velocity = Vector3.zero;
            ghost.isStopped = true;
            killTimer += Time.deltaTime; // kill time must be over 0.2 secounds! 
            if (killTimer > 3f)
            {
                if (SpawnAfterKilled)
                {
                    spawnpointId = UnityEngine.Random.Range(1, spawnPoints.Length);
                    
                    if (RespawnsOn)
                    {
                        if (RespawnsLeft == 0)
                        {
                            gameObject.SetActive(false);
                        }
                        else
                        {
                            RespawnsLeft--;
                        }
                        
                        gameObject.transform.position = spawnPoints[spawnpointId].transform.position;
                        spawnpointId = spawnpointId++;
                        if (spawnpointId >= spawnPoints.Length)
                        {
                            spawnpointId = 1;
                        }
                        ghostDying = false;
                        killTimer = 0;
                        
                    }
                  

                }
                else
                {
                    gameObject.SetActive(false);
                    ghostDying = false;
                    killTimer = 0;
                }
                
            }
        }
        
        
        
        

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(ghost.transform.position,ghost.destination);
        Gizmos.DrawWireSphere(transform.position,HearingRange);

        if (ghost)
        {
              if (ghost.destination != null)
              {
                  Gizmos.DrawRay(transform.position, ghost.destination);
              }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CameraShoot"))
        {
            ghostDying = true;
        }
    }
}
