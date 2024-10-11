using UnityEngine;
using SurvivalChicken.SaveLoadDatas;
using SurvivalChicken.Interfaces;
using SurvivalChicken.Structures;

namespace SurvivalChicken.CollectItems
{
    public abstract class CollectItem : MonoBehaviour, ICollect
    {
        protected SaveLoadData SaveLoadData;

        [field:SerializeField] public Sprite Icon { get; set; }
        [field: SerializeField] public int Amount { get; set; }
        [field: SerializeField] public EquipmentRarities.EquipmentRarity EquipmentRarity { get; set; }

        public virtual void Initialize(SaveLoadData saveLoadData, int amount)
        {
            SaveLoadData = saveLoadData;
            Amount = amount;
        }
    }
}
