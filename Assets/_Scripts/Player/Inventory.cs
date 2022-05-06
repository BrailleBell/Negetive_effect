using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //public static TextureSaving ts;

    public GameManager gm;
    public GameObject film;
    public GameObject[] notes;

    //Inventory popout VR 
    public GameObject inventory;
    public GameObject anchor;
    bool UIactive;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("__GM").GetComponent<GameManager>();

        inventory.SetActive(false);
        UIactive = false;
    }

    private void Update()
    {
        #region POP out inventory (not for use in game)
        if (Input.GetKeyDown("secondaryButton")) //this will not work but its just to test something
        {
            UIactive = !UIactive;
            inventory.SetActive(UIactive);
        }

        if (UIactive)
        {
            inventory.transform.position = anchor.transform.position;
            inventory.transform.eulerAngles = new Vector3(anchor.transform.eulerAngles.x + 15, anchor.transform.eulerAngles.y, 0);
        }
        #endregion
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RightHand"))
        {
            InstantiateFilm();
        }
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
