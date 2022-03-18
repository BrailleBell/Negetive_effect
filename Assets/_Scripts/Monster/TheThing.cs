
using System;
using System.Collections;
//using TMPro.EditorUtilities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
using UnityEngine.Timeline;
using FMOD;

public class TheThing : MonoBehaviour
{
 // floats && ints
    public float distanceToPlayer;
    private float lerpStuff, lookAngle;
    public float LookRange;
    [Range(0,360)]
    public float FOV;
    public float  awareRadius;
    private float startTime, killTimer, uptime;
    private float groundlevel, baseoffset, attackDist;
    private float vectorAngle;
    public int wayPointInd;
    public float radiusToWaypoint;
    public float raduiusAroundTarget,spinningRadius; // Spinningradius always lover than radiusAroundTarget
    private float pos,goingdownTimer;
    public Material orgMat, hidingMat;

    // States and enums
    public enum State
    {
        Idle,
        Patroling,
        Attacking,
        Chase,
    }

    public State state;
    
    
    // bools && triggers
    public bool aboveGround;
    public bool SpawnAfterKilled;
    public bool seesPlayer, testOn, circlingThePlayer,attackTest, alwaysChasePlayer;
    private bool shouldLerp, lerpHasStarted, ghostDying, walking, attacking;

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
         if(GetComponent<Renderer>().isVisible && distanceToPlayer < awareRadius)

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
        anim.SetBool("Attack",false);
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
        ghost.SetDestination(Player.transform.position);
        ghost.speed = 3.5f;
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Cover");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
                hidingMat = closest.GetComponent<Renderer>().material;
                float lerptimer = Mathf.PingPong(Time.time, 1) / 100f;
                GetComponent<Renderer>().material.Lerp(GetComponent<Renderer>().material, hidingMat, lerptimer);
                ghost.destination = closest.transform.position;

                if(distanceToPlayer > 20)
                {
                    float lerp = Mathf.PingPong(Time.time, 1) / 100f;
                    GetComponent<Renderer>().material.Lerp(GetComponent<Renderer>().material, orgMat, lerp);

                }
            }
            
            
                
            
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
        ghost.SetDestination(Player.transform.position);
        transform.LookAt(Player.transform.position);
        if (distanceToPlayer < spinningRadius / 2)
        {
            if (!attacking)
            {
                ghost.speed = 5;
                anim.SetBool("Attack",true);
                attacking = true;
            }
            else if(attacking)
            {
                GetComponent<BoxCollider>().enabled = true;
                if(distanceToPlayer > 3f)
                    
                {
                    ghost.speed = 7.5f;
                    
                }
                else if (distanceToPlayer < 3f)
                {
                    ghost.speed = 6f;
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