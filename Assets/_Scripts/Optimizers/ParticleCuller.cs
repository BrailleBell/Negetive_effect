using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCuller : MonoBehaviour
{
GameObject player;
ParticleSystem particle;

public float distance;
public float cullDistance;
public bool enabled;

  //public ParticleSystem fart;
  void Start()
  {

      particle = gameObject.GetComponent<ParticleSystem>();
      player = GameObject.FindGameObjectWithTag("Player");
  }
  void Update()
  {
      distance = Vector3.Distance(player.transform.position, particle.transform.position);
      if (enabled == true)
      {
          
          //Debug.Log("FART!!");
          var emission = particle.emission;
          emission.rateOverTime = 100;

      }
      else
      {
          //Debug.Log("FART!!2");
          var emission = particle.emission;
          emission.rateOverTime = 0;
 
      }
      OnBecameVisible();
      OnBecameInvisible();
  }

    void OnBecameVisible()
    {
        enabled = true;
    }

    
    void OnBecameInvisible()
    {
        enabled = false;
    }


}
