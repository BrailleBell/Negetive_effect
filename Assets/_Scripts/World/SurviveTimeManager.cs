using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        timeText.text = PlayerPrefs.GetInt("timer").ToString();

        Debug.Log(timeText.text);
    }

    //Dont understand how this works tbh, gonna have to look more into it
    private void TimeCheck()
    {
        if(GameManager.Hour == 01)
        {
            PlayerPrefs.SetInt("timer", 0);
        }
    }
}
