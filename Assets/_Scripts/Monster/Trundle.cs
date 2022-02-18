
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class Trundle : MonoBehaviour
{
    private GameObject Player;
    private NavMeshAgent ghost;
    public float distanceToPlayer;
    private float lerp;
    public float aboveTimer, belowTimer;
    public bool aboveGround, notTestKill;
    private bool shouldLerp, lerpHasStarted, ghostDying, walking;
    private float startTime, killTimer;
    private Rigidbody rb;
    public int GoToSceneWhenKilled;
    private Animator anim;
    private float groundlevel, baseoffset, attackDist;
    private Vector3 monsterOrgPos;
    private AnimationTrack AdjustSpeed;


    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        ghost = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        baseoffset = ghost.baseOffset;
        monsterOrgPos = transform.position;

    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);

        Debug.Log(distanceToPlayer + " distance to player");
        if (distanceToPlayer >= 150)
        {
            walking = true;
            ghost.SetDestination(Player.transform.position);
            aboveGround = true;
            //  GetComponentInChildren<SkinnedMeshRenderer>().enabled = false
        }
        else if (distanceToPlayer <= 150)
        {
            aboveGround = true;
            //GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
            gameObject.transform.LookAt(Player.transform);

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


        if (ghostDying) // after taking picture of the ghost it dies after 0.5 sec
        {
            ghost.velocity = Vector3.zero;
            ghost.isStopped = true;
            killTimer += Time.deltaTime; // kill time must be over 0.2 secounds! 
            if (killTimer > 0.5f)
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






        // going up and down
        if (distanceToPlayer <= 30 && distanceToPlayer >= 10)
        {
            aboveGround = false;
            Debug.Log("TETSTSATSDKAJSBDKJAHSBDJAHSBD");



            // attacking the player
            if (distanceToPlayer <= 10)
            {
                anim.ResetTrigger("Down");
                anim.SetTrigger("Up");
                anim.SetTrigger("Walk");
                GetComponent<BoxCollider>().enabled = true;
                ghost.speed = 7f;
                aboveTimer += Time.deltaTime;
                if (aboveTimer > 1.5f)
                {




                }

                if (distanceToPlayer < 2)
                {
                    SceneManager.LoadScene(GoToSceneWhenKilled); // kill op hurt player 
                }
            }
        }

        Vector3 currentPos = transform.position;
        if (!aboveGround)
            
        {
            anim.ResetTrigger("Up");
            anim.ResetTrigger("Walk");
            anim.SetTrigger("Down");
            ghost.isStopped = false;
            GetComponent<BoxCollider>().enabled = false;
            aboveTimer = 0;
            // anim.ResetTrigger("Walk");
            ghost.speed = distanceToPlayer / 3;




        }

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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CameraShoot")
        {
            Debug.Log("monster hit");
            ghostDying = true;


        }
    }
    
    
    
}



