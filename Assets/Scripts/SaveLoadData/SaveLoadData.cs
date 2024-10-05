using System.IO;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;

namespace SurvivalChicken.SaveLoadDatas
{
    public sealed class SaveLoadData : MonoBehaviour
    {
        //private readonly string GeneralPath = "Assets/SaveData/";

        private readonly string DataFile = "/GameData.dat";

        public int Coins = 0;
        public int Gems = 0;
        public int Energy = 100;

        public int[] WorldTimes = new int[1];

        public void Initialize()
        {
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
        }

        public void ResetData()
        {
            string path = Application.persistentDataPath + DataFile;

            File.Delete(path);

            Coins = 0;
            Gems = 0;
            Energy = 100;
            WorldTimes[0] = 0;
    }

        /*

        #region WorldTimes

        public void SaveWorldTime(int index, int value)
        {
            string path = GeneralPath + "WorldTimes.txt";

            List<string> lines = new List<string>();

            using (FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
            using (StreamReader streamReader = new StreamReader(file))
            {
                while (streamReader.Peek() > -1)
                    lines.Add(streamReader.ReadLine());

                if (lines.Count <= index)
                    lines.Add("");
            }

            using (StreamWriter streamWriter = new StreamWriter(path, false))
            {
                streamWriter.Write("");
            }

            using (StreamWriter streamWriter = new StreamWriter(path, true))
            {
                if (lines.Count <= 0)
                {
                    streamWriter.WriteLine(value);
                    return;
                }

                for (int i = 0; i < lines.Count; i++)
                {
                    if (i == index)
                    {
                        lines[i] = value.ToString();
                    }
                    streamWriter.WriteLine(lines[i]);
                }
            }
        }

        public bool TryGetWorldTime(int index, out int value)
        {
            string path = GeneralPath + "WorldTimes.txt";

            List<string> lines = new List<string>();

            using (FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
            using (StreamReader streamReader = new StreamReader(file))
            {
                while (streamReader.Peek() > -1)
                    lines.Add(streamReader.ReadLine());
            }

            if (lines.Count <= index)
            {
                value = -1;
                return false;
            }

            value = System.Convert.ToInt32(lines[index]);
            return true;
        }

        #endregion

        #region Currency Values

        public void SaveCurrencyValue(int value, CurrencyTypes.CurrencyType currencyType)
        {
            string path = GeneralPath + "CurrencyValues.txt";

            string coinsLabel = "Coins";
            string gemsLabel = "Gems";
            string energyLabel = "Energy";

            List<string> lines = new List<string>();
            Dictionary<string, int> currencies = new Dictionary<string, int>();

            using(FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
            using(StreamReader streamReader = new StreamReader(file))
            {
                while(streamReader.Peek() > -1)
                    lines.Add(streamReader.ReadLine());

                foreach(string line in lines)
                {
                    string[] splitStr = line.Split(' ');
                    currencies.Add(splitStr[0], System.Convert.ToInt32(splitStr[1]));
                }
            }

            switch (currencyType)
            {
                case CurrencyTypes.CurrencyType.Coins:
                    if (currencies.ContainsKey(coinsLabel))
                        currencies[coinsLabel] = value;
                    break;
                case CurrencyTypes.CurrencyType.Gems:
                    if (currencies.ContainsKey(gemsLabel))
                        currencies[gemsLabel] = value;
                    break;
                case CurrencyTypes.CurrencyType.Energy:
                    if (currencies.ContainsKey(energyLabel))
                        currencies[energyLabel] = value;
                    break;
            }

            using(StreamWriter streamWriter = new StreamWriter(path, false))
            {
                streamWriter.Write("");
            }

            using(StreamWriter streamWriter = new StreamWriter(path, true))
            {
                foreach (string str in currencies.Keys)
                    streamWriter.WriteLine($"{str} {currencies[str]}");
            }
        }

        public bool TryGetCurrencyValue(out int value, CurrencyTypes.CurrencyType currencyType)
        {
            string path = GeneralPath + "CurrencyValues.txt";

            string coinsLabel = "Coins";
            string gemsLabel = "Gems";
            string energyLabel = "Energy";

            List<string> lines = new List<string>();
            Dictionary<string, int> currencies = new Dictionary<string, int>();

            using (FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
            using (StreamReader streamReader = new StreamReader(file))
            {
                while (streamReader.Peek() > -1)
                    lines.Add(streamReader.ReadLine());

                foreach (string line in lines)
                {
                    string[] splitStr = line.Split(' ');
                    currencies.Add(splitStr[0], System.Convert.ToInt32(splitStr[1]));
                }
            }

            switch (currencyType)
            {
                case CurrencyTypes.CurrencyType.Coins:
                    if (currencies.ContainsKey(coinsLabel))
                    {
                        value = currencies[coinsLabel];
                        return true;
                    }
                    break;
                case CurrencyTypes.CurrencyType.Gems:
                    if (currencies.ContainsKey(gemsLabel))
                    {
                        value = currencies[gemsLabel];
                        return true;
                    }
                    break;
                case CurrencyTypes.CurrencyType.Energy:
                    if (currencies.ContainsKey(energyLabel))
                    {
                        value = currencies[energyLabel];
                        return true;
                    }
                    break;
            }

            value = -1;
            return false;
        }

        #endregion

        */
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
