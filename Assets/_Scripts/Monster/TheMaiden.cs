using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class TheMaiden : MonoBehaviour
{
    private RaycastHit hit;
    public float _float;
    private float deathTimer;
    private float timetodie;
    private GameObject Player;
    private GameObject ghost;
    public float DistanceToPlayer;
    public float HearingRange;
    private Animator anim;
    public bool ghostDying;
    public bool DontFollow;
    public bool RespawnsOn;
    public int RespawnsLeft;
    private float killTimer;
    public float moveSpeed;
    public GameObject toTurnOff;
    public GameObject[] spawnPoints;
    private int spawnpointId;
    private FMOD.Studio.EventInstance MaidenDeathScream;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        ghost = gameObject;
        if (GetComponentInChildren<Animator>())
        {
            anim = GetComponentInChildren<Animator>();
        }
        toTurnOff = GameObject.Find("D_TheMaiden_Mnstr");

    }

    // Update is called once per frame
    void Update()
    {
        
        // Looks at the player
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Player.transform.position - transform.position), 3 * Time.deltaTime);
        
        
        if (!DontFollow)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        
        else
        {
            transform.position = transform.position;
        }
        
        //Keeping her above ground, _float is her floating height
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down,out hit, Mathf.Infinity, 1<<7))
        {
            if (hit.distance > _float)
            {
                transform.position = new Vector3(transform.position.x, hit.point.y + _float, transform.position.z);
            }
            else if (hit.distance < _float)
            {
                transform.position = new Vector3(transform.position.x, hit.point.y + _float, transform.position.z);
            }

        }


        if (Input.GetKeyUp(KeyCode.K))
        {
            ghostDying = true;
            FMODUnity.RuntimeManager.PlayOneShot("event:/Effects/Monsters/MaidenDeath",GetComponent<Transform>().position);
        }
     
        

        
        
        DistanceToPlayer = Vector3.Distance(ghost.transform.position, Player.transform.position);

        if (DistanceToPlayer < HearingRange)
        {
         
        }
        else
        {
            
        }
        
        if (ghostDying) // after taking picture of the ghost it dies after killtimer 
        {
            
            timetodie  += Time.deltaTime;
            Debug.Log(timetodie);
            anim.SetBool("Death",true);
            anim.SetBool("Flying",false);
            MaidenDeathScream.start();
            GetComponent<BoxCollider>().enabled = false;
            killTimer += Time.deltaTime; // kill time must be over 0.2 secounds! 
            if (killTimer > 3f)
            {

                
                Debug.Log("deathTimer = " + deathTimer + "timeToDie = "+ timetodie);
                toTurnOff.SetActive(false);
                if (timetodie > deathTimer)
                {

                   //Debug.Log("Maidens SpawnPointId = " + spawnpointId);
                    spawnpointId = UnityEngine.Random.Range(1, spawnPoints.Length);
                    
                      if (RespawnsOn)
                      {
                           if (RespawnsLeft == 0)
                           {
                               toTurnOff.SetActive(false);
                           }
                           else
                          {
                              RespawnsLeft--;
                          }

                          anim.SetBool("Death",false);
                          transform.position = spawnPoints[spawnpointId].transform.position;
                          GetComponent<BoxCollider>().enabled = true;
                          toTurnOff.SetActive(true);
                           anim.SetBool("Flying",true);
                            timetodie = 0;
                         if (spawnpointId >= spawnPoints.Length)
                          {
                              spawnpointId = 1;
                          }
                          ghostDying = false;
                          killTimer = 0;

                           if (transform.position == spawnPoints[spawnpointId].transform.position)
                           {
                               transform.position += transform.forward * moveSpeed * Time.deltaTime;
                              spawnpointId = spawnpointId++;
                           }

                      }
                      else
                      {
                           transform.position = spawnPoints[spawnpointId].transform.position;
                           GetComponent<BoxCollider>().enabled = true;
                           anim.SetBool("Flying",true);
                           anim.SetBool("Death",false);
                            timetodie = 0;
                            toTurnOff.SetActive(true);
                            if (transform.position == spawnPoints[spawnpointId].transform.position)
                            {
                            transform.position += transform.forward * moveSpeed * Time.deltaTime;
                            spawnpointId = spawnpointId++;
                           }
                        
                    }

                    //toTurnOff.SetActive(false);
                    //ghostDying = false;
                    //killTimer = 0; 

                }
                
            }
        
        
        }
        

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(ghost.transform.position,ghost.destination);
        Gizmos.DrawWireSphere(transform.position,HearingRange);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CameraShoot"))
        {
            //Debug.Log("Maiden is captured!!! OMG it works maybe");
            ghostDying = true;
            FMODUnity.RuntimeManager.PlayOneShot("event:/Effects/Monsters/MaidenDeath",GetComponent<Transform>().transform.position);
            deathTimer = Random.Range(60, 120);
        }
    }
}
