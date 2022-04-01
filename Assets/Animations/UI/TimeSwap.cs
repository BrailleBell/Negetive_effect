using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSwap : MonoBehaviour
{
    public string time;
    public Text clock;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    public void ChangeTime (string T)
    {
        clock.text = T;
    }
}
