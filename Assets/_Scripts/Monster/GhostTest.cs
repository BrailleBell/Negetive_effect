using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.AI;
using Random = System.Random;

public class GhostTest : MonoBehaviour
{
    
    private GameObject Player;
    private Vector3 targetPos;
    private int dir;
    private bool seen, seenafterTeleport;
    public float timer;
    private GameObject[] cover;
    public Material hidingMat;
    public Material orgMat;
    private NavMeshAgent ghost;
    public GameObject cabin;
    public AudioClip[] hidingSound;
    public AudioClip killSound;
    private AudioSource sound;
    private Vector3 ghostPos;
    private float attackDist = 3;






    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
     //   orgMat = gameObject.GetComponent<Renderer>().material;
        ghost = GetComponent<NavMeshAgent>();
        sound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
            ghostPos = ghost.transform.position;
            targetPos = Player.transform.position; // finds the player
            gameObject.transform.LookAt(targetPos);


            if (GetComponent<Renderer>().isVisible) // checks if visible or not
            {
                seen = true;
                Debug.Log("SYNLIG");

            }
            else
            {
                seen = false;
                Debug.Log("IKKE SYNLIG");
                timer = 0;
                ghost.destination = targetPos;
                gameObject.transform.LookAt(Player.transform.position);
                ghost.speed = 8;
                float lerp = Mathf.PingPong(Time.time, 1) / 100f;
                GetComponent<Renderer>().material.Lerp(GetComponent<Renderer>().material, orgMat, lerp);
            }

            if (seen)
            {
                timer += Time.deltaTime;
                if (timer > 1.5f)
                {
                    teleportaway();
                    ghost.speed = 3.5f;
                }
                seen = false;
            }
            if(Vector3.Distance(ghostPos,Player.transform.position) < 2)
            {
                Debug.Log("dead");
            }
            
        




    }

    
    void teleportaway() // teleports the ghost to closest cover
    {
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
               // hidingMat = closest.GetComponent<Renderer>().material;
                float lerptimer = Mathf.PingPong(Time.time, 1) / 100f;
                GetComponent<Renderer>().material.Lerp(GetComponent<Renderer>().material, hidingMat, lerptimer);
            }
        }

        ghost.destination = closest.transform.position;
        transform.position =  Vector3.MoveTowards(transform.position,closest.transform.position, 20f * Time.deltaTime);
        {
            
            
        }
         
        
        
        sound = null;
        Debug.Log("Teleport");
        

    }
    
}

