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
    
    //dont mess up the generatedMonstercount kek
    private int generatedMonstersCount = 0;
    public bool dying;
    private bool haveSpawned;
    private NavMeshAgent ghost;
    private Animator anim;
    private float killTimer;
    public GameObject[] spawnPoints;
    private int SpawnPointId;
    public float reSpawnTimer;
    private float timerReset;
    
    


    // Start is called before the first frame update
    void Start()
    {
      Player = GameObject.FindWithTag("Player");
      ghost = GetComponent<NavMeshAgent>();
    }

    public void SpawnMonsters()
    {
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
            }
            
        }
        
        
        //Distance calculation
        DistanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
        if (DistanceToPlayer < awareRadius)
        {
            if (!haveSpawned)
            {
                //spawns the monstr lel cant u read
                SpawnMonsters();
                haveSpawned = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            dying = true;
        }
        
        
        if (dying) // after taking picture of the ghost it dies after killtimer 
        {
         //   anim.SetBool("Death",true);
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
                    SpawnPointId = UnityEngine.Random.Range(1, spawnPoints.Length);
                    timerReset += Time.deltaTime;
                    if (timerReset > reSpawnTimer)
                    {
                        transform.position = spawnPoints[SpawnPointId].transform.position;
                        dying = false;
                    }
                    
                }
                
                
               
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


