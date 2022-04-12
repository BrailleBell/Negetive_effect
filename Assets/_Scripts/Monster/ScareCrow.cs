using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareCrow : MonoBehaviour
{
    private bool dying;
    private Animator anim;
    private float killTimer;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (dying) // after taking picture of the ghost it dies after killtimer 
        {
            anim.SetBool("Death", true);
            killTimer += Time.deltaTime; // kill time must be over 0.2 secounds! 
            if (killTimer > 3f)
            {
                //gameObject.SetActive(false);
                GetComponent<BoxCollider>().enabled = false;
            }
  
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CameraShoot"))
        {
            dying = true;
        }
    }

}
