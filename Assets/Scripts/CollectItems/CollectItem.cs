using System;
using UnityEngine;
using SurvivalChicken.SaveLoadDatas;
using SurvivalChicken.Interfaces;
using SurvivalChicken.Structures;

namespace SurvivalChicken.CollectItems
{
    public class CollectItem : MonoBehaviour, ICollect
    {
        private event Action _collected;

        private SaveLoadData _saveLoadData;

        [field: SerializeField] public Sprite Icon { get; set; }
        [field: SerializeField] public EquipmentRarities.EquipmentRarity EquipmentRarity { get; set; }
        public int Amount { get; set; }

        public virtual void Initialize(SaveLoadData saveLoadData, int amount, Action collected)
        {
            _saveLoadData = saveLoadData;
            Amount = amount;
            _collected = collected;

            _collected?.Invoke();

            _saveLoadData.SaveGame();
            Destroy(gameObject);
        }
    }
}
