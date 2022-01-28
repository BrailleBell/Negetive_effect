using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.Rendering;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using UnityEngine.AI;
public class Trundle : MonoBehaviour
{
    private GameObject Player;
    private NavMeshAgent ghost;
    
    // Start is called before the first frame update
    void Start()
    {
        ghost = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        ghost.destination = Player.transform.position;
        
        
    }
}
