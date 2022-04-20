using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public GameObject Player;

    public int SceneToGoTo;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("XR Origin");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            SceneManager.LoadScene(SceneToGoTo);
        }
        
        if (other.CompareTag("DeathBarrier"))
        {
            SceneManager.LoadScene(SceneToGoTo);
        }
    }
}
