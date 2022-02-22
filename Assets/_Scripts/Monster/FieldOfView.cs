using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public bool canSeePlayer;

    public float lookRadius;

    public float angle;
    public LayerMask obstructionMask,targertMask;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FovRoutine());
    }
    
    private IEnumerator FovRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);
        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }


    private void FieldOfViewCheck()
    {
        Collider[] rangeChecs = Physics.OverlapSphere(transform.position, lookRadius,obstructionMask);

        if (rangeChecs.Length != 0)
        {
            Transform target = rangeChecs[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget,targertMask))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
               
            }
            else
            {
                canSeePlayer = false;
            }
            
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
