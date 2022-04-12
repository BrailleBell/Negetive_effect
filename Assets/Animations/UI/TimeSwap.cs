using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSwap : MonoBehaviour
{
    public  Animator ani;
    public string time;
    public  Text clock;
    public GameObject Canvas;
    public string[] ams = { "01:00", "02:00", "03:00", "04:00", "05:00" };
    private int onTheHour = 0;

 
    public void hour(Transform T)
    {
        clock.text = ams [onTheHour]; 
        ani.SetBool("Hour", true);
        Canvas.transform.position = T.forward * 2;
        onTheHour++;
    }

    public void stop()
    {
        ani.SetBool("Hour", false);
    }

  public  void ChangeTime (string T)
    {
        clock.text = T;
        ani.SetBool("Hour", true);
    }
   
}
