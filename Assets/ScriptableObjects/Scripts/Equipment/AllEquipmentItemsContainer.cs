using System.Collections.Generic;
using UnityEngine;

namespace SurvivalChicken.ScriptableObjects.EquipmentsParameters
{
    [CreateAssetMenu(fileName = "New Container", menuName = "ScriptableObjects/Equipment/All Items Container")]
    public class AllEquipmentItemsContainer : ScriptableObject
    {
        public int ChanceToGetCommonItem;
        public int ChanceToGetRareItem;
        public int ChanceToGetEpicItem;
        public int ChanceToGetLegendaryItem;

        public List<EquipmentParameters> CommonItems = new List<EquipmentParameters>();
        public List<EquipmentParameters> RareItems = new List<EquipmentParameters>();
        public List<EquipmentParameters> EpicItems = new List<EquipmentParameters>();
        public List<EquipmentParameters> LegendaryItems = new List<EquipmentParameters>();

        public EquipmentParameters GetRandomEquipment()
        {
            int chance = Random.Range(0, 100);

            if (chance <= ChanceToGetLegendaryItem && LegendaryItems.Count > 0)
                return LegendaryItems[Random.Range(0, LegendaryItems.Count)];

            if (chance <= ChanceToGetEpicItem && EpicItems.Count > 0)
                return EpicItems[Random.Range(0, EpicItems.Count)];

            if (chance <= ChanceToGetRareItem && RareItems.Count > 0)
                return RareItems[Random.Range(0, RareItems.Count)];

            if (chance <= ChanceToGetCommonItem && CommonItems.Count > 0)
                return CommonItems[Random.Range(0, CommonItems.Count)];

            return null;
        }
    }
}
