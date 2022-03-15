using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int film, SceneToGoTo;
    public GameObject VRheadset;
    public GameObject[] spawnPoints;
    public GameObject Player;

    //Camera reloading
    public bool reloaded,reloadReady;
    
    /// <summary>
    /// To be able to trigger time specific events
    /// like saving the game each hour and having the monsters behave differently 
    /// every half an hour (ingame time) etc...
    /// </summary>
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;

    //timer and saving stuff
    public bool SaveGame = false;
    public float timer; //local time
    public float minuteToRealTime; //every half a sec realtime is 1minute ingame (needs to be changed obvs, but this is for testing)

    public static int Minute { get; private set; }
    public static int Hour { get; private set; }

    // Start is called before the first frame update
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        #region Camera
        // ReloadChecks for the camera
        
        reloadReady = true;
        reloaded = false;
        film = 0;
        #endregion
        
        //game starts at 00:00am
        Minute = 0;
        Hour = 00;
        timer = minuteToRealTime;


       DontDestroyOnLoad(this); 
       // DontDestroyOnLoad(VRheadset);
       if(GameObject.Find("__GM").activeInHierarchy == true)
       {
           
       }

    
    }


    private void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            Minute++;
            OnMinuteChanged?.Invoke(); //the "?" is the "null" check instead of putting it into an "if statement"

            if (Minute >= 59)
            {
                Hour++;
                OnHourChanged?.Invoke(); //the "?" is the "null" check instead of putting it into an "if statement"
                Minute = 0;
            }

            timer = minuteToRealTime;
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
