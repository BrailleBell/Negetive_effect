using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtSprite : MonoBehaviour
{
    public Transform Target;

    private float Dist;

    private SpriteRenderer rend;

    private void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        Dist = Vector3.Distance(Target.position, transform.position);
        if (Dist >= 20)
        {
            rend.enabled = false;
        }
        else
        {
            Dist = Dist * 0.05f;
            rend.enabled = true;
            transform.LookAt(Target, Vector3.up);
            transform.localScale = new Vector3(Dist, Dist, Dist);
        }
    }
}
