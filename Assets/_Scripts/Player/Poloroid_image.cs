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
    public GameObject flash;
    private bool lightsOn;
    public GameObject cameraRange;

    private void Start()
    {
      //  Instantiate(Picture, transform.position, Quaternion.identity);
        //screenCapture = new Texture2D(516, 516, TextureFormat.RGB24, false);

        camCam = transform.GetChild(0).GetComponent<Camera>();
        flash.SetActive(false);
        lightsOn = false;
        cameraRange.SetActive(false);
        


    }

    
    private void Update()
    {
        if (lightsOn)
        {
          //  Ray ray;
          //  ray = new Ray(transform.position, transform.forward * 50);
          //  RaycastHit hit;
            timerForFlash += Time.deltaTime;
            if(timerForFlash > 0.2f) 
            {
                flash.SetActive(false);
                lightsOn = false;
                timerForFlash = 0;
                screenCapture = toTexture2D(PoleroidImage);
                Picture.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial = new Material(shaderMat);
                Picture.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial.SetTexture("_MainTex", screenCapture);
                // StartCoroutine(CapturePhoto());
                //  mat = new Material(Shader.Find("Universal_Render_Pipeline/2D/Sprite-Lit-Default"));
                Instantiate(Picture, transform.position, Quaternion.Euler(90, 180, 0));
                timerForPolaroid = +Time.deltaTime;
                if(cameraRange.GetComponent<MeshCollider>())
                cameraRange.SetActive(false);
                if (timerForPolaroid > 5)
                {
                    Picture.GetComponent<BoxCollider>().enabled = false;
                    timerForPolaroid = 0;
                }
            }
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            lightsOn = true;
            flash.SetActive(true);
            cameraRange.SetActive(true);

        }
        
        
    }

    public void TakePictureInVR()
    {
        lightsOn = true;
        flash.SetActive(true);
        cameraRange.SetActive(true);
    }

    //take the render texture and turn it into it's own 2d texture
    Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(x, y, TextureFormat.RGB24, false);
        // ReadPixels looks at the active RenderTexture.
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
      
    
            //mat.SetTexture("texture", screenCapture);
            //Picture.transform.GetChild(0).GetComponent<Material>().
            //  Picture.transform.GetChild(0).GetComponent<Renderer>().material = mat;
           
      
   
        //Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
        //photoDisplayArea.sprite = photoSprite;
    }
}
