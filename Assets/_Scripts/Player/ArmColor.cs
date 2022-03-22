using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmColor : MonoBehaviour
{
    
    public Material greenLight, redlight;

    public float meetersToMonster;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Monster");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
                float lerptimer = Mathf.PingPong(Time.time, 1) / 100f;

                if(Vector3.Distance(transform.position,closest.transform.position) < meetersToMonster)
                {
                    float lerp = Mathf.PingPong(Time.time, 1) / 100f;
                    GetComponent<Renderer>().material.Lerp(GetComponent<Renderer>().material, redlight, lerp);

                }
                else
                {
                    float lerp = Mathf.PingPong(Time.time, 1) / 100f;
                    GetComponent<Renderer>().material.Lerp(GetComponent<Renderer>().material, greenLight, lerp);
                    
                }
            }
            
            
                
            
        }
    }
}
