using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameManager gm;
    public GameObject film;
    public GameObject[] notes;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("__GM").GetComponent<GameManager>();
    }

    public void InstantiateFilm()
    {
        Debug.Log("grabbed a film, do you have more than 0?");

        if (gm.film >= 1)
        {
            gm.film--;
            Instantiate(film,gameObject.transform.position,quaternion.identity);
            Debug.Log("Film has been instatiated");
        }

    }

    public void InstantiateNotes()
    {
        Debug.Log("Notes has been instatiated");
    }
}
