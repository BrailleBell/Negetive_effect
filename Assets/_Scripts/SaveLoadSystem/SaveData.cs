using UnityEngine;
using System;

namespace SaveLoadSystem
{
    [System.Serializable]
    public class SaveData
    {
        //TEST
        public int index = 1;
        [SerializeField] private float myFloat = 5.8f;
    }
}

