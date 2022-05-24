using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSounds : MonoBehaviour
{
    private int sounds;
    public float timer;
    private bool reset;
    public float counter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!reset)
        {
            counter += Time.deltaTime;
            timer = Random.Range(120,240);
            if (counter > timer)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Effects/SuspenseForRandomEncounter",GetComponent<Transform>().position);
                counter = 0;
                reset = true;
            }
        }
        else
        {
            reset = false;
        }
    }
}
