using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.AI;

public class GhostTest : MonoBehaviour
{
    
    private GameObject Player;
    private Vector3 targetPos;
    private int dir;
    private bool seen, seenafterTeleport;
    public float timer;
    private GameObject[] cover;
    private Material hidingMat;
    public Material orgMat;
    private NavMeshAgent ghost;
    public GameObject cabin;






    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
     //   orgMat = gameObject.GetComponent<Renderer>().material;
        ghost = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Vector3.Distance(ghost.transform.position,targetPos));
        if (Vector3.Distance(ghost.transform.position, targetPos) < 100)
        {


          //  Debug.Log(gameObject.GetComponent<MeshRenderer>().material);
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
                float lerp = Mathf.PingPong(Time.time, 2.0f) / 2.0f;
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

        }
        else
        {
            ghost.destination = cabin.transform.position;
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
                hidingMat = closest.GetComponent<Renderer>().material;
                float lerp = Mathf.PingPong(Time.time, 2.0f) / 2.0f;
                GetComponent<Renderer>().material.Lerp(GetComponent<Renderer>().material, hidingMat, lerp);
            }
        }

        ghost.destination = closest.transform.position;
        transform.position =  Vector3.MoveTowards(transform.position,closest.transform.position, 100f * Time.deltaTime);
        Debug.Log("Teleport");
        

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
          //  Player
            
        }
    }
}

