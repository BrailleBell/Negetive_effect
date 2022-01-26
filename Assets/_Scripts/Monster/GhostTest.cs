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
    private Material hidingMat, orgMat;
    private NavMeshAgent ghost;






    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        orgMat = GetComponent<Renderer>().material;
        ghost = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
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
            gameObject.GetComponent<Renderer>().material = orgMat;

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
            }
        }

        ghost.destination = closest.transform.position;
        transform.position =  Vector3.MoveTowards(transform.position,closest.transform.position, 100f * Time.deltaTime);
        Debug.Log("Teleport");

        if (transform.position == closest.transform.position)
        {
            hidingMat = closest.GetComponent<Renderer>().material;
            gameObject.GetComponent<Renderer>().material = hidingMat;

        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
          //  Player
            
        }
    }
}

