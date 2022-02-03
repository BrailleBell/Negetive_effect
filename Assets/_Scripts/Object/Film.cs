using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Film: MonoBehaviour
{
    public GameManager gm;
    // Start is called before the first frame update
    
    private void OnTriggerEnter(Collider other)
    {
        
        gm.GetFilm();
        gameObject.SetActive(false);
    }
  
}
