using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple_player_cont : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal"), 0);
        Debug.Log("hello");
        transform.Translate(0, 0, Input.GetAxis("Vertical")* 0.5f);
    }
}
