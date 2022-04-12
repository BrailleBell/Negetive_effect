using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrundleHide : MonoBehaviour
{
    public float DistanceToPlayer;
    public GameObject Player;
    public bool seen;
    private Animator anim;
    private float timer;
    public float DistanceForHiding, DistanceForAppearing;
    public SkinnedMeshRenderer trundle;
    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        anim = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(Player.transform.position);
        DistanceToPlayer = Vector3.Distance(gameObject.transform.position, Player.transform.position);


        if (DistanceToPlayer < DistanceForAppearing)
        {
            anim.SetBool("Down",false);
            trundle.enabled = true;
            anim.SetBool("Up",true);
            
        }
        else
        {
            anim.SetBool("Down",true);
            trundle.enabled = false;
        }
        
        if (trundle.isVisible)
        {
            seen = true;
        }
        else
        {
            seen = false;
        }

        if (seen)
        {
            if (DistanceToPlayer < DistanceForHiding)
            {
                timer += Time.deltaTime;
                anim.SetBool("Up",false);
                anim.SetBool("Down",true);
                FMODUnity.RuntimeManager.PlayOneShot("event:/Monsters/Trundle/Down",GetComponent<Transform>().position);
                if (timer > 1)
                {
                    gameObject.SetActive(false);
                }
                
                
            }
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,DistanceForHiding);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position,DistanceForAppearing);
    }
}
