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
        public int GoldKeys = 0;

        public int HelmetUpgradeLevel = 1;
        public int ArmorUpgradeLevel = 1;
        public int BootsUpgradeLevel = 1;
        public int AttributeUpgradeLevel = 1;
        public int WeaponModuleUpgradeLevel = 1;
        public int PetUpgradeLevel = 1;

        public int[] StagePlayTimes = new int[2];

        public int[,] OpenedWorldGifts = new int[2, 3];

        public int[] LockedWorlds = new int[2] { 0, 1 };

        public int DailyRewardID = 0;

        public int[] ClaimedDailyRewards = new int[7] { 0, 0, 0, 0, 0, 0, 0 };

        public int CurrentDayToGetDailyReward = 0;

        public int NextDayToGetDailyReward = 0;

        public int MusicMute = 0;

        public int SoundMute = 0;

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
                saveData.GoldKeys = GoldKeys;
                saveData.ArmorUpgradeLevel = ArmorUpgradeLevel;
                saveData.AttributeUpgradeLevel = AttributeUpgradeLevel;
                saveData.BootsUpgradeLevel = BootsUpgradeLevel;
                saveData.HelmetUpgradeLevel = HelmetUpgradeLevel;
                saveData.WeaponModuleUpgradeLevel = WeaponModuleUpgradeLevel;
                saveData.PetUpgradeLevel = PetUpgradeLevel;
                saveData.StagePlayTimes = StagePlayTimes;
                saveData.OpenedWorldGifts = OpenedWorldGifts;
                saveData.LockedWorlds = LockedWorlds;
                saveData.DailyRewardID = DailyRewardID;
                saveData.ClaimedDailyRewards = ClaimedDailyRewards;
                saveData.CurrentDayToGetDailyReward = CurrentDayToGetDailyReward;
                saveData.NextDayToGetDailyReward = NextDayToGetDailyReward;
                saveData.MusicMute = MusicMute;
                saveData.SoundMute = SoundMute;
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
                GoldKeys = saveData.GoldKeys;
                ArmorUpgradeLevel = saveData.ArmorUpgradeLevel;
                AttributeUpgradeLevel = saveData.AttributeUpgradeLevel;
                BootsUpgradeLevel = saveData.BootsUpgradeLevel;
                HelmetUpgradeLevel = saveData.HelmetUpgradeLevel;
                WeaponModuleUpgradeLevel = saveData.WeaponModuleUpgradeLevel;
                PetUpgradeLevel = saveData.PetUpgradeLevel;
                StagePlayTimes = saveData.StagePlayTimes;
                OpenedWorldGifts = saveData.OpenedWorldGifts;
                LockedWorlds = saveData.LockedWorlds;
                DailyRewardID = saveData.DailyRewardID;
                ClaimedDailyRewards = saveData.ClaimedDailyRewards;
                CurrentDayToGetDailyReward = saveData.CurrentDayToGetDailyReward;
                NextDayToGetDailyReward = saveData.NextDayToGetDailyReward;
                MusicMute = saveData.MusicMute;
                SoundMute = saveData.SoundMute;
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
            GoldKeys = 0;

            DailyRewardID = 0;

            CurrentDayToGetDailyReward = 0;

            NextDayToGetDailyReward = 0;

            MusicMute = 0;

            SoundMute = 0;

            for (int i = 0; i < StagePlayTimes.Length; i++)
                StagePlayTimes[i] = 0;

            for (int i = 0; i < ClaimedDailyRewards.Length; i++)
                ClaimedDailyRewards[i] = 0;

            for (int i = 0; i < OpenedWorldGifts.GetLength(0); i++)
                for (int j = 0; j < OpenedWorldGifts.GetLength(1); j++)
                    OpenedWorldGifts[i, j] = 0;

            for (int i = 0; i < LockedWorlds.Length; i++)
                if (i == 0)
                    LockedWorlds[i] = 0;
                else
                    LockedWorlds[i] = 1;
        }

    }

    [Serializable]
    public class SaveData
    {
        public int Coins;
        public int Gems;
        public int Energy;
        public int GoldKeys;

        public int HelmetUpgradeLevel;
        public int ArmorUpgradeLevel;
        public int BootsUpgradeLevel;
        public int AttributeUpgradeLevel;
        public int WeaponModuleUpgradeLevel;
        public int PetUpgradeLevel;

        public int[] StagePlayTimes = new int[2];

        public int[,] OpenedWorldGifts = new int[2, 3];

        public int[] LockedWorlds = new int[2];

        public int DailyRewardID;

        public int[] ClaimedDailyRewards = new int[7];

        public int CurrentDayToGetDailyReward = 0;

        public int NextDayToGetDailyReward = 0;

        public int MusicMute;

        public int SoundMute;
    }
}
