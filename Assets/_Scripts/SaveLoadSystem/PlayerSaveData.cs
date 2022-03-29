using SaveLoadSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSaveData : MonoBehaviour
{
    private OurPlayerData OurData = new OurPlayerData();

    private int currentTime = GameManager.Minute; //Hopefully it works like this
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

        if (Input.GetKeyDown(KeyCode.R)) //just to create the initial save file ig...
        {
            SaveGameManager.CurrentSaveData.OurPlayerData = OurData;
            SaveGameManager.SaveGame();
            Debug.Log("SaveGame has saved");
        }

        if (Input.GetKeyDown(KeyCode.T)) //gotta make it load when you die / when you start the game from a saveload
        {
            SaveGameManager.LoadGame();
            OurData = SaveGameManager.CurrentSaveData.OurPlayerData;
            currentTime = (int)OurData.CurrentTime; //causally converting the float into an int for this to work
            Debug.Log("LoadGame has loaded the savefile");
        }

        ///Trying some dummy way of saving after each hour
        ///
        if(GameManager.Minute == 01)
        {
            SaveGameManager.CurrentSaveData.OurPlayerData = OurData;
            SaveGameManager.SaveGame();
            Debug.Log("Saved at 1am");
        }
        if (GameManager.Hour == 02)
        {
            SaveGameManager.CurrentSaveData.OurPlayerData = OurData;
            SaveGameManager.SaveGame();
            Debug.Log("Saved at 2am");
        }
        if (GameManager.Hour == 03)
        {
            SaveGameManager.CurrentSaveData.OurPlayerData = OurData;
            SaveGameManager.SaveGame();
            Debug.Log("Saved at 3am");
        }
        if (GameManager.Hour == 04)
        {
            SaveGameManager.CurrentSaveData.OurPlayerData = OurData;
            SaveGameManager.SaveGame();
            Debug.Log("Saved at 4am");
        }
        if (GameManager.Hour == 05)
        {
            SaveGameManager.CurrentSaveData.OurPlayerData = OurData;
            SaveGameManager.SaveGame();
            Debug.Log("Saved at 5am");
        }
        if (GameManager.Hour == 06)
        {
            SaveGameManager.CurrentSaveData.OurPlayerData = OurData;
            SaveGameManager.SaveGame();
            SceneManager.LoadScene(4);
            Debug.Log("Saved at 6am");

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
