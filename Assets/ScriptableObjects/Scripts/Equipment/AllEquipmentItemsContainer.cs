using System.Collections.Generic;
using UnityEngine;

namespace SurvivalChicken.ScriptableObjects.EquipmentsParameters
{
    [CreateAssetMenu(fileName = "New Container", menuName = "ScriptableObjects/Equipment/All Items Container")]
    public class AllEquipmentItemsContainer : ScriptableObject
    {
        public List<EquipmentParameters> CommonItems = new List<EquipmentParameters>();
        public List<EquipmentParameters> RareItems = new List<EquipmentParameters>();
        public List<EquipmentParameters> EpicItems = new List<EquipmentParameters>();
        public List<EquipmentParameters> LegendaryItems = new List<EquipmentParameters>();

        public EquipmentParameters GetRandomEquipment(int chanceToGetCommonItem, 
            int chanceToGetRareItem, 
            int chanceToGetEpicItem, 
            int chanceToGetLegendaryItem)
        {
            int chance = Random.Range(0, 100);

            if (chance <= chanceToGetLegendaryItem && LegendaryItems.Count > 0)
                return LegendaryItems[Random.Range(0, LegendaryItems.Count)];

            if (chance <= chanceToGetEpicItem && EpicItems.Count > 0)
                return EpicItems[Random.Range(0, EpicItems.Count)];

            if (chance <= chanceToGetRareItem && RareItems.Count > 0)
                return RareItems[Random.Range(0, RareItems.Count)];

            if (chance <= chanceToGetCommonItem && CommonItems.Count > 0)
                return CommonItems[Random.Range(0, CommonItems.Count)];

            GetRandomEquipment(chanceToGetCommonItem, 
                chanceToGetRareItem, 
                chanceToGetEpicItem, 
                chanceToGetLegendaryItem);

            return null;
        }
    }
}
