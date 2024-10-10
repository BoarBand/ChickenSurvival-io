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
            else
                GetRandomEquipment();

            if (chance <= ChanceToGetEpicItem && EpicItems.Count > 0)
                return EpicItems[Random.Range(0, EpicItems.Count)];
            else
                GetRandomEquipment();

            if (chance <= ChanceToGetRareItem && RareItems.Count > 0)
                return RareItems[Random.Range(0, RareItems.Count)];
            else
                GetRandomEquipment();

            if (chance <= ChanceToGetCommonItem && CommonItems.Count > 0)
                return CommonItems[Random.Range(0, CommonItems.Count)];
            else
                GetRandomEquipment();

            return null;
        }
    }
}
