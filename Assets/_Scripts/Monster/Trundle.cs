
using System;
using System.Collections;
using TMPro.EditorUtilities;
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
    public int wayPointInd;
    public float radiusToWaypoint;
    public float raduiusAroundTarget,spinningRadius; // Spinningradius always lover than radiusAroundTarget
    public float circlingSpeed;
    private float pos;
    public float _speed;

    // States and enums
    public enum State
    {
        Idle,
        Patroling,
        Attacking,
        Chase
    }

    public State state;
    
    
    // bools && triggers
    public bool aboveGround, notTestKill, seesPlayer, testOn, circlingThePlayer;
    private bool shouldLerp, lerpHasStarted, ghostDying, walking, belowGround;

    // rest
    public GameObject[] MonsterWaypoints;
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
        state = State.Patroling;
        anim = GetComponentInChildren<Animator>();
        ghost = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        redlight = new Color(94, 14, 0, 255); 
        greenlight = new Color(16, 60, 13, 255);
        wayPointInd = Random.Range(1, MonsterWaypoints.Length);



    }

  
    void Update()
    {
         distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
         




         switch (state)
        {
            case State.Idle:
                LookingForPlayer();
                break;
            case State.Attacking:
                break;
            case State.Chase:
                Chase();
                break;
            case State.Patroling:
                Patroling();
                LookingForPlayer();
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
            
            if ((distanceToPlayer <= LookRange) && (distanceToPlayer <= awareRadius))
            {
             
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
        
    }

    public void BelowGround()
    {
       // anim.SetTrigger("Down");
       // anim.ResetTrigger("Walk");
      //  anim.ResetTrigger("Up");
        
    }

    public void Patroling()
    {
        anim.SetBool("Up",true);
        anim.SetBool("Walk",true);
        gameObject.GetComponent<BoxCollider>().enabled = true;
        if (Vector3.Distance(gameObject.transform.position, MonsterWaypoints[wayPointInd].transform.position) >= radiusToWaypoint)
        {
            ghost.SetDestination(MonsterWaypoints[wayPointInd].transform.position);
            transform.LookAt(MonsterWaypoints[wayPointInd].transform.position);
            
        }
        else if(Vector3.Distance(gameObject.transform.position, MonsterWaypoints[wayPointInd].transform.position) <= radiusToWaypoint)
        {
            wayPointInd +=1 ;
            if (wayPointInd >= MonsterWaypoints.Length - 1)
            {
                wayPointInd = 0;
            }
        }

        if (seesPlayer)
        {
            state = State.Chase;
        }
        


    }

    public void LookingForPlayer()
    {
        Vector2 angleToPlayer = (new Vector2(Player.transform.position.x, Player.transform.position.z) -
                                 new Vector2(transform.position.x, transform.position.z)).normalized;
        vectorAngle = Vector2.Angle(transform.forward, angleToPlayer);
        if (distanceToPlayer <= LookRange && vectorAngle <= FOV / 2)
        {
            seesPlayer = true;
        }
        else
        {
            seesPlayer = false;
        }
        
    }

    public void Chase()
    {
        belowGround = true;
        if (belowGround)
        {
            anim.SetBool("Down",true);
            belowGround = false;

        }
        else
        {
            anim.SetBool("Down",false);
        }
        anim.SetBool("Walk",false);
        anim.SetBool("Up",false);
        
        belowTimer += Time.deltaTime;
        if (belowTimer >= Random.Range(10, 50))
        {
           // state = State.Attacking;
           // belowTimer = 0;
        }

        ghost.speed = distanceToPlayer / 3;
        if (distanceToPlayer < raduiusAroundTarget)
        {
            float startAngle;
            if (!circlingThePlayer)
            {
                
                startAngle = Vector3.Angle(transform.position, Player.transform.position);
                ghost.speed = circlingSpeed;
                float x = Mathf.Cos(startAngle) * spinningRadius;
                float y = Mathf.Sin(startAngle) * spinningRadius;
                Vector3 targetPos = new Vector3(x, 0, y);
                targetPos = Player.transform.position + targetPos;
                ghost.SetDestination(targetPos);
                circlingThePlayer = true;
                transform.LookAt(targetPos);
            }
            else
            {
                ghost.acceleration = 100;
                ghost.angularSpeed = 100;
                pos += Time.deltaTime * circlingSpeed;
                ghost.speed = _speed;
                float angle = pos * 3.14159f;
                float x = Mathf.Cos(angle) * spinningRadius;
                float y = Mathf.Sin(angle) * spinningRadius;
                Vector3 targetPos = new Vector3(x, 0, y);
                targetPos = Player.transform.position + targetPos;
                ghost.SetDestination(targetPos);
                transform.LookAt(targetPos);
                _speed = circlingSpeed * 50;



            }
            
            

        }
        else
        {
            circlingThePlayer = false;
            ghost.SetDestination(Player.transform.position);
        }
       
        
        
        if (!seesPlayer && !testOn)
        {
            state = State.Patroling;
        }
        
    }

    public void Attacking()
    {
        if (!aboveGround)
        {
            anim.SetBool("Up",true);
            aboveGround = true;
        }
        

        ghost.SetDestination(Player.transform.position);
        if (distanceToPlayer < 1)
        {
            
            
            
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
        if (ghost != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(ghost.destination,raduiusAroundTarget);
            Gizmos.DrawSphere(ghost.destination, 1);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(Player.transform.position,spinningRadius);
        }
        
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



