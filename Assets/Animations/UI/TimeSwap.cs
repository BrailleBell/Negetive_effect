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

    private void Start()
    {
        Canvas = transform.GetChild(0).gameObject;
    }
    public void hour(string T, Vector3 V)
    {
        clock.text = T; 
        ani.SetBool("Hour", true);
        Canvas.transform.position = V;
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
