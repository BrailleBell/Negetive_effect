using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Film: MonoBehaviour
{
    public GameManager gm;
    
    // Start is called before the first frame update
    private void Start()
    {
        gm = GameObject.Find("__GM").GetComponent<GameManager>();


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Film")
        {
            collision.gameObject.SetActive(false);
            gm.GetFilm();
            
        }
    }
}
