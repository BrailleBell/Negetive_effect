using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Film: MonoBehaviour
{
    public GameManager gm;
    public GameObject Camera;
    public GameObject[] ArmPacks;
    public bool gripped;
    
    // Start is called before the first frame update
    private void Start()
    {
        gm = GameObject.Find("__GM").GetComponent<GameManager>();
            Camera = GameObject.Find("PoloroidCamera");
            ArmPacks = GameObject.FindGameObjectsWithTag("ArmPack");
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
                    gm.reloadReady = false;
                    Destroy(gameObject);
            }
            else
            {
            //    Debug.Log(Vector3.Distance(gameObject.transform.position, Camera.transform.position));
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
                gm.reloadReady = false;
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.CompareTag("ArmPack"))
        {
            gm.GetFilm();
            Destroy(gameObject);
        }
    }

}
