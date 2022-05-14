using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableGlow : MonoBehaviour
{
    public Renderer objectMat;
    bool grabbed;
    public bool glowNeeded;
    void Start()
    {
        objectMat = GetComponent<Renderer>();
        //objectMat.material.shader = Shader.Find("Boolean_3682b94a8c5b4e43a9224de8fc8c6bed");
    }

    public void HoverEnter()
    {
        if(grabbed == false)
        {
        objectMat.material.SetFloat("Boolean_3682b94a8c5b4e43a9224de8fc8c6bed", 1.0f);
        }
    }

    public void HoverExit()
    {
        objectMat.material.SetFloat("Boolean_3682b94a8c5b4e43a9224de8fc8c6bed", 0.0f);
    }

    public void Holding()
    {
        grabbed = true;
        objectMat.material.SetFloat("Boolean_a94d065e38c04e3a8f218d10ba5b2a4c", 0.0f);
    }

        public void NotHolding()
    {
        if(glowNeeded == true)
        {
            objectMat.material.SetFloat("Boolean_a94d065e38c04e3a8f218d10ba5b2a4c", 1.0f);
        }

        objectMat.material.SetFloat("Boolean_3682b94a8c5b4e43a9224de8fc8c6bed", 0.0f);
        grabbed = false;
    }
}
