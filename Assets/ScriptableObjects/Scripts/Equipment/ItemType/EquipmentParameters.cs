using UnityEngine;
using SurvivalChicken.Structures;
using SurvivalChicken.Skills;

namespace SurvivalChicken.ScriptableObjects.EquipmentsParameters
{
    public abstract class EquipmentParameters : ScriptableObject
    {
        public Sprite Icon;
        public string Label;
        public string Description;
        public EquipmentRarities.EquipmentRarity EquipmentRarity;
        public EquipmentTypes.EquipmentType EquipmentType;
        public Skill Skill;
    }
}
