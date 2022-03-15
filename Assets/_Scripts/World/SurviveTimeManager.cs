using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// This saves the game at every ingame hour
/// </summary>
public class SurviveTimeManager : MonoBehaviour
{
    public static Text timeText;
  
    private void OnEnable()
    {
        GameManager.OnHourChanged += TimeCheck;
    }

    private void OnDisable()
    {
        GameManager.OnHourChanged -= TimeCheck;
    }

    private void Update()
    {
        
        //Debug.Log("is it working?");
    }

    //Dont understand how this works tbh, gonna have to look more into it
    private void TimeCheck()
    {
        if(GameManager.Hour == 01)
        {
            if(GameManager.Minute == 59)
            {
                SceneManager.LoadScene(4);
                Debug.Log("working?");
            }
        }
    }
}
