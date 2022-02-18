using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkPile : MonoBehaviour
{

    
    // Trundle
    
    
    
    // ghost.baseOffset = -5f;
    // GetComponentInChildren<BoxCollider>().enabled = false;
    // if (currentPos.y - (riseSpeed * Time.deltaTime) < groundlevel - offset)
    // {
    //     transform.position = new Vector3(currentPos.x, groundlevel - offset, currentPos.z);
    // }
    // else
    // {
    //     transform.Translate(Vector3.down * riseSpeed * Time.deltaTime);
    // }


    //  shouldLerp = true; 

    // if (shouldLerp)
    // {
    //     if (!lerpHasStarted)
    //     {
    //         lerpHasStarted = true;
    //         startTime = Time.time;
    //     }
    //     
    //     transform.Translate();
    //     transform.position = LerpHelper(transform.position, underGroundPos, startTime, _interval);
    //     if (transform.position == underGroundPos)
    //     {
    //         transform.position = underGroundPos;
    //         shouldLerp = false;
    //         lerpHasStarted = false;
    //         
    //     }

    //   rb.useGravity = false;
    //   Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
    //   randomDirection += transform.position;
    //   NavMeshHit hit;
    //   NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
    //   Vector3 finalPosition = hit.position;
    //   aboveTimer = 0;
    // }
    
    
    
    //   anim.SetTrigger("Walk");
    //  float randomDownTime = Random.Range(10, 50);
    //  if (aboveTimer > randomDownTime)
    //  {
    //      aboveGround = false;
    //  }

    
    //  if (currentPos.y + (riseSpeed * Time.deltaTime) > groundlevel + offset)
    //  {
    //      transform.position = new Vector3(currentPos.x, groundlevel + offset, currentPos.z);
    //      GetComponentInChildren<BoxCollider>().enabled = true;
    //      ghost.radius = baseoffset;
    //  }
    //  else
    //  {
    //      transform.Translate(Vector3.up * riseSpeed * Time.deltaTime);
    //      GetComponentInChildren<BoxCollider>().enabled = true;
    //      ghost.radius = baseoffset;
    //  }

            
    // shouldLerp = true;
    // if (shouldLerp) 
    // {
    //     if (!lerpHasStarted)
    //     {
    //         lerpHasStarted = true;
    //         startTime = Time.time;
    //     }
    //     transform.position = LerpHelper(transform.position, aboveGroundPos, startTime, _interval);
    //     if (transform.position == aboveGroundPos)
    //     {
    //         
    //         
    //         shouldLerp = false;
    //         lerpHasStarted = false;
    //     }
    //
    private void FixedUpdate()
    {
        //  if (Physics.Raycast(transform.position + new Vector3(0, offset * 2, 0), Vector3.down, out RaycastHit hit,
        //          offset * 3, 1 << LayerMask.NameToLayer("Ground")))
        //  {
        //      groundlevel = hit.point.y;
        //  }
    }
    
    //Debug.Log("startime is " + startTime);
    //Debug.Log(" Ghost baseofset " + ghost.baseOffset);
    
    //  public float _interval, offset, riseSpeed;
    
    // gameObject.SetActive(false);
    // Debug.Log(Vector3.Distance(gameObject.transform.position, Player.transform.position) +
    // " Hit Ditscance");
    
    //&& currentPos.y > groundlevel - offset)
}
