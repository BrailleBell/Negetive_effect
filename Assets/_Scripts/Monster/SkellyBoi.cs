using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class SkellyBoi : MonoBehaviour
{
    //read and u will understand
    public int AmountOfMonsters;
    public GameObject Player;
    public Transform monster;
    public float distFromPlayerToSpawn;
    public float DistanceToPlayer;
    public float awareRadius;
    public bool DontRespawn;
    public bool summoning;
    
    //dont mess up the generatedMonstercount kek
    private int generatedMonstersCount = 0;
    public bool dying;
    [HideInInspector]
    public bool haveSpawned;
    private NavMeshAgent ghost;
    private Animator anim;
    private float killTimer;
    public GameObject[] spawnPoints;
    private int SpawnPointId;
    public float reSpawnTimer;
    private float timerReset;
    public float resettimer;
    private float ifNotseenInAmount;
    
    


    // Start is called before the first frame update
    void Start()
    {
      Player = GameObject.FindWithTag("Player");
      ghost = GetComponent<NavMeshAgent>();
      anim = gameObject.GetComponentInChildren<Animator>();

    }

    public void SpawnMonsters()
    {
        summoning = true;
        anim.SetBool("Summon",true);
        for (int i = 0; i < AmountOfMonsters; i++)
        {
            generatedMonstersCount++;
            string objName = "SkellyCopy" + generatedMonstersCount;
            float angleIteration = 360 / AmountOfMonsters;
            float currentRotation = angleIteration * i;

            Transform monst;
            monst = Instantiate(monster, Player.transform.position, Player.transform.rotation) as Transform;
            monst.name = objName;
            
            monst.transform.Rotate(new Vector3(0,currentRotation,0));
            monst.transform.Translate(new Vector3(distFromPlayerToSpawn,5,0));

        }
    }

   

// Update is called once per frame
    void Update()
    {
       // Debug.Log("Havespawned bool = " + haveSpawned);
        // teleports the enemy to another position when the player dies
        if (Player.GetComponent<PlayerManagerTEST>())
        {
            if (Player.GetComponent<PlayerManagerTEST>().PlayerDied)
            {
                SpawnPointId = UnityEngine.Random.Range(1, spawnPoints.Length);
                transform.position = spawnPoints[SpawnPointId].transform.position;
            }
            
        }

        

        if (Player.GetComponent<PlayerManager>())
        {
            if (Player.GetComponent<PlayerManager>().PlayerDied) 
            {
                SpawnPointId = UnityEngine.Random.Range(1, spawnPoints.Length);
                transform.position = spawnPoints[SpawnPointId].transform.position;
                anim.SetBool("Summon",false);
                haveSpawned = false;
            }
            
        }
        
        
        //Distance calculation
        DistanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
        if (DistanceToPlayer < awareRadius)
        {
            if (!haveSpawned)
            {
                if(!dying)
                {
                    //ghost.SetDestination(gameObject.transform.position);
                    //spawns the monstr 
                    SpawnMonsters();
                    haveSpawned = true;
                }             
            }
        }
        else
        {
            //ghost.SetDestination(Player.transform.position);
            ifNotseenInAmount += Time.deltaTime;
            if(ifNotseenInAmount > resettimer)
            {
                dying = true;
                ifNotseenInAmount = 0;
            }
        }

        if(DistanceToPlayer > awareRadius)
        {
            //ghost.SetDestination(Player.transform.position);
        }

    
        if (Input.GetKeyUp(KeyCode.B))
        {
            dying = true;
        }
        
        
        if (dying) // after taking picture of the ghost it dies after killtimer
        {
            summoning = false;
            haveSpawned = false;
            anim.SetBool("Death",true);
            anim.SetBool("Summon",false);
            ghost.velocity = Vector3.zero;
            ghost.isStopped = true;
            killTimer += Time.deltaTime; // kill time must be over 0.2 secounds! 
            if (killTimer > 3f)
            {
                if (DontRespawn)
                {
                    gameObject.SetActive(false);
                    
                }
                else
                {
                    
                    
                    //ghost.SetDestination(Player.transform.position);
                    transform.position = spawnPoints[SpawnPointId].transform.position;
                    
                    Debug.Log("SpawnPointID er " + SpawnPointId);
                    awareRadius = 0;
                    timerReset += Time.deltaTime;
                    if (timerReset > reSpawnTimer)
                    {
                        dying = false;
                        anim.SetBool("Death",false);
                        timerReset = 0;
                        haveSpawned = false;
                        Debug.Log("SpawnPointId for Shellback is " + SpawnPointId);
                        awareRadius = 100;
                        //ghost.SetDestination(Player.transform.position);
                        //SpawnPointId = UnityEngine.Random.Range(1, spawnPoints.Length -1);
                        SpawnPointId++;
                        if(SpawnPointId == spawnPoints.Length)
                        {
                            SpawnPointId = 0;
                        }

                        transform.position = spawnPoints[SpawnPointId].transform.position;
                        killTimer = 0;
                    }
                    
                }
                
                
               
            }
        }
        if(summoning == false)
        {
            if(dying == false)
            {
                //ghost.SetDestination(Player.transform.position);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,awareRadius);
        
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


