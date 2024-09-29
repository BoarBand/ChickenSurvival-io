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
    }
}
