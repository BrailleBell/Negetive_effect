using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_box_sc : MonoBehaviour
{
    private GameObject keyMe;
    private GameObject disk;
    private GameObject top;
    private float rotFlo;

    // Start is called before the first frame update
    void Start()
    {
        keyMe = transform.GetChild(1).gameObject;
        disk = transform.GetChild(3).gameObject;
        top = transform.GetChild(4).gameObject;
    }

    // Update is called once per frame
    
    void Update()
    {
        Mathf.Clamp(top.transform.rotation.x,0, 30);



      if (transform.GetChild(4).rotation.x > 0)
        {
            transform.GetChild(1).Rotate(-1*Time.deltaTime, 0, 0);
            Mathf.Clamp(transform.GetChild(1).rotation.z, 0, 200);
        }
    }
    private void OnTriggerStay(Collider other)
    {
       
    }



}
//I want a song to play when the key is turned around and let go. when this happens the disk will also spin around. the keys rotation will also be clamped
//spinning and playing untill the rotation returns to 0. once let go the key will stop winding.
//the top part of the music box will be movable, but it's rotation clamped at a certain angle.