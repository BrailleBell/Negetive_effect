using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GhostTest : MonoBehaviour
{
    
    private GameObject Player;
    public float ghostSpeed;
    private Vector3 targetPos;
    private GameObject Ghost;
    private Rigidbody rb;
    private int dir;
    private bool seen, seenafterTeleport;
    public float timer;
    private GameObject[] cover;
    public GameObject coverTarget;
    
    
    
    
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Ghost = gameObject;
        rb = GetComponent<Rigidbody>();
        

    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log(Player.transform.position);
        // behaviour 
        targetPos = Player.transform.position;
        Ghost.transform.position =
            Vector3.MoveTowards(gameObject.transform.position, targetPos, ghostSpeed * Time.deltaTime);
        
        gameObject.transform.LookAt(Player.transform.position);

        


        if (GetComponent<Renderer>().isVisible)
        {
            seen = true;
            Debug.Log("SYNLIG");
            
        }
        else
        {
            seen = false;
            Debug.Log("IKKE SYNLIG");
            timer = 0;
            targetPos = Player.transform.position;
            Ghost.transform.position =
                Vector3.MoveTowards(gameObject.transform.position, targetPos, ghostSpeed * Time.deltaTime);
        
            gameObject.transform.LookAt(Player.transform.position);

            
            
        }

        if (seen)
        {
            timer += Time.deltaTime;
            if (timer > 1.5f)
            {
                teleportaway();
                

            }

            seen = false;

        }
        

    }

    
    void teleportaway()
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

        transform.position =  Vector3.MoveTowards(transform.position,closest.transform.position, 100f * Time.deltaTime);
        Debug.Log("Teleport");

        if (transform.position == closest.transform.position)
        {
        
            
        }

    } 

}

