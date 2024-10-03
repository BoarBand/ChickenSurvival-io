using UnityEngine;
using SurvivalChicken.Structures;

namespace SurvivalChicken.ScriptableObjects.EquipmentsParameters
{
    public abstract class EquipmentParameters : ScriptableObject
    {
        public Sprite Icon;
        public string Label;
        public string Description;
        public EquipmentRarities.EquipmentRarity EquipmentRarity;
        public EquipmentTypes.EquipmentType EquipmentType;
    }
}
