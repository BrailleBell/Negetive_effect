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
    public float DistanceForHiding;
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
                anim.SetBool("Down",true);
                if (timer > 1)
                {
                    gameObject.SetActive(false);
                }
                
                
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,DistanceForHiding);
    }
}
