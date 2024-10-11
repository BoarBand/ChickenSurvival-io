using UnityEngine;
using SurvivalChicken.Structures;

namespace SurvivalChicken.Interfaces
{
    public interface ICollect
    {
        public Sprite Icon { get; set; }

        public int Amount { get; set; }

        public EquipmentRarities.EquipmentRarity EquipmentRarity { get; set; }
    }
}
