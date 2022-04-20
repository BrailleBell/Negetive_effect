using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class TheMaiden : MonoBehaviour
{
    private RaycastHit hit;
    public float _float;
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
    public float moveSpeed;
    public GameObject[] spawnPoints;
    private int spawnpointId;

    // Start is called before the first frame update
    void Start()
    {

        Player = GameObject.FindWithTag("Player");
        ghost = GetComponent<NavMeshAgent>();
        if (GetComponentInChildren<Animator>())
        {
            anim = GetComponentInChildren<Animator>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position,Vector3.down,Color.green);
        if(Physics.Raycast(gameObject.transform.position,Vector3.down,_float,7))
        {
            transform.position = new Vector3(transform.position.x, _float, transform.position.z);
        }
        else{
            
        }
     
        
        // Looks at the player
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Player.transform.position - transform.position), 3 * Time.deltaTime);
        
        if (Input.GetKeyUp(KeyCode.F))
        {
           // FMODUnity.RuntimeManager.PlayOneShot("event:/Effects/MaidenDeath",GetComponent<Transform>().position);
        }
        
        if (!DontFollow)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        
        else
        {
            ghost.SetDestination(ghost.transform.position);
        }
        
        
        DistanceToPlayer = Vector3.Distance(ghost.transform.position, Player.transform.position);

        if (DistanceToPlayer < HearingRange)
        {
            moveSpeed = DistanceToPlayer / 4;
        }
        else
        {
            moveSpeed = 10;
        }
        
        if (ghostDying) // after taking picture of the ghost it dies after killtimer 
        {
            anim.SetBool("Death",true);
            anim.SetBool("Flying",false);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Effects/MaidenDeath",GetComponent<Transform>().position);
            ghost.velocity = Vector3.zero;
            ghost.isStopped = true;
            GetComponent<BoxCollider>().enabled = false;
            killTimer += Time.deltaTime; // kill time must be over 0.2 secounds! 
            if (killTimer > 3f)
            {
                gameObject.SetActive(false);
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

                        anim.SetBool("Death",false);
                        transform.position = spawnPoints[spawnpointId].transform.position;
                        GetComponent<BoxCollider>().enabled = true;
                        anim.SetBool("Flying",true);
                        spawnpointId = spawnpointId++;
                        if (spawnpointId >= spawnPoints.Length)
                        {
                            spawnpointId = 1;
                        }
                        ghostDying = false;
                        killTimer = 0;

                    }
                    else
                    {
                        transform.position = spawnPoints[spawnpointId].transform.position;
                        GetComponent<BoxCollider>().enabled = true;
                        anim.SetBool("Flying",true);
                        anim.SetBool("Death",false);
                        
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
            Debug.Log("Maiden is captured!!! OMG it works maybe");
            ghostDying = true;
        }
    }
}
