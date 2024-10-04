using UnityEngine;
using SurvivalChicken.Interfaces;
using SurvivalChicken.ScriptableObjects.EquipmentsParameters;

namespace SurvivalChicken.ScriptableObjects.CharactersParameters.Player
{
    [CreateAssetMenu(fileName = "New Player", menuName = "ScriptableObjects/Characters/Player")]
    public class PlayerCharacterParameters : CharacterParameters, IPlayerEquipment
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
    }
}
