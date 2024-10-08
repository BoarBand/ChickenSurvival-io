using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SurvivalChicken.Interfaces;

namespace SurvivalChicken.ScriptableObjects.EquipmentsParameters
{
    [CreateAssetMenu(fileName = "New Container", menuName = "ScriptableObjects/Equipment/Container")]
    public class EquipmentContainer : ScriptableObject, ISaveLoadPersistentData
    {
        [SerializeField] private List<EquipmentParameters> _equipmentItems = new List<EquipmentParameters>();

        [field: SerializeField] public string FileName { get; set; }

        public IEnumerable GetList()
        {
            return _equipmentItems;
        }

        public void AddItem(EquipmentParameters item)
        {
            _equipmentItems.Add(item);
        }

        public void RemoveItem(EquipmentParameters item)
        {
            _equipmentItems.Remove(item);
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