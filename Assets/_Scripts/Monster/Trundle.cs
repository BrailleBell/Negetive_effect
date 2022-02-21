
using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.Timeline;

public class Trundle : MonoBehaviour
{
    // floats && ints
    public float distanceToPlayer;
    private float lerp, lookAngle;
    public float LookRange;
    [Range(0,360)]
    public float FOV;
    public float aboveTimer, belowTimer, awareRadius;
    private float startTime, killTimer;
    private float groundlevel, baseoffset, attackDist;
    public int GoToSceneWhenKilled;
    private float vectorAngle;
    
    // bools && triggers
    public bool aboveGround = true, notTestKill, seesPlayer;
    private bool shouldLerp, lerpHasStarted, ghostDying, walking;

    // rest
    private GameObject Player;
    private NavMeshAgent ghost;
    private Rigidbody rb;
    private Animator anim;
    private Vector3 monsterOrgPos;
    private AnimationTrack AdjustSpeed;
    public Light chestLight;
    private Color redlight, greenlight;


    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        ghost = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        
        redlight = new Color(94, 14, 0, 255); 
        greenlight = new Color(16, 60, 13, 255);
    }

  
    void Update()
    {
        #region Visibility

        Vector2 angleToPlayer = (new Vector2(Player.transform.position.x, Player.transform.position.z) -
                                 new Vector2(transform.position.x, transform.position.z)).normalized;

        vectorAngle = Vector2.Angle(transform.forward, angleToPlayer);

        Vector2 debugger = new Vector2(transform.forward.x, transform.forward.z);
        debugger = Quaternion.Euler(0, 0, -FOV / 2) * debugger;
        Debug.DrawLine(transform.position, transform.position + (new Vector3(debugger.x,0,debugger.y)) * LookRange);
        debugger = Quaternion.Euler(0, 0, FOV) * debugger;
        Debug.DrawLine(transform.position, transform.position + (new Vector3(debugger.x,0,debugger.y)) * LookRange);
        
        if (vectorAngle < FOV / 2 && distanceToPlayer < LookRange || distanceToPlayer < awareRadius)
        {
            seesPlayer = true;
            // sees player 
        }
        else
        {
            seesPlayer = false;
        }
        

        #endregion
        
        
        anim.SetTrigger("Walk");
        #region movement
        distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);

        
        if (seesPlayer)
        {   
            ghost.SetDestination(Player.transform.position);
            // Change colour of inner circle
            chestLight.color = Color.Lerp(Color.green, Color.red,  Time.deltaTime * 0.5f);

            if (distanceToPlayer <= ghost.stoppingDistance)
            {
                GetComponent<BoxCollider>().enabled = true;
                // attack
                
                
                // face the player
                Vector3 direction = (Player.transform.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
                if (distanceToPlayer <= 1)
                {
                    SceneManager.LoadScene(GoToSceneWhenKilled); // kill or hurt player 
                }
                anim.ResetTrigger("Down");
                anim.SetTrigger("Up");
                anim.SetTrigger("Walk");

            }
        }
        

        if (ghost.destination == Player.transform.position)
        {
            walking = true;
        }

        if (walking)
        {
            float walkTimer = 0;
            walkTimer += Time.deltaTime;
            
            if (walkTimer > 1.4f)
            {
                ghost.speed = walkTimer * 80;
                walkTimer = 0;
            }
            
            anim.ResetTrigger("Up");
            anim.ResetTrigger("Down_Idle");
            anim.ResetTrigger("Down");
            anim.ResetTrigger("Idle");
            anim.SetTrigger("Walk");
        }


        if (ghostDying) // after taking picture of the ghost it dies after killtimer 
        {
            anim.SetTrigger("Death");
            ghost.velocity = Vector3.zero;
            ghost.isStopped = true;
            killTimer += Time.deltaTime; // kill time must be over 0.2 secounds! 
            if (killTimer > 2f)
            {
                if (notTestKill)
                {
                    gameObject.SetActive(false);

                }
                else
                {
                    gameObject.transform.position = monsterOrgPos; //KILL GHOST INSERT HERE 
                    
                    killTimer = 0;
                    ghostDying = false;

                }
            }
        }
        else // whenplayer is out of sight
        {
           
            chestLight.color = Color.Lerp(Color.red, Color.green, Time.deltaTime * 0.5f);
        }
        #endregion
    }

    private Vector3 LerpHelper(Vector3 Start, Vector3 End, float TimeStarted, float Interval = 1)
    {
        //calculates a new lerp location from 0-1 based on how much time has passed since the lerp started
        float TimePassed = Time.time - TimeStarted;
        float lerpLocation = TimePassed / Interval;

        //returns new lerped position
        return Vector3.Lerp(Start, End, lerpLocation);
    }

    private void AboveGroundMethod()
    {
        
        
    }

    private void BelowGroundMethod()
    {
        
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
            Debug.Log("monster hit");
            ghostDying = true;


        }
    }
    
    
    
}



