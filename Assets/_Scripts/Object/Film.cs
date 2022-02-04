using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Film: MonoBehaviour
{
    
    // Start is called before the first frame update
    private void Start()
    {
        
        
    }
    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        GameManager.GetFilm();
       
    }
  
}
