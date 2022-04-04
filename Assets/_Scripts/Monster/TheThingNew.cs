using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class TheThingNew : MonoBehaviour
{
    private GameObject Player;
    private Animator anim;
    private NavMeshAgent ghost;
    public float visibilityRange;
    public float DistanceToPlayer;
    private bool dying;
    private float killTimer;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        ghost = GetComponent<NavMeshAgent>();
        ghost.speed = 0;


    }

    // Update is called once per frame
    void Update()
    {
        DistanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
        if (DistanceToPlayer < visibilityRange)
        {
            anim.SetBool("Attack",true);
            ghost.SetDestination(Player.transform.position);
            ghost.speed = 15;

        }
        
        if (dying) // after taking picture of the ghost it dies after killtimer 
        {
            anim.SetBool("Death",true);
            ghost.velocity = Vector3.zero;
            ghost.isStopped = true;
            GetComponent<BoxCollider>().enabled = false;
            killTimer += Time.deltaTime; // kill time must be over 0.2 secounds! 
            if (killTimer > 3f)
            {
                gameObject.SetActive(false);
            }
  
        }
        

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,visibilityRange);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CameraShoot")
        {
            UnityEngine.Debug.Log("monster hit");
            dying = true;

        }
    }
}
