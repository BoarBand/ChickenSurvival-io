using System.Collections.Generic;
using UnityEngine;

namespace SurvivalChicken.ScriptableObjects.EquipmentsParameters
{
    [CreateAssetMenu(fileName = "New Container", menuName = "ScriptableObjects/Equipment/Container")]
    public class EquipmentContainer : ScriptableObject
    {
        public List<EquipmentParameters> EquipmentItems = new List<EquipmentParameters>();
    }
}