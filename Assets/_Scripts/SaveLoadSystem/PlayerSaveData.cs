using SaveLoadSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSaveData : MonoBehaviour
{
    private OurPlayerData OurData = new OurPlayerData();

    private float currentTime = GameManager.getTimer; //Hopefully it works like this
    private GameObject currentFilm = GameManager.polaroidImage;
    private GameObject[] notesSaved = GameManager.Notes;
    

    private void Awake()
    {
        ///Ads the event from GM where we call this function to happen every hour
        ///we then call the AutoSave function from here
        GameManager.OnHourChanged.AddListener(AutoSave); //this gets nullreferenced
    }

    private void Start()
    {
        currentFilm = GameObject.FindGameObjectWithTag("Film");
        Debug.Log("Film tag has been located");
        notesSaved = GameObject.FindGameObjectsWithTag("Save");
        Debug.Log("The tag save has been loacted");
    }

    // Update is called once per frame
    void Update()
    {
        OurData.CurrentTime = currentTime;
        OurData.CurrentFilm = currentFilm;
        OurData.NotesSaved = notesSaved;

        #region SAVE TESTING
        if (Input.GetKeyDown(KeyCode.R))
        {
            SaveGameManager.CurrentSaveData.OurPlayerData = OurData;
            SaveGameManager.SaveGame();
            Debug.Log("SaveGame has saved");
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            SaveGameManager.LoadGame();
            OurData = SaveGameManager.CurrentSaveData.OurPlayerData;
            currentTime = OurData.CurrentTime;
            Debug.Log("LoadGame has loaded the savefile");
        }
        #endregion
    }

    /// <summary>
    /// Saves the player data and overwrites the SaveGame file
    /// this AutoSave is run every ingame hour or when game is quit
    /// </summary>
    public void AutoSave()
    {
        SaveGameManager.CurrentSaveData.OurPlayerData = OurData;
        SaveGameManager.SaveGame();
        Debug.Log("SaveGame has saved");
    }

    public void LoadSaveFile() //this is used on the load button in the main menu scene
    {
        SaveGameManager.LoadGame(); //calls the load game function from the SaveGameManager.cs
    }

    /// <summary>
    /// When the player exits the game it will autosave the game
    /// </summary>
    void OnApplicationQuit() //Needs to be tested
    {
        AutoSave();
        Debug.Log("Application ending after " + Time.time + " seconds");
    }
}

/// <summary>
/// This is where you put the stuff needed to be saved on the player
/// (just make sure that if stuff needs to be linked from somewhere else
/// to make sure it actually does work)
/// </summary>
[System.Serializable]
public struct OurPlayerData
{
    public float CurrentTime;
    public GameObject CurrentFilm;
    public GameObject[] NotesSaved;
}
