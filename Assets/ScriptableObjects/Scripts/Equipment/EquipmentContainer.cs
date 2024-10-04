using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivalChicken.ScriptableObjects.EquipmentsParameters
{
    [CreateAssetMenu(fileName = "New Container", menuName = "ScriptableObjects/Equipment/Container")]
    public class EquipmentContainer : ScriptableObject
    {
        [SerializeField] private List<EquipmentParameters> _equipmentItems = new List<EquipmentParameters>();

        public IEnumerable GetList()
        {
            return _equipmentItems;
        }

        public void AddItem(EquipmentParameters item)
        {
            _equipmentItems.Add(item);
        }

        public void RemoveItem(EquipmentParameters item)
        {
            _equipmentItems.Remove(item);
        }
    }
}