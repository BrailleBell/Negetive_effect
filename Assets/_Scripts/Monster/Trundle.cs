
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
    private float lerpStuff, lookAngle;
    public float LookRange;
    [Range(0,360)]
    public float FOV;
    public float aboveTimer, belowTimer, awareRadius;
    private float startTime, killTimer;
    private float groundlevel, baseoffset, attackDist;
    public int GoToSceneWhenKilled;
    private float vectorAngle;
    
    
    // States and enums
    public enum State
    {
        Idle,
        Walking,
        Attacking,
        Underground,
        AboweGround
    }

    public State state;
    
    
    // bools && triggers
    public bool aboveGround, notTestKill, seesPlayer;
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
        state = State.Idle;
    }

  
    void Update()
    {
         distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
         ghost.SetDestination(Player.transform.position);
         if (distanceToPlayer > awareRadius)
         {
             anim.SetTrigger("Down");
         }
         
         
         switch (state)
        {
            case State.Idle:
                LookingForPlayer();
                break;
            case State.Attacking:
                break;
            case State.Walking:
                
                LookingForPlayer();
                anim.SetTrigger("Walk");
                break;
            case State.AboweGround:
                AboveGround();
                break;
            case State.Underground:
                BelowGround();
                break;
        }
        
        #region Visibility
        
      


        #endregion
        
        
        
        #region movement

        if (!aboveGround)
        {
            BelowGround();
        }
        
        if (seesPlayer)
        {
            ghost.updatePosition = true;
            ghost.SetDestination(Player.transform.position);
            
            // Change colour of inner circle
            if (lerpStuff < 1)
            {
                lerpStuff += Time.deltaTime / 0.5f;

            }
            
            // face the player
            Vector3 direction = (Player.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
            
            if ((distanceToPlayer <= LookRange) && (distanceToPlayer >= awareRadius))
            {


            }
            else if ((distanceToPlayer <= LookRange) && (distanceToPlayer <= awareRadius))
            {
                AboveGround();
                if (distanceToPlayer <= ghost.stoppingDistance)
                {
                    GetComponent<BoxCollider>().enabled = true;
                    // attack


                   
                    if (distanceToPlayer <= 1)
                    {
                        SceneManager.LoadScene(GoToSceneWhenKilled); // kill or hurt player 
                    }
                }
                
            }
           
            
        }
        
        else // whenplayer is out of sight
        {
            seesPlayer = false;
            if (lerpStuff > 0)
            {
                lerpStuff -= Time.deltaTime / 0.5f;
                aboveGround = true;
            }
            
        }
        
        chestLight.color = Color.Lerp(Color.green, Color.red, lerpStuff);
        

        if (walking)
        {
            float walkTimer = 0;
            walkTimer += Time.deltaTime;
            
            if (walkTimer > 1.4f)
            {
                ghost.speed = walkTimer * 80;
                walkTimer = 0;
            }
            
        }
        
        


        if (ghostDying) // after taking picture of the ghost it dies after killtimer 
        {
            // anim.SetTrigger("Death");
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


    public void AboveGround()
    {
       // anim.ResetTrigger("Down");
        anim.SetTrigger("Up");
    }

    public void BelowGround()
    {
       // anim.SetTrigger("Down");
       // anim.ResetTrigger("Walk");
      //  anim.ResetTrigger("Up");
        
    }

    public void LookingForPlayer()
    {
        Vector2 angleToPlayer = (new Vector2(Player.transform.position.x, Player.transform.position.z) -
                                 new Vector2(transform.position.x, transform.position.z)).normalized;
        vectorAngle = Vector2.Angle(transform.forward, angleToPlayer);
        if (distanceToPlayer <= LookRange && vectorAngle <= FOV / 2)
        {
            seesPlayer = true;
            if (distanceToPlayer > awareRadius)
            {
                state = State.Underground;
                ghost.SetDestination(Player.transform.position);
            }
            else if(distanceToPlayer < awareRadius)
            {
                state = State.AboweGround;
                ghost.SetDestination(Player.transform.position);
            }
        }
        else
        {
            seesPlayer = false;
        }
    }
    
    
    
    
    #region debug
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,awareRadius);
        Vector2 debugger = new Vector2(transform.forward.x, transform.forward.z);
        debugger = Quaternion.Euler(0, 0, -FOV / 2) * debugger;
        Debug.DrawLine(transform.position, transform.position + (new Vector3(debugger.x,0,debugger.y)) * LookRange);
        debugger = Quaternion.Euler(0, 0, FOV) * debugger;
        Debug.DrawLine(transform.position, transform.position + (new Vector3(debugger.x,0,debugger.y)) * LookRange);
    }
    #endregion
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CameraShoot")
        {
            Debug.Log("monster hit");
            ghostDying = true;


        }
    }
    
    
    
}



