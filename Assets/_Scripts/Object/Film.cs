using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Film: MonoBehaviour
{
    public GameManager gm;
    public GameObject Camera;
    
    // Start is called before the first frame update
    private void Start()
    {
        gm = GameObject.Find("__GM").GetComponent<GameManager>();
            Camera = GameObject.Find("PoloroidCamera");
    }

    private void Update()
    {
        if (gm.reloadReady)
        {
            if (Vector3.Distance(gameObject.transform.position, Camera.transform.position) < 0.5f)
            {
                    Debug.Log("reloaded check, reloaded is "+ gm.reloaded);
                    Debug.Log("RELOADED!!");
                    gm.reloaded = true;
                    gm.GetFilm();
                    Destroy(gameObject);
            }
            else
            {
                Debug.Log(Vector3.Distance(gameObject.transform.position, Camera.transform.position));
            }
        }
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (gm.reloadReady)
        {
            if (collision.gameObject.CompareTag("Camera"))
            {
                Debug.Log("reloaded check, reloaded is "+ gm.reloaded);
                Debug.Log("RELOADED!!");
                gm.reloaded = true;
                gm.GetFilm();
                Destroy(gameObject);
                
            }
            
        }

    }
}
