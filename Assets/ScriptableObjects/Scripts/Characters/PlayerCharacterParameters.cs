using UnityEngine;
using SurvivalChicken.Interfaces;
using SurvivalChicken.ScriptableObjects.EquipmentsParameters;
using System.IO;

namespace SurvivalChicken.ScriptableObjects.CharactersParameters.Player
{
    [CreateAssetMenu(fileName = "New Player", menuName = "ScriptableObjects/Characters/Player")]
    public class PlayerCharacterParameters : CharacterParameters, IPlayerEquipment, ISaveLoadPersistentData
    {
        public int CritDamageChance;
        public int CritDamageValue;

        public AbilitiesParameters.AbilityParameters InitialAbility;

        [field: SerializeField] public HelmetEquipmentParameters HelmetEquipment { get; set; }
        [field: SerializeField] public ArmorEquipmentParameters ArmorEquipment { get; set; }
        [field: SerializeField] public BootsEquipmentParameters BootsEquipment { get; set; }
        [field: SerializeField] public AttributeEquipmentParameters AttributeEquipment { get; set; }
        [field: SerializeField] public WeaponModuleEquipmentParameters WeaponModuleEquipment { get; set; }
        [field: SerializeField] public PetEquipmentParameters PetEquipment { get; set; }

        [field: SerializeField] public string FileName { get; set; }

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

            using(FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
            using(StreamReader streamReader = new StreamReader(fileStream))
            {
                JsonUtility.FromJsonOverwrite(streamReader.ReadLine(), this);
            }
        }
    }
}
