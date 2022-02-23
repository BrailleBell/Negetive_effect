using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public int film;
    public GameObject VRheadset;

    public GameObject[] spawnPoints;

    //saving stuff
    public float Timer = 0;
    public float TimeCheck = 180; //180 = 3 minutes
    public bool SaveGame = false;

    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        LoadGameFunction();

       DontDestroyOnLoad(this); 
       // DontDestroyOnLoad(VRheadset);
       if(GameObject.Find("__GM").activeInHierarchy == true)
       {
           
       }

    
    }


    private void Update()
    {
        Timer = Timer + 1 * Time.deltaTime;

        if(Timer >= TimeCheck)
        {
            SaveGame = true;
        }

        //need to find out how to save the time and makes sure it stays at that time
        if(SaveGame == true)
        {
            SaveGameFunction();
            LoadGameFunction();
            Timer = 0; //resets the timer
            SaveGame = false;
            Debug.Log(SaveGame);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void SaveGameFunction()
    {
        PlayerPrefs.SetInt("Films", film);
    }

    public void LoadGameFunction()
    {
        film = PlayerPrefs.GetInt("Films");
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
