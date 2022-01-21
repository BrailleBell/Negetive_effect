using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_control : MonoBehaviour
{
    public Collider flash;
    public GameObject ghost;
    public GameObject flash_effect;
    private Color col;
    
    private float alpha;
    public float timer = 1;
    
    private bool shot;
    // Start is called before the first frame update
    void Start()
    {
       col =  flash_effect.GetComponent<SpriteRenderer>().color;
        flash_effect.GetComponent<SpriteRenderer>().color = new Vector4(col.r, col.g, col.b, 0);
        flash = gameObject.transform.GetChild(0).GetComponent<Collider>(); 
        flash.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetButtonDown("Space"));
        {
            shot = true;
        }
        if (shot)
        {
            timer -= Time.deltaTime;
            
            flash.enabled = true;
            flash_effect.GetComponent<SpriteRenderer>().color = new Vector4(col.r, col.g, col.b, timer);
            
            if (timer <= 0)
            {
                flash.enabled = false;
                shot = false;
                flash_effect.GetComponent<SpriteRenderer>().color = new Vector4(col.r, col.g, col.b, 0);
                timer = 1;
            }
        }

    }
    void onTriggerEnter (Collider other)
    {
        
    }
    
}
