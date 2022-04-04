using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int film, SceneToGoTo;
    public GameObject VRheadset;
    public GameObject[] spawnPoints;

    [Header("Gameobjects")]
    public GameObject Player;
    public GameObject MonsterSpawn;

    public bool isCreated;

    //Camera reloading
    public bool reloaded,reloadReady;
    
    /// <summary>
    /// To be able to trigger time specific events
    /// like saving the game each hour and having the monsters behave differently 
    /// every half an hour (ingame time) etc...
    /// </summary>
    public static UnityEvent OnMinuteChanged;
    public static UnityEvent OnHourChanged;

    //timer and saving stuff
    public bool SaveGame = false;
    [Header("Time")]
    static float timer; //local time
    public float minuteToRealTime; //every half a sec realtime is 1minute ingame (needs to be changed obvs, but this is for testing)

    public static float getTimer => timer;

    int previousHour;

    // Start is called before the first frame update
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        MonsterSpawn = GameObject.FindGameObjectWithTag("MonsterSpawn");
    }
    void Start()
    {
        MonsterSpawn.SetActive(false);
        #region Camera
        // ReloadChecks for the camera
        
        reloadReady = true;
        reloaded = false;
        film = 0;
        #endregion
        
        //game starts at 00:00am
        timer = minuteToRealTime;

       DontDestroyOnLoad(this); 
       // DontDestroyOnLoad(VRheadset);
       if(GameObject.Find("__GM").activeInHierarchy == true)
       {
           
       }

    
    }


    private void Update()
    {
        timer += Time.deltaTime; //makes so that the times goes forward
        int min = (int)timer / 60 % 60; //ingame minutes
        int hour = (int)timer / 3600 % 24; //ingame hours

        Debug.Log(hour + ":" + min);

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(2))
        {
            //this indicates that something will happen every hour 
            if (previousHour != hour) //measures that it has reached a new hour and then if it has then whatever inside this will happen
            {
                OnHourChanged?.Invoke(); /*the "?" is the "null" check instead of putting it into an "if statement"
                                          * starts the event OnHourChanged*/
                previousHour = hour; //makes sure it resets the function
            }

            timer = minuteToRealTime;
            

            //At 25 minutes it spawnes a monster
            if (min >= 25)
            {
                if (!isCreated)
                {
                    MonsterSpawn.SetActive(true);
                    Debug.Log("spawned");

                    isCreated = true;
                }

            }
//            Debug.Log("Scene is loaded MainScene");
        }
        
    }


    // films
    public void SnapPic()
    {

    }

    public void  GetFilm()
    {
        film++ ;
      
    }
    
}
