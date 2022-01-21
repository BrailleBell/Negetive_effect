using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;

    public void Snapshot() //sound of taking a photo
    {
        audioSource.PlayOneShot(audioClip);
    }
}
