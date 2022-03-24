using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
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

        public static bool Save()
        {
            var dir = Application.persistentDataPath + SaveDirectory;

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            return true;
        }
   }
}

