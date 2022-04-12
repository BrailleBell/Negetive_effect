
using System;
using System.Collections;
//using TMPro.EditorUtilities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.Timeline;
using FMOD;

public class Trundle : MonoBehaviour
{
    
    // floats && ints
    public float distanceToPlayer;
    private float lerpStuff, lookAngle;
    public float LookRange;
    [Range(0,360)]
    public float FOV;
    public float aboveTimer, belowTimer, awareRadius,attackTimer;
    private float startTime, killTimer, uptime;
    private float groundlevel, baseoffset, attackDist;
    public int GoToSceneWhenKilled;
    private float vectorAngle;
    public int wayPointInd;
    public float radiusToWaypoint;
    public float raduiusAroundTarget,spinningRadius; // Spinningradius always lover than radiusAroundTarget
    public float circlingSpeed;
    private float pos,goingdownTimer;
    public float _speed;

    // States and enums
    public enum State
    {
        Idle,
        Patroling,
        Attacking,
        Chase,
        GoingDown
    }

    public State state;
    
    
    // bools && triggers
    public bool aboveGround;
    public bool SpawnAfterKilled;
    public bool seesPlayer, testOn, circlingThePlayer,attackTest, alwaysChasePlayer;
    private bool shouldLerp, lerpHasStarted, ghostDying, walking, belowGround, attacking,goingDown;

    // rest
    public GameObject[] MonsterWaypoints;
    public GameObject arm1, arm2, arm3;
    private GameObject Player;
    private NavMeshAgent ghost;
    private Rigidbody rb;
    private Animator anim;
    private Vector3 monsterOrgPos;
    private AnimationTrack AdjustSpeed;
    public Light chestLight;
    private Color redlight, greenlight;
    private Vector3 undergroundPos;
    public LayerMask Ground;
    
    
    
    void Start()
    {
       // anim.SetBool("Chase", false);
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
                Attacking();
                break;
            case State.Chase:
                Chase();
                break;
            case State.Patroling:
                Patroling();
                LookingForPlayer();
                break;
            case State.GoingDown:
                GoingDown();
                break;
        }


         if (state == State.Attacking)
         {
             arm1.GetComponent<BoxCollider>().enabled = true;
             arm2.GetComponent<BoxCollider>().enabled = true;
             arm3.GetComponent<BoxCollider>().enabled = true;
         }
         else
         {
             arm1.GetComponent<BoxCollider>().enabled = false;
             arm2.GetComponent<BoxCollider>().enabled = false;
             arm3.GetComponent<BoxCollider>().enabled = false;
         }

         #region movement

