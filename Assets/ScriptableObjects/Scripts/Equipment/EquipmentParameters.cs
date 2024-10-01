using UnityEngine;
using SurvivalChicken.Interfaces;

namespace SurvivalChicken.ScriptableObjects.EquipmentsParameters
{
    public abstract class EquipmentParameters : ScriptableObject
    {
        public Sprite Icon;
        public string Label;
        public string Description;
        public IEquipmentRarity EquipmentRarity;
    }
}
