using SaveLoadSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSaveData : MonoBehaviour
{
    private OurPlayerData OurData = new OurPlayerData();

    private float currentTime = GameManager.getTimer; //Hopefully it works like this
    private int currentFilm;

    private void Awake()
    {
        GameManager.OnHourChanged.AddListener(AutoSave);
    }

    // Update is called once per frame
    void Update()
    {
        OurData.CurrentTime = currentTime;
        //OurData.CurrentFilm = currentFilm;

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
            currentTime = OurData.CurrentTime;
            Debug.Log("LoadGame has loaded the savefile");
        }
    }

    public void AutoSave()
    {
        SaveGameManager.CurrentSaveData.OurPlayerData = OurData;
        SaveGameManager.SaveGame();
        Debug.Log("SaveGame has saved");
    }

    public void LoadSaveFile()
    {
        SaveGameManager.LoadGame();
    }

    void OnApplicationQuit()
    {
        AutoSave();
        Debug.Log("Application ending after " + Time.time + " seconds");
    }
}

/// <summary>
/// This is where you put the stuff needed to be saved as the player
/// </summary>
[System.Serializable]
public struct OurPlayerData
{
    public float CurrentTime;
    //public int CurrentLevel;
    //public int CurrentFilm;
}
