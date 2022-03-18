using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //OneShots 
    [HideInInspector] public FMOD.Studio.EventInstance TakePicture, EmptyClick;
    void Start()
    {
         TakePicture =FMODUnity.RuntimeManager.CreateInstance("event:/Effects/TakingPicture");
         EmptyClick = FMODUnity.RuntimeManager.CreateInstance("event:/Effects/EmptyClick");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
