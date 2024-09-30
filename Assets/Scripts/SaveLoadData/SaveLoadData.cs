using System.Collections.Generic;
using System.IO;

namespace SurvivalChicken.SaveLoadDatas
{
    public sealed class SaveLoadData
    {
        private readonly string GeneralPath = "Assets/SaveData/";

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

        public enum CurrencyTypes
        {
            Coins,
            Gems,
            Energy
        }

        public void SaveCurrencyValue(int value, CurrencyTypes currencyType)
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
                case CurrencyTypes.Coins:
                    if (currencies.ContainsKey(coinsLabel))
                        currencies[coinsLabel] = value;
                    break;
                case CurrencyTypes.Gems:
                    if (currencies.ContainsKey(gemsLabel))
                        currencies[gemsLabel] = value;
                    break;
                case CurrencyTypes.Energy:
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

        public bool TryGetCurrencyValue(out int value, CurrencyTypes currencyType)
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
                case CurrencyTypes.Coins:
                    if (currencies.ContainsKey(coinsLabel))
                    {
                        value = currencies[coinsLabel];
                        return true;
                    }
                    break;
                case CurrencyTypes.Gems:
                    if (currencies.ContainsKey(gemsLabel))
                    {
                        value = currencies[gemsLabel];
                        return true;
                    }
                    break;
                case CurrencyTypes.Energy:
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
    }
}
