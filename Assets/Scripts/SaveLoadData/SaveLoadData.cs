using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using SurvivalChicken.Interfaces;

namespace SurvivalChicken.SaveLoadDatas
{
    public sealed class SaveLoadData : MonoBehaviour
    {
        private readonly string DataFile = "/GameData.dat";

        public int Coins = 0;
        public int Gems = 0;
        public int Energy = 100;

        public int[] WorldTimes = new int[1];

        [SerializeField] private ScriptableObject[] _scriptableObjectsToSave;

        private List<ISaveLoadPersistentData> _saveLoadPersistentDatas = new List<ISaveLoadPersistentData>();

        public void Initialize()
        {
            foreach (ScriptableObject scriptableObject in _scriptableObjectsToSave)
                if (scriptableObject is ISaveLoadPersistentData)
                    _saveLoadPersistentDatas.Add((ISaveLoadPersistentData)scriptableObject);

            LoadGame();
        }

        public void SaveGame()
        {
            string path = Application.persistentDataPath + DataFile;

            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                SaveData saveData = new SaveData();
                saveData.Coins = Coins;
                saveData.Gems = Gems;
                saveData.Energy = Energy;
                saveData.WorldTimes = WorldTimes;
                bf.Serialize(file, saveData);
            }

            foreach (ISaveLoadPersistentData saveLoadPersistentData in _saveLoadPersistentDatas)
                saveLoadPersistentData.Save();
        }

        public void LoadGame()
        {
            string path = Application.persistentDataPath + DataFile;

            using (FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
            {
                BinaryFormatter bf = new BinaryFormatter();
                if (file.Length <= 0)
                    return;
                SaveData saveData = (SaveData)bf.Deserialize(file);
                Coins = saveData.Coins;
                Gems = saveData.Gems;
                Energy = saveData.Energy;
                WorldTimes = saveData.WorldTimes;
            }

            foreach (ISaveLoadPersistentData saveLoadPersistentData in _saveLoadPersistentDatas)
                saveLoadPersistentData.Load();
        }

        public void ResetData()
        {
            string path = Application.persistentDataPath + DataFile;

            if(File.Exists(path))
                File.Delete(path);

            Coins = 0;
            Gems = 0;
            Energy = 100;
            WorldTimes[0] = 0;
        }

    }

    [Serializable]
    public class SaveData
    {
        public int Coins;
        public int Gems;
        public int Energy;

        public int[] WorldTimes = new int[1];
    }
}
