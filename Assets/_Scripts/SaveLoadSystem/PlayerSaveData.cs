using SaveLoadSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveData : MonoBehaviour
{
    private OurPlayerData OurData = new OurPlayerData();

    private int currentTime = GameManager.Hour; //Hopefully it works like this
    private int currentFilm;

    // Update is called once per frame
    void Update()
    {
        OurData.CurrentTime = currentTime;
        //OurData.CurrentFilm = currentFilm;

        /*if (Input.GetButtonDown(XRButton.Grip));
        {
            SaveGameManager.CurrentSaveData.OurPlayerData = OurData;
            SaveGameManager.SaveGame();
        }*/

        if (Input.GetKeyDown(KeyCode.R))
        {
            SaveGameManager.CurrentSaveData.OurPlayerData = OurData;
            SaveGameManager.SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            SaveGameManager.LoadGame();
            OurData = SaveGameManager.CurrentSaveData.OurPlayerData;
            currentTime = (int)OurData.CurrentTime; //causally converting the float into an int for this to work
        }
    }
}

/// <summary>
/// This is where you put the stuff needed to be saved as the player
/// </summary>
[System.Serializable]
public struct OurPlayerData
{
    public float CurrentTime;
    public int CurrentLevel;
    public int CurrentFilm;
}
