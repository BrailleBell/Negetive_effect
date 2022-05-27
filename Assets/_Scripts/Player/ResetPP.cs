using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPP : MonoBehaviour
{
    private Vector3 orgPos;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        orgPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position != orgPos)
        {
            timer += Time.deltaTime;
            if (timer > 3)
            {
                gameObject.transform.position = orgPos;
            }
        }
    }
}
