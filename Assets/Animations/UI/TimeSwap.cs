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
    public string[] ams = {"00:00", "00:00", "01:00", "02:00", "03:00", "04:00", "05:00" };
    private int onTheHour = 0;
    public bool changed;
    private Transform P;

    private void Update()
    {
        if (changed)
        {
            Canvas.transform.position = P.position;
            Canvas.transform.rotation.eulerAngles.Set(P.rotation.eulerAngles.x, P.rotation.eulerAngles.y, 0); //= new Vector3(P.rotation.eulerAngles.x, P.rotation.eulerAngles.y, 0);
            Canvas.transform.Rotate(0, 0, 0);
        }
    }


    //will put the clock display infront of the player and display the ingame time.
    //lastly it will increase the integer value of onTheHour, making it easier to change the time.
    public void hour(Transform T)
    {
        if (onTheHour > ams.Length)
        {
            Debug.Log("outside the bounds of the AMS array");
        }
        else
        {
            clock.text = ams[onTheHour];
            ani.SetBool("Hour", true);
            changed = true;
            P = T.GetChild(0).GetChild(0);
        }
    }

    //sets the Hour parameter to false afther the animation has played
    public void stop()
    {
        ani.SetBool("Hour", false);
    }

    //made for the 6am event. 
  public  void ChangeTime (string T)
    {
        clock.text = T;
        ani.SetBool("Hour", true);
        changed = false;
    }

    public void NewHour()
    {
        onTheHour++;
    }
   
}
