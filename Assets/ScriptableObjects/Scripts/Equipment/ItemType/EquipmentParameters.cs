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
        public string CommonSkillDescription;
        public string RareSkillDescription;
        public string EpicSkillDescription;
        public string LegendarySkillDescription;
        public EquipmentRarities.EquipmentRarity EquipmentRarity;
        public EquipmentTypes.EquipmentType EquipmentType;
        public Skill Skill;

        private readonly int CommonDefaultValue = 10;
        private readonly int RareDefaultValue = 20;
        private readonly int EpicDefaultValue = 30;
        private readonly int LegendaryDefaultValue = 40;

        public int Value
        {
            get
            {
                return EquipmentRarity switch
                {
                    EquipmentRarities.EquipmentRarity.Common => CommonDefaultValue,
                    EquipmentRarities.EquipmentRarity.Rare => RareDefaultValue,
                    EquipmentRarities.EquipmentRarity.Epic => EpicDefaultValue,
                    EquipmentRarities.EquipmentRarity.Legendary => LegendaryDefaultValue,
                    _ => 0,
                };
            }
        }
    }
}
