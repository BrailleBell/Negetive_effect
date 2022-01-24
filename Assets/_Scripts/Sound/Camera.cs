using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Camera : MonoBehaviour
{
    //Flash stuff
    public float flashTimeLength = 0.2f;
    private float startTime;

    private Image flashImage;

    public bool doCameraFlash = false;
    private bool flashing = false;

    //Sound stuff
    public AudioSource audioSource;
    public AudioClip audioClip;

    private void Start()
    {
        flashImage = GetComponent<Image>();
        Color col = flashImage.color;
        col.a = 0.0f;
        flashImage.color = col;
    }

    private void Update()
    {
        if(doCameraFlash && !flashing)
        {
            CameraFlash();
        }
        else
        {
            doCameraFlash = false;
        }
    }

    public void Snapshot() //sound of taking a photo
    {
        audioSource.PlayOneShot(audioClip);
        Debug.Log("Photo taken");
    }

    public void CameraFlash()
    {
        //initial color
        Color col = flashImage.color;

        //start time to fade over time
        startTime = Time.time;

        //taking photo again
        doCameraFlash = false;

        //starts as alpha = 1.0(opaque)
        col.a = 1.0f;

        //the image start color
        flashImage.color = col;

        //makes sure that the player cant flash twice in a row
        flashing = true;

        StartCoroutine(FlashCoroutine());
    }

    IEnumerator FlashCoroutine()
    {
        bool done = false;

        while (!done)
        {
            float perc;
            Color col = flashImage.color;

            perc = Time.time - startTime;
            perc = perc / flashTimeLength;

            if(perc > 1.0f)
            {
                perc = 1.0f;
                done = true;
            }

            col.a = Mathf.Lerp(1.0f, 0.0f, perc);
            flashImage.color = col;
            flashing = true;

            yield return null;
        }

        flashing = false;

        yield break;
    }
}
