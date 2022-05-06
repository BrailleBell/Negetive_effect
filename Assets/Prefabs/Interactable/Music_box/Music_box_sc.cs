using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_box_sc : MonoBehaviour
{
    private Animator ani;

    private void Start()
    {
        ani = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        ani.Play("Play");
        ani.SetBool("Touched", true);
    
    }

    public void end()
    {
        ani.SetBool("Touched", false);
    }

}
//I want a song to play when the key is turned around and let go. when this happens the disk will also spin around. the keys rotation will also be clamped
//spinning and playing untill the rotation returns to 0. once let go the key will stop winding.
//the top part of the music box will be movable, but it's rotation clamped at a certain angle.