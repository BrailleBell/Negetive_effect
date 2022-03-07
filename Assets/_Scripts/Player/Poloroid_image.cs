using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
    
    //Light
    public GameObject reloadedlamp;

    //Sound stuff
    public AudioSource audioSource;
    public AudioClip audioClip;

    private void Start()
    {
      //  Instantiate(Picture, transform.position, Quaternion.identity);
        //screenCapture = new Texture2D(516, 516, TextureFormat.RGB24, false);

        camCam = transform.GetChild(0).GetComponent<Camera>();
        flash.SetActive(false);
        lightsOn = false;
        cameraRange.SetActive(false);
        gm = GameObject.Find("__GM").GetComponent<GameManager>();
        filmText = GameObject.Find("FilmText").GetComponent<TextMesh>();
        gm.reloaded = true;
        Reloadedfilm.SetActive(false);


    }


    private void Update()
    {
        
        if (lightsOn) 
        {
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
                    if (Vector3.Distance(transform.position, closest.transform.position) < 0.8f)
                    {
                        Reloadedfilm.SetActive(true);
                        
                    }
                    else
                    {
                        Reloadedfilm.SetActive(false);
                        
                    }
                }
            }
        }
        
  
  #region pccontroller
      //  if (Input.GetMouseButtonDown(0))
      //  {
      //      lightsOn = true;
      //      flash.SetActive(true);
      //      if (gm.film > 0)
      //      {
      //          cameraRange.SetActive(true);
      //          
      //      }
//
      //  }
       // else if(Input.GetKeyUp(KeyCode.R) && gm.reloadReady)
       // {
       //     Debug.Log("reloaded check, reloaded is "+ gm.reloaded);
       //     gm.reloaded = true;
       //     gm.GetFilm();
//
       // }

       if (Input.GetKeyDown(KeyCode.Space))
       {
           Debug.Log("A pressed");
       
           if (!gm.reloadReady)
           {
               // instaniate film without glow
               //play reload sound 
               gm.reloadReady = true;
               Debug.Log("Reload Ready, insert film");
            
           }
       }
     
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

    public void ReloadCamera()
    {
        Debug.Log("A pressed");
       
        if (!gm.reloadReady)
        {
            // instaniate film without glow
            //play reload sound 
            gm.reloadReady = true;
            Debug.Log("Reload Ready, insert film");
            
        }

    }

    public void TakePictureInVR()
    {
        if (gm.reloaded)
        {
            cameraRange.SetActive(true); 
            gm.SnapPic(); 
            reloadedlamp.SetActive(true);
            cameraRange.SetActive(true); 
            gm.reloadReady = false;
            lightsOn = true;
            Debug.Log("Took a picture");
            

        }
        else if(!gm.reloaded)
        {
            // Play clicking sound only 

            reloadedlamp.SetActive(true);
            
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
