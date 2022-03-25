using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveData : MonoBehaviour
{
    private OurPlayerData OurData = new OurPlayerData();

    private int currentTime = GameManager.Hour;
    private int currentFilm;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        OurData.CurrentTime = currentTime;
        //OurData.CurrentFilm = currentFilm;

        if (Input.GetButtonDown(XRButton.Grip));
        {

        }
    }
}

[System.Serializable]
public struct OurPlayerData
{
    public float CurrentTime;
    public int CurrentLevel;
    public int CurrentFilm;
}
