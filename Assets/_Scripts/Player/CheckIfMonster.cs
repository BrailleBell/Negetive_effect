using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfMonster : MonoBehaviour
{
    public Poloroid_image pol;
    // Start is called before the first frame update
    void Start()
    {
        pol = GetComponentInParent<Poloroid_image>();

    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            pol.reloadedlamp.GetComponent<Material>().color = Color.red;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            pol.reloadedlamp.GetComponent<Material>().color = Color.white;
        }
        
    }
}
