using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class TheMaiden : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {

        Player = GameObject.FindWithTag("Player");
        ghost = GetComponent<NavMeshAgent>();
        if (GetComponent<Animator>())
        {
            anim = GetComponent<Animator>();
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (!DontFollow)
        {
            ghost.SetDestination(Player.transform.position);
            
        }
        else
        {
            ghost.SetDestination(ghost.transform.position);
        }
        
        
        DistanceToPlayer = Vector3.Distance(ghost.transform.position, Player.transform.position);

        if (DistanceToPlayer < HearingRange)
        {
            ghost.speed = DistanceToPlayer / 3;
        }
        else
        {
            ghost.speed = 5;
        }
        
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
        
        
        
        

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(ghost.transform.position,ghost.destination);
        Gizmos.DrawWireSphere(ghost.transform.position,HearingRange);
        
    }
}
