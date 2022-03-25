using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfMonster : MonoBehaviour
{
    public Poloroid_image pol;

    private Material mymat;
    // Start is called before the first frame update
    void Start()
    {
        pol = GetComponentInParent<Poloroid_image>();
        mymat = pol.reloadedlamp.GetComponent<Renderer>().material;

    }

    private void update ()
    {
        if (pol.isActiveAndEnabled)
        {
            pol = GetComponentInParent<Poloroid_image>();
            mymat = pol.reloadedlamp.GetComponent<Renderer>().material;
        }

    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Monster"))
        {

            Debug.Log("ser monster shø");
            mymat.SetColor("_EmissionColor", Color.red);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Debug.Log("Ser ikke monster");
            mymat.SetColor("_EmissionColor", Color.white);
        }
        
    }
}
