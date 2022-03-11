
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameManager gm;
    public GameObject film;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("__GM").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateFilm()
    {
        Debug.Log("grabbed a film, do you have more than 0?");
        if (gm.film >= 1)
        {
            gm.film--;
            Instantiate(film,gameObject.transform.position,quaternion.identity);

        }

    }
}
