using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Poloroid_image : MonoBehaviour
{
    
    public Material sh;
    public GameObject Picture;
    private Texture2D screenCapture;
    public Image photoDisplayArea;
    public Material mat;
    private Camera camCam;
    public RenderTexture Poleroid;
    public int x;
    public int y;

    private void Start()
    {
      //  Instantiate(Picture, transform.position, Quaternion.identity);
        //screenCapture = new Texture2D(516, 516, TextureFormat.RGB24, false);

        camCam = transform.GetChild(0).GetChild(1).GetComponent<Camera>();
       
    }

    
    private void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            
           screenCapture = toTexture2D(Poleroid);
            Picture.transform.GetChild(0).GetComponent<Renderer>().material = new Material(sh);
            Picture.transform.GetChild(0).GetComponent<Renderer>().material.SetTexture("_MainTex", screenCapture);
            // StartCoroutine(CapturePhoto());
            //  mat = new Material(Shader.Find("Universal_Render_Pipeline/2D/Sprite-Lit-Default"));
            Instantiate(Picture, transform.position, Quaternion.Euler(90, 0, 0));

        }
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
