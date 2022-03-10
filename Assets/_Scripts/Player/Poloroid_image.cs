using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Poloroid_image : MonoBehaviour
{

    public Material shaderMat;
    public GameObject Picture;
    private Texture2D screenCapture;
    //public Image photoDisplayArea;
 //  public Material mat;
    private Camera camCam;
    public RenderTexture PoleroidImage;
    public int x = 200;
    public int y = 200;
    private float timerForPolaroid, timerForFlash;
    public GameObject flash, UVLight,Reloadedfilm;
    private bool lightsOn;
    public GameObject cameraRange;
    public GameManager gm;
    private TextMesh filmText;
    public GameObject oldFilm;
    
    //Light
    public GameObject reloadedlamp;

    //Sound stuff
    public AudioSource audioSource;
    public AudioClip audioClip;

    //VR input stuff
    public InputActionReference reloadReference = null;

    private void Start()
    {
      //  Instantiate(Picture, transform.position, Quaternion.identity);
        //screenCapture = new Texture2D(516, 516, TextureFormat.RGB24, false);

        camCam = transform.GetChild(0).GetComponent<Camera>();
        flash.SetActive(false);
        lightsOn = false;
        cameraRange.SetActive(false);
        gm = GameObject.Find("__GM").GetComponent<GameManager>();
//        filmText = GameObject.Find("FilmText").GetComponent<TextMesh>();
        Reloadedfilm.SetActive(false);

      //  reloadReference.action.started += ReloadCamera;
    }


    private void Update()
    {

        if (gm.reloaded)
        {
            reloadedlamp.SetActive(true);

        }
        else
        {
            reloadedlamp.SetActive(false);
            
        }


        if (lightsOn)
        {
            Debug.Log("Shoot picture");
            flash.SetActive(true);
            timerForFlash += Time.deltaTime;

            if (timerForFlash > 0.2f)

            {
                flash.SetActive(false);

                lightsOn = false;

                timerForFlash = 0;

                screenCapture = toTexture2D(PoleroidImage);

                Picture.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial = new Material(shaderMat);

                Picture.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial

                    .SetTexture("_MainTex", screenCapture);

                Instantiate(Picture, transform.position, Quaternion.Euler(90, 180, 0));

                timerForPolaroid = +Time.deltaTime;

                // if(cameraRange.GetComponent<MeshCollider>()) 

                cameraRange.SetActive(false);

                gm.reloaded = false;

                ReloadCamera();

                if (timerForPolaroid > 5)

                {

                    Debug.Log(timerForPolaroid);


                }
            }

        }

        if (!gm.reloaded && gm.reloadReady)
        {
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag("Film");
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in gos)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
//                    Debug.Log(closest.transform.position);
                    Debug.DrawRay(gameObject.transform.position,closest.transform.position,Color.green);
                    if (Vector3.Distance(transform.position, closest.transform.position) < 0.7f)
                    {
                        Reloadedfilm.SetActive(true);
                        

                        /*if (Vector3.Distance(transform.position, closest.transform.position) < 0.4f)
                        {
                            Debug.Log("reloaded check, reloaded is "+ gm.reloaded);
                            Debug.Log("RELOADED!!");
                            gm.reloaded = true;
                            gm.reloadReady = false;
                            closest.SetActive(false);
                        }
                        */
                        
                        

                    }
                    else
                    {
                        Reloadedfilm.SetActive(false);

                    }
                }
            }
        }
        else if (!gm.reloadReady)
        {
            Reloadedfilm.SetActive(false);
        }


        #region pccontroller

        if (Input.GetMouseButtonDown(0) && gm.reloaded)
        {
            cameraRange.SetActive(true); 
            gm.SnapPic(); 
            reloadedlamp.SetActive(true);
            gm.reloadReady = false;
            lightsOn = true;
            Debug.Log("Took a picture");
//
        }
        // else if(Input.GetKeyUp(KeyCode.R) && gm.reloadReady)
        // {
        //     Debug.Log("reloaded check, reloaded is "+ gm.reloaded);
        //     gm.reloaded = true;
        //     gm.GetFilm();
        //
        //}

        /*if (Input.GetKeyDown(KeyCode.Space))
           {
               Debug.Log("Space pressed");

               if (!gm.reloadReady)
               {

                   ReloadCamera();

                   // instaniate film without glow
                   //play reload sound 
                   //gm.reloadReady = true;
                   //Debug.Log("Reload Ready, insert film");
                   //Instantiate(oldFilm, transform.position, quaternion.identity);

               }
           }*/

        //   if (Input.GetKey(KeyCode.Mouse1))
        //   {
        //       ReloadCamera();
        //   }
        //   else
        //   {
        //       UVLight.SetActive(false);
        //   }


        //        filmText.text = gm.film.ToString();


        #endregion

    }

    private void OnDestroy()
    {
        //reloadReference.action.started -= ReloadCamera;
    }

    public void ReloadCamera()
    {
        //delete this after testing if the action works lmao
       // bool isActive = !gameObject.activeSelf;
       // gameObject.SetActive(isActive);
        //
        
        Debug.Log("A pressed");
        gm.reloadReady = true;
        Instantiate(oldFilm, transform.position, quaternion.identity);
        Debug.Log("Check0");
         Debug.Log("check1");
         //play reload sound 
       
      Debug.Log("Reload Ready, insert film");
        //Instantiate(oldFilm, transform.position, quaternion.identity);

    }

    public void TakePictureInVR()
    {
        if (gm.reloaded)
        {
            cameraRange.SetActive(true); 
            gm.SnapPic(); 
            reloadedlamp.SetActive(true);
            lightsOn = true;
            Debug.Log("Took a picture");
            
            

        }
        else if(!gm.reloaded)
        {
            // Play clicking sound only 
            
            
        }
        
    }

   // public void Snapshot() //sound of taking a photo
   // {
   //     audioSource.PlayOneShot(audioClip);
   //     Debug.Log("Photo taken");
   // }

    //take the render texture and turn it into it's own 2d texture
    Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(x, y, TextureFormat.RGB24, false);
        // ReadPixels looks at the active RenderTexture.
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }
    
}
