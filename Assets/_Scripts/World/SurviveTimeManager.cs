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

        //Debug.Log("is it working?");
    }

    private void OnDisable()
    {
        GameManager.OnHourChanged -= TimeCheck;

        //Debug.Log("is it working?");
    }

    private void Update()
    {
        timeText.text = PlayerPrefs.GetInt("timer").ToString();

        //Debug.Log("is it working?");
    }

    //Dont understand how this works tbh, gonna have to look more into it
    private void TimeCheck()
    {
        if(GameManager.Hour == 01 && GameManager.Minute == 01)
        {
            PlayerPrefs.SetInt("timer", 01);

            //Debug.Log("is it working?");
        }

        Debug.Log("is it working?");
    }
}
