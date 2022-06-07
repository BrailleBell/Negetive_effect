using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class ScellyBoiCopys : MonoBehaviour
{
    private NavMeshAgent ghost;
    public GameObject Player;
    public GameObject MainHost;

    private bool dying,fakeDying;
    private Animator anim;
    private float killTimer;
    private float Dist;
    private SkellyBoi _skellyBoi;
     private PlayerManager pl;

    private SkinnedMeshRenderer meshRend;
    
    // Start is called before the first frame update
    void Awake()
    { 
        meshRend = GetComponentInChildren<SkinnedMeshRenderer>();
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = gameObject.GetComponentInChildren<Animator>();
        MainHost = GameObject.Find("Shellback");
        _skellyBoi = GameObject.Find("Shellback").GetComponent<SkellyBoi>();
        Dist = MainHost.GetComponent<SkellyBoi>().distFromPlayerToSpawn;
        anim.SetBool("Summoned",true);
        anim.SetBool("Attack",true);
        anim.SetBool("Walk",true);
        
        ghost = GetComponent<NavMeshAgent>();
        gameObject.tag = "Monster";
       

    //    anim.SetBool("Flying",true);

    }

    // Update is called once per frame
    void Update()
    {


        // destroys the fake skellyboi when the player dies
        if (Player.GetComponent<PlayerManagerTEST>())
        {
            if (Player.GetComponent<PlayerManagerTEST>().PlayerDied)
            {
                Destroy(gameObject);
            }
        }
        
        if (Player.GetComponent<PlayerManager>())
        {
            if (Player.GetComponent<PlayerManager>().PlayerDied) 
            {
                Destroy(gameObject);
            }
            
        }


        if (Input.GetKeyUp(KeyCode.H))
        {
            fakeDying = true;
        }
        if (Player != null)
        {
            ghost.SetDestination(Player.transform.position);
        }
        else
        {
            Debug.Log("Cant find player");
        }
        
        
        if (dying) // after taking picture of the ghost it dies after killtimer 
        {
            anim.SetBool("CloneDeath",true);
            ghost.velocity = Vector3.zero;
            ghost.isStopped = true;
            killTimer += Time.deltaTime; // kill time must be over 0.2 secounds! 
            if (killTimer > 2f)
            {
                gameObject.SetActive(false);
                
            }
        }
         
        if (fakeDying) // after taking picture of the ghost it dies after killtimer 
        {
                anim.SetBool("CloneDeath",true);
                  
            ghost.SetDestination(transform.position);
            killTimer += Time.deltaTime; // kill time must be over 0.2 secounds! 
            if (killTimer > 2f)
            {
                anim.SetBool("Walk",false);
                anim.SetBool("Summoned",false);
                anim.SetBool("Attack",false);
                meshRend.GetComponent<Renderer>().enabled = false;
                //GetComponent<CapsuleCollider>().enabled = false;
                transform.position = new Vector3(Player.transform.position.x, -5, Player.transform.position.y);
                
                if (killTimer > 6)
                {
                    transform.position = new Vector3(Player.transform.position.x + UnityEngine.Random.Range(-10, 10),
                        Player.transform.position.y,
                        Player.transform.position.x + UnityEngine.Random.Range(-10, 10));
                    while (Vector3.Distance(Player.transform.position, transform.position) < 2)
                    {
                        transform.position = new Vector3(Player.transform.position.x + UnityEngine.Random.Range(-10, 10),
                            Player.transform.position.y,
                            Player.transform.position.x + UnityEngine.Random.Range(-10, 10));

                    }
                    
                    meshRend.GetComponent<Renderer>().enabled = true;
                    //GetComponent<CapsuleCollider>().enabled = true;
                    fakeDying = false;
                    killTimer = 0;
                    ghost.SetDestination(Player.transform.position);

                    anim.SetBool("CloneDeath",false);
                    anim.SetBool("Summoned",true);
                    anim.SetBool("Attack",true);
                    anim.SetBool("Walk",true);
                   
                }
            }
        }

        if (MainHost.activeInHierarchy == false)
        {
            dying = true;
        }
        else if(MainHost == null)
        {
            dying = true;
        }

        if (_skellyBoi.dying)
        {
            dying = true;
        }



    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CameraShoot")
        {
            UnityEngine.Debug.Log("monster hit");
            fakeDying = true;

        }

        if (other.CompareTag("Player"))
        {
            fakeDying = true;
        }
    }
}
