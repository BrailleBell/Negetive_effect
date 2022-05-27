using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerTEST : MonoBehaviour
{
    public GameObject Player;
    public static Vector3 lastPostPos = new Vector3(-3,0,-3);
    public int SceneToGoTo;
    public float respawnTimer;
    private float respawnTimerCounter;
    private bool DeathTimer;


    private void Awake()
    { 
        GameObject.Find("FPSPlayer!").transform.position = lastPostPos;
    }

    void Start()
    {
        Player = GameObject.Find("FPSPlayer!");
        
    }

    void Update()
    {
        if (DeathTimer) // A bool, checks if the player has died
        {
            respawnTimerCounter += Time.deltaTime;
            if (respawnTimerCounter > respawnTimer)
            {
                respawnTimerCounter = 0;
                //Write here, everything that should happen before death
                
                
                
                
                Dying();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            DeathTimer = true;
            Debug.Log("DeathCounter " + respawnTimerCounter);
        }
        
        if (other.CompareTag("DeathBarrier"))
        {
            DeathTimer = true;
        }
    }


    public void Dying()
    {
        // Write everything that happens during death
        transform.position = lastPostPos;
        // SceneManager.LoadScene(SceneToGoTo);
        
        
        
        
        //This is last entry
        DeathTimer = false;
        

    }
}
