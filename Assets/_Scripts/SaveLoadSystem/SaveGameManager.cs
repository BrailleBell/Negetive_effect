using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Events;
/// <summary>
/// Gonna make the outline here first and then implement it
/// to the GameManager since its easier to see things in an own script
/// -Charlie
/// </summary>
/// 
namespace SaveLoadSystem
{
   public static class SaveGameManager
   {
        public static SaveData CurrentSaveData = new SaveData();
        public const string SaveDirectory = "/SaveData/";
        public const string FileName = "SaveGame.txt";

        public static UnityAction OnLoadGameStart;
        public static UnityAction OnLoadGameFinish;

        public static bool SaveGame()
        {
            var dir = Application.dataPath + Path.AltDirectorySeparatorChar + SaveDirectory;

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string json = JsonUtility.ToJson(CurrentSaveData, true);
            File.WriteAllText(dir + FileName, json);

            GUIUtility.systemCopyBuffer = dir;

            return true;
        }

        public static void LoadGame()
        {
            OnLoadGameStart?.Invoke();

            string fullPath = Application.persistentDataPath + SaveDirectory + FileName;
            SaveData tempData = new SaveData();

            if (File.Exists(fullPath))
            {
                string json = File.ReadAllText(fullPath);
                tempData = JsonUtility.FromJson<SaveData>(json);
            }
            else
            {
                Debug.LogError("Save file does not exist!");
            }

            CurrentSaveData = tempData;
            OnLoadGameFinish?.Invoke();
        }
   }
}