        if (seesPlayer)
        {
            ghost.updatePosition = true;
            ghost.SetDestination(Player.transform.position);
            
            
            // face the player
            Vector3 direction = (Player.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
            
            if ((distanceToPlayer <= LookRange) && (distanceToPlayer <= awareRadius))
            {

                
            }
           
            
        }
        
        
        chestLight.color = Color.Lerp(Color.green, Color.red, lerpStuff);

        if (ghostDying) // after taking picture of the ghost it dies after killtimer 
        {
            anim.SetBool("Death",true);
            ghost.velocity = Vector3.zero;
            ghost.isStopped = true;
            killTimer += Time.deltaTime; // kill time must be over 0.2 secounds! 
            if (killTimer > 3f)
            {
                if (SpawnAfterKilled)
                {
                    gameObject.transform.position = MonsterWaypoints[wayPointInd].transform.position;
                    ghostDying = false;
                    killTimer = 0;

                }
                else
                {
                    gameObject.SetActive(false);
                    ghostDying = false;
                    killTimer = 0;
                }
                
               //{
               //     gameObject.transform.position = monsterOrgPos; //KILL GHOST INSERT HERE 
               //     
               //     killTimer = 0;
               //     ghostDying = false;
               //
               //
               //}
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

    public void Patroling()
    {
        

        if (lerpStuff > 0)
        {
            lerpStuff -= Time.deltaTime / 0.5f;
            aboveGround = true;
        }
        anim.SetBool("Death",false);
        anim.SetBool("Up",false);
        anim.SetBool("Attack",false);
        anim.SetBool("Down",false);
        anim.SetBool("Walk",true);
        anim.SetBool("Chase", false);
        gameObject.GetComponent<BoxCollider>().enabled = true;
        ghost.acceleration = 8;
        ghost.angularSpeed = 120;
        ghost.speed = 3.5f;

        if(alwaysChasePlayer)
        {
            ghost.SetDestination(Player.transform.position);
        }
        else if (Vector3.Distance(gameObject.transform.position, MonsterWaypoints[wayPointInd].transform.position) >= radiusToWaypoint)
        {
            ghost.SetDestination(MonsterWaypoints[wayPointInd].transform.position);

        }
        else if(Vector3.Distance(gameObject.transform.position, MonsterWaypoints[wayPointInd].transform.position) <= radiusToWaypoint)
        {
            wayPointInd +=1 ;
            if (wayPointInd >= MonsterWaypoints.Length - 1)
            {
                wayPointInd = 0;
            }
        }


        if (state == State.Patroling && seesPlayer)
        {
            state = State.GoingDown;

        }
        


    }

    private void GoingDown()
    {
        
        goingdownTimer += Time.deltaTime;
        anim.SetBool("Down",true);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Monsters/Trundle/Down",GetComponent<Transform>().position);
        if (goingdownTimer > 2)
        {
            goingDown = true;   
        }

        if (goingDown)
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

        else if (distanceToPlayer < awareRadius)
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
        
        anim.SetBool("Chase", true);
        goingDown = false;
        attacking = false;
        
        if (lerpStuff < 1)
        {
            lerpStuff += Time.deltaTime / 0.5f;

        }
        
        GetComponent<BoxCollider>().enabled = false;
        attacking = false;
        
        if (!belowGround)
        {
            anim.SetBool("Walk",false);
            anim.SetBool("Up",false);
            anim.SetBool("Down",true);
            belowGround = true;

        }
        
        
        belowTimer += Time.deltaTime;
        if (!attackTest && belowTimer >= Random.Range(10, 30))
        {
            state = State.Attacking;
             belowTimer = 0;
        }
        else if (attackTest && belowTimer > attackTimer)
        {
            state = State.Attacking;
            belowTimer = 0;
        }

        ghost.speed = distanceToPlayer / 3;
        if (distanceToPlayer < raduiusAroundTarget)
        {
            float startAngle;
            if (!circlingThePlayer)
            {
                RaycastHit hit;
                if (Physics.Raycast(new Vector3(transform.position.x,500,transform.position.z), -Vector3.up, out hit,Mathf.Infinity, Ground))
                {
                    startAngle = Vector3.Angle(transform.position, Player.transform.position);
                    ghost.speed = circlingSpeed;
                    float x = Mathf.Cos(startAngle) * spinningRadius;
                    float y = Mathf.Sin(startAngle) * spinningRadius;
                    float height = hit.transform.position.y;
                    Vector3 targetPos = new Vector3(x, height, y);
                    targetPos = Player.transform.position + targetPos;
                    ghost.SetDestination(targetPos);
                    circlingThePlayer = true;
                    // transform.LookAt(targetPos);
                }
                
            }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(new Vector3(transform.position.x,500,transform.position.z), -Vector3.up, out hit,Mathf.Infinity,Ground))
                {
                    ghost.acceleration = 100;
                    ghost.angularSpeed = 100;
                    pos += Time.deltaTime * circlingSpeed;
                    ghost.speed = _speed; 
                    float angle = pos * 3.14159f;
                    float x = Mathf.Cos(angle) * spinningRadius;
                    float y = Mathf.Sin(angle) * spinningRadius;
                    float height = hit.point.y;
                    Vector3 targetPos = new Vector3(Player.transform.position.x + x, height, Player.transform.position.z + y);
                    //targetPos = Player.transform.position + targetPos;
                    transform.position = targetPos;
                    // ghost.SetDestination(targetPos);
                    // transform.LookAt(targetPos);
                    _speed = circlingSpeed * 50;
                    
                }
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
        if(distanceToPlayer > awareRadius)
        {
            state = State.Chase;
            uptime = 0;
        }
        
        circlingThePlayer = false;
        belowGround = false;
        ghost.SetDestination(Player.transform.position);
        transform.LookAt(Player.transform.position);
        if (distanceToPlayer < spinningRadius / 2)
        {
            if (!attacking)
            {
                ghost.speed = 2;
                anim.SetBool("Up",true);
                anim.SetBool("Attack",true);
                anim.SetBool("Down",false);
                attacking = true;
            }
            else if(attacking)
            {
                GetComponent<BoxCollider>().enabled = true;
                uptime += Time.deltaTime;
                anim.SetBool("Up",false);
                if(distanceToPlayer > 3f)
                    
                {
                    ghost.speed = 7.5f;
                    
                }
                else if (distanceToPlayer < 3f)
                {
                    ghost.speed = 6f;
                }

               // if (distanceToPlayer < 1f)
               // {
               //     SceneManager.LoadScene(GoToSceneWhenKilled);
               // }
                if (uptime >= 6)
                {
                    state = State.Chase;
                    uptime = 0;
                }

            }
        }
    }
    
    
    
    
    #region debug
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,awareRadius);
        Vector2 debugger = new Vector2(transform.forward.x, transform.forward.z);
        debugger = Quaternion.Euler(0, 0, -FOV / 2) * debugger;
        UnityEngine.Debug.DrawLine(transform.position, transform.position + (new Vector3(debugger.x,0,debugger.y)) * LookRange);
        debugger = Quaternion.Euler(0, 0, FOV) * debugger;
        UnityEngine.Debug.DrawLine(transform.position, transform.position + (new Vector3(debugger.x,0,debugger.y)) * LookRange);
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
            UnityEngine.Debug.Log("monster hit");
            ghostDying = true;

        }
    }
    
    
    
}



