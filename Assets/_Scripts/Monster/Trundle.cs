
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Trundle : MonoBehaviour
{
    private GameObject Player;
    private NavMeshAgent ghost;
    public float distanceToPlayer;
    private float lerp;
    public float walkRadius;
    public float aboveTimer, belowTimer;
    public bool aboveGround;
    private bool shouldLerp, lerpHasStarted;
    private float startTime;
    public float _interval, offset, riseSpeed;
    private Rigidbody rb;
    private Animator anim;
    private float groundlevel, baseoffset;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        ghost = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        baseoffset = ghost.baseOffset;

    }

    void Update()
    {
        
        Debug.Log("startime is " + startTime);
        Debug.Log(" Ghost baseofset " + ghost.baseOffset);
        distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
        ghost.destination = Player.transform.position;
        //  gameObject.transform.LookAt(Player.transform);
        
        
        // attacking the player
        if (distanceToPlayer < 10)
        {
            anim.ResetTrigger("Up");
            anim.ResetTrigger("Down");
            anim.SetTrigger("Attack");
        }
        
        
        // going up and down
        if (distanceToPlayer <= 40)
        {
            aboveGround = true;
        }
        else
        {
            aboveGround = false;
        }

        Vector3 currentPos = transform.position;
        if (!aboveGround)
            //&& currentPos.y > groundlevel - offset)
        {
            aboveTimer = 0;
            anim.ResetTrigger("Up");
            anim.SetTrigger("Down");
            
            
           // ghost.baseOffset = -5f;
           // GetComponentInChildren<BoxCollider>().enabled = false;
           // if (currentPos.y - (riseSpeed * Time.deltaTime) < groundlevel - offset)
           // {
           //     transform.position = new Vector3(currentPos.x, groundlevel - offset, currentPos.z);
           // }
           // else
           // {
           //     transform.Translate(Vector3.down * riseSpeed * Time.deltaTime);
           // }


          //  shouldLerp = true; 
            
           // if (shouldLerp)
           // {
           //     if (!lerpHasStarted)
           //     {
           //         lerpHasStarted = true;
           //         startTime = Time.time;
           //     }
           //     
           //     transform.Translate();
           //     transform.position = LerpHelper(transform.position, underGroundPos, startTime, _interval);
           //     if (transform.position == underGroundPos)
           //     {
           //         transform.position = underGroundPos;
           //         shouldLerp = false;
           //         lerpHasStarted = false;
           //         
           //     }
                
             //   rb.useGravity = false;
             //   Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
             //   randomDirection += transform.position;
             //   NavMeshHit hit;
             //   NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
             //   Vector3 finalPosition = hit.position;
             //   aboveTimer = 0;
           // }
            
        }
        

        
         
        if (aboveGround)
            //currentPos.y < groundlevel + offset)
        {
            anim.ResetTrigger("Down");
            anim.SetTrigger("Up");
            aboveTimer += Time.deltaTime;
            if (aboveTimer > 1.5f)
            {
                anim.ResetTrigger("Down");
                anim.SetTrigger("Walk");
                float randomDownTime = Random.Range(10, 50);
                if (aboveTimer > randomDownTime)
                {
                    aboveGround = false;
                }
                
                



            }


            //  if (currentPos.y + (riseSpeed * Time.deltaTime) > groundlevel + offset)
              //  {
              //      transform.position = new Vector3(currentPos.x, groundlevel + offset, currentPos.z);
              //      GetComponentInChildren<BoxCollider>().enabled = true;
              //      ghost.radius = baseoffset;
              //  }
              //  else
              //  {
              //      transform.Translate(Vector3.up * riseSpeed * Time.deltaTime);
              //      GetComponentInChildren<BoxCollider>().enabled = true;
              //      ghost.radius = baseoffset;
              //  }

            
           // shouldLerp = true;
           // if (shouldLerp) 
           // {
           //     if (!lerpHasStarted)
           //     {
           //         lerpHasStarted = true;
           //         startTime = Time.time;
           //     }
           //     transform.position = LerpHelper(transform.position, aboveGroundPos, startTime, _interval);
           //     if (transform.position == aboveGroundPos)
           //     {
           //         
           //         
           //         shouldLerp = false;
           //         lerpHasStarted = false;
           //     }
//

            }
        }

    private void FixedUpdate()
    {
      //  if (Physics.Raycast(transform.position + new Vector3(0, offset * 2, 0), Vector3.down, out RaycastHit hit,
      //          offset * 3, 1 << LayerMask.NameToLayer("Ground")))
      //  {
      //      groundlevel = hit.point.y;
      //  }
        
        
        
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
}



