using System.Collections.Generic;
using UnityEngine;
using SurvivalChicken.ScriptableObjects.EquipmentsParameters;

namespace SurvivalChicken.Controllers
{
    public sealed class InventoryView : MonoBehaviour
    {
        [SerializeField] private EquipmentItemView _equipmentItem;
        [SerializeField] private Transform _transformContainer;
        [SerializeField] private EquipmentContainer _equipmentContainer;
        [SerializeField] private EquipmentItemInfo _equipmentItemInfo;

        [Header("Inventory Cells")]
        [SerializeField] private InventoryCellView _helmetCell;
        [SerializeField] private InventoryCellView _armorCell;
        [SerializeField] private InventoryCellView _bootsCell;
        [SerializeField] private InventoryCellView _attributeCell;
        [SerializeField] private InventoryCellView _weaponModuleCell;
        [SerializeField] private InventoryCellView _petCell;

        private List<EquipmentItemView> _equipmentItemViewsContainer = new List<EquipmentItemView>();

        public void Initalize()
        {
            CreateItemView();
            UpdateEquipmentItemsView();
        }

        private void CreateItemView()
        {
            foreach (EquipmentParameters item in _equipmentContainer.EquipmentItems)
            {
                EquipmentItemView itemView = Instantiate(_equipmentItem);
                itemView.Initialize(item, (item) => _equipmentItemInfo.Initialize(item));
                itemView.transform.SetParent(_transformContainer, false);
            }
        }

        private void UpdateEquipmentItemsView()
        {
            foreach (EquipmentItemView item in _equipmentItemViewsContainer)
                item.UpdateView();
        }
    }
}
