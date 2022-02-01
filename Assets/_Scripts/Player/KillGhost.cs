using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillGhost : MonoBehaviour
{
    public int SceneToGoToWhenWon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.DrawRay(gameObject.transform.position,transform.forward * 100,Color.green,0.3f);
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray;
            ray = new Ray(transform.position, transform.forward * 50);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.distance > 49f)
                {
                    if (hit.collider.transform.gameObject.CompareTag("Monster"))
                    {
                        Destroy(hit.transform.gameObject);
                        Debug.Log("monster hit");
                        
                    }
                    
                
                }
                else
                {
                    Debug.Log("Nothing has been hit");
                }
            
            }
        }
    }
    
    public void PhotoTaken() //checks if it has hit an enemy with the flash
    {
       Debug.Log("yes");
      
    }
    
}
