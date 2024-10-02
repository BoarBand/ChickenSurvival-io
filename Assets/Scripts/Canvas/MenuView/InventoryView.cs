using System.Collections.Generic;
using UnityEngine;

namespace SurvivalChicken.Controllers
{
    public sealed class InventoryView : MonoBehaviour
    {
        [SerializeField] private List<EquipmentItemView> _equipmentItemsContainer = new List<EquipmentItemView>();

        [Header("Inventory Cells")]
        [SerializeField] private InventoryCellView _helmetCell;
        [SerializeField] private InventoryCellView _armorCell;
        [SerializeField] private InventoryCellView _bootsCell;
        [SerializeField] private InventoryCellView _attributeCell;
        [SerializeField] private InventoryCellView _weaponModuleCell;
        [SerializeField] private InventoryCellView _petCell;

        public void Initalize()
        {

        }
    }
}
