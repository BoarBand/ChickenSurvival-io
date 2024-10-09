using UnityEngine;
using SurvivalChicken.ScriptableObjects.EquipmentsParameters;
using UnityEngine.UI;

namespace SurvivalChicken.Controllers
{
    public sealed class EquipmentItemInfo : MonoBehaviour
    {
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private Button _selectButton;
        [SerializeField] private Button _removeButton;

        [field: SerializeField] public EquipmentParameters EquipmentParameters { get; private set; }
        [field: SerializeField] public EquipmentContainer EquipmentContainer { get; private set; }

        public void Initialize(EquipmentParameters equipmentParameters, bool select)
        {
            EquipmentParameters = equipmentParameters;

            _selectButton.gameObject.SetActive(false);
            _removeButton.gameObject.SetActive(false);

            if (select)
                _selectButton.gameObject.SetActive(true);
            else
                _removeButton.gameObject.SetActive(true);

            gameObject.SetActive(true);
        }

        public void Equip()
        {
            _inventoryView.SetEquipment(EquipmentParameters);
            EquipmentContainer.RemoveItem(EquipmentParameters);
            Disactivate();
        }

        public void Remove()
        {
            _inventoryView.RemoveEquipment(EquipmentParameters);
            EquipmentContainer.AddItem(EquipmentParameters);
            Disactivate();
        }

        private void Disactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
