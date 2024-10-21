using UnityEngine;
using SurvivalChicken.Interfaces;
using System.IO;

namespace SurvivalChicken.Tutorials
{
    [CreateAssetMenu(fileName = "New Tutorial", menuName = "ScriptableObjects/Tutorial")]
    public class TutorialParameters : ScriptableObject, ISaveLoadPersistentData
    {
        [SerializeField] private string _description;
        [SerializeField] private int _status;

        [field: SerializeField] public string FileName { get; set; }

        public bool IsComplete()
        {
            if (_status == 0)
                return false;

            if (_status == 1)
                return true;

            return false;
        }

        public void Complete()
        {
            _status = 1;
        }

        public void Save()
        {
            string path = Application.persistentDataPath + "/" + FileName;
            string json = JsonUtility.ToJson(this);

            using (StreamWriter streamWriter = new StreamWriter(path, false))
            {
                streamWriter.Write("");
            }

            using (StreamWriter streamWriter = new StreamWriter(path, true))
            {
                streamWriter.WriteLine(json);
            }
        }

        public void Load()
        {
            string path = Application.persistentDataPath + "/" + FileName;

            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
            using (StreamReader streamReader = new StreamReader(fileStream))
            {
                JsonUtility.FromJsonOverwrite(streamReader.ReadLine(), this);
            }
        }
    }
}
