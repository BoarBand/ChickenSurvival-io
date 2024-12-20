using System.Collections.Generic;
using UnityEngine;
using SurvivalChicken.ScriptableObjects.EquipmentsParameters;
using SurvivalChicken.ScriptableObjects.CharactersParameters.Player;
using SurvivalChicken.Interfaces;
using SurvivalChicken.SaveLoadDatas;
using TMPro;

namespace SurvivalChicken.Controllers
{
    public sealed class InventoryView : MonoBehaviour
    {
        [SerializeField] private EquipmentItemView _equipmentItem;
        [SerializeField] private Transform _transformContainer;
        [SerializeField] private EquipmentContainer _equipmentContainer;
        [SerializeField] private EquipmentItemInfo _equipmentItemInfo;
        [SerializeField] private SaveLoadData _saveLoadData;
        [SerializeField] private TextMeshProUGUI _hpViewTxt;
        [SerializeField] private TextMeshProUGUI _totalDamageViewTxt;

        [Header("Inventory Cells")]
        [SerializeField] private InventoryCellView _helmetCell;
        [SerializeField] private InventoryCellView _armorCell;
        [SerializeField] private InventoryCellView _bootsCell;
        [SerializeField] private InventoryCellView _attributeCell;
        [SerializeField] private InventoryCellView _weaponModuleCell;
        [SerializeField] private InventoryCellView _petCell;

        [Header("Container")]
        [SerializeField] private List<EquipmentItemView> _equipmentItemViewsContainer = new List<EquipmentItemView>();

        [field:SerializeField] public PlayerCharacterParameters PlayerParameters { get; set; }

        private void OnEnable()
        {
            UpdateEquipmentItemsView();
            UpdateInventoryCellView(PlayerParameters);
        }

        public void Initalize()
        {
            CreateItemsView();
            UpdateEquipmentItemsView();
            UpdateInventoryCellView(PlayerParameters);

            PlayerParameters.UpdateAdditionValues(_saveLoadData);
        }

        private void CreateItemsView()
        {
            foreach (EquipmentParameters item in _equipmentContainer.GetList())
                CreateItemView(item);
        }

        public void CreateItemView(EquipmentParameters equipmentParameters)
        {
            EquipmentItemView itemView = Instantiate(_equipmentItem);
            itemView.Initialize(equipmentParameters, (i) => _equipmentItemInfo.Initialize(i, true));
            itemView.transform.SetParent(_transformContainer, false);
            _equipmentItemViewsContainer.Add(itemView);
        }

        public void UpdateEquipmentItemsView()
        {
            for (int i = 0; i < _equipmentItemViewsContainer.Count; i++)
            {
                if(_equipmentContainer.ContainerLength > i)
                    _equipmentItemViewsContainer[i].Initialize(_equipmentContainer.GetItemByIndex(i), (i) => _equipmentItemInfo.Initialize(i, true));
            }

            _hpViewTxt.text = PlayerParameters.TotalHealth.ToString();
            _totalDamageViewTxt.text = PlayerParameters.TotalDamage.ToString();
        }

        public void SetEquipment(EquipmentParameters equipmentParameters)
        {
            if (equipmentParameters is HelmetEquipmentParameters)
            {
                if (PlayerParameters.HelmetEquipment != null)
                {
                    _equipmentContainer.AddItem(PlayerParameters.HelmetEquipment);
                    RemoveEquipment(PlayerParameters.HelmetEquipment);
                }
                PlayerParameters.HelmetEquipment = equipmentParameters as HelmetEquipmentParameters;
                _equipmentContainer.RemoveItem(equipmentParameters);
            }

            if (equipmentParameters is ArmorEquipmentParameters)
            {
                if (PlayerParameters.ArmorEquipment != null)
                {
                    _equipmentContainer.AddItem(PlayerParameters.ArmorEquipment);
                    RemoveEquipment(PlayerParameters.ArmorEquipment);
                }
                PlayerParameters.ArmorEquipment = equipmentParameters as ArmorEquipmentParameters;
                _equipmentContainer.RemoveItem(equipmentParameters);
            }

            if (equipmentParameters is BootsEquipmentParameters)
            {
                if (PlayerParameters.BootsEquipment != null)
                {
                    _equipmentContainer.AddItem(PlayerParameters.BootsEquipment);
                    RemoveEquipment(PlayerParameters.BootsEquipment);
                }
                PlayerParameters.BootsEquipment = equipmentParameters as BootsEquipmentParameters;
                _equipmentContainer.RemoveItem(equipmentParameters);
            }

            if (equipmentParameters is AttributeEquipmentParameters)
            {
                if (PlayerParameters.AttributeEquipment != null)
                {
                    _equipmentContainer.AddItem(PlayerParameters.AttributeEquipment);
                    RemoveEquipment(PlayerParameters.AttributeEquipment);
                }
                PlayerParameters.AttributeEquipment = equipmentParameters as AttributeEquipmentParameters;
                _equipmentContainer.RemoveItem(equipmentParameters);
            }

            if (equipmentParameters is WeaponModuleEquipmentParameters)
            {
                if (PlayerParameters.WeaponModuleEquipment != null)
                {
                    _equipmentContainer.AddItem(PlayerParameters.WeaponModuleEquipment);
                    RemoveEquipment(PlayerParameters.WeaponModuleEquipment);
                }
                PlayerParameters.WeaponModuleEquipment = equipmentParameters as WeaponModuleEquipmentParameters;
                _equipmentContainer.RemoveItem(equipmentParameters);
            }

            if (equipmentParameters is PetEquipmentParameters)
            {
                if (PlayerParameters.PetEquipment != null)
                {
                    _equipmentContainer.AddItem(PlayerParameters.PetEquipment);
                    RemoveEquipment(PlayerParameters.PetEquipment);
                }
                PlayerParameters.PetEquipment = equipmentParameters as PetEquipmentParameters;
                _equipmentContainer.RemoveItem(equipmentParameters);
            }

            EquipmentItemView item = GetItemView(equipmentParameters);
            _equipmentItemViewsContainer.Remove(item);
            Destroy(item.gameObject);

            UpdateInventoryCellView(PlayerParameters);
            PlayerParameters.UpdateAdditionValues(_saveLoadData);
            UpdateEquipmentItemsView();

            _saveLoadData.SaveGame();
        }

        public void RemoveEquipment(EquipmentParameters equipmentParameters)
        {
            if (equipmentParameters is HelmetEquipmentParameters)
            {
                PlayerParameters.HelmetEquipment = null;
            }

            if (equipmentParameters is ArmorEquipmentParameters)
            {
                PlayerParameters.ArmorEquipment = null;
            }

            if (equipmentParameters is BootsEquipmentParameters)
            {
                PlayerParameters.BootsEquipment = null;
            }

            if (equipmentParameters is AttributeEquipmentParameters)
            {
                PlayerParameters.AttributeEquipment = null;
            }

            if (equipmentParameters is WeaponModuleEquipmentParameters)
            {
                PlayerParameters.WeaponModuleEquipment = null;
            }

            if (equipmentParameters is PetEquipmentParameters)
            {
                PlayerParameters.PetEquipment = null;
            }

            CreateItemView(equipmentParameters);

            UpdateInventoryCellView(PlayerParameters);
            PlayerParameters.UpdateAdditionValues(_saveLoadData);
            UpdateEquipmentItemsView();

            _saveLoadData.SaveGame();
        }

        public void UpdateInventoryCellView(IPlayerEquipment playerEquipment)
        {
            if (playerEquipment == null)
                return;

            if (playerEquipment.HelmetEquipment != null)
                _helmetCell.Initialize(playerEquipment.HelmetEquipment, _saveLoadData.HelmetUpgradeLevel, (item) => _equipmentItemInfo.Initialize(item, false));
            else
                _helmetCell.Initialize(null, _saveLoadData.HelmetUpgradeLevel, (item) => _equipmentItemInfo.Initialize(item, false));

            if (playerEquipment.ArmorEquipment != null)
                _armorCell.Initialize(playerEquipment.ArmorEquipment, _saveLoadData.ArmorUpgradeLevel, (item) => _equipmentItemInfo.Initialize(item, false));
            else
                _armorCell.Initialize(null, _saveLoadData.ArmorUpgradeLevel, (item) => _equipmentItemInfo.Initialize(item, false));

            if (playerEquipment.BootsEquipment != null)
                _bootsCell.Initialize(playerEquipment.BootsEquipment, _saveLoadData.BootsUpgradeLevel, (item) => _equipmentItemInfo.Initialize(item, false));
            else
                _bootsCell.Initialize(null, _saveLoadData.BootsUpgradeLevel, (item) => _equipmentItemInfo.Initialize(item, false));

            if (playerEquipment.AttributeEquipment != null)
                _attributeCell.Initialize(playerEquipment.AttributeEquipment, _saveLoadData.AttributeUpgradeLevel, (item) => _equipmentItemInfo.Initialize(item, false));
            else
                _attributeCell.Initialize(null, _saveLoadData.AttributeUpgradeLevel, (item) => _equipmentItemInfo.Initialize(item, false));

            if (playerEquipment.WeaponModuleEquipment != null)
                _weaponModuleCell.Initialize(playerEquipment.WeaponModuleEquipment, _saveLoadData.WeaponModuleUpgradeLevel, (item) => _equipmentItemInfo.Initialize(item, false));
            else
                _weaponModuleCell.Initialize(null, _saveLoadData.WeaponModuleUpgradeLevel, (item) => _equipmentItemInfo.Initialize(item, false));

            if (playerEquipment.PetEquipment != null)
                _petCell.Initialize(playerEquipment.PetEquipment, _saveLoadData.PetUpgradeLevel, (item) => _equipmentItemInfo.Initialize(item, false));
            else
                _petCell.Initialize(null, _saveLoadData.PetUpgradeLevel, (item) => _equipmentItemInfo.Initialize(item, false));
        }

        private EquipmentItemView GetItemView(EquipmentParameters parameters)
        {
            foreach (EquipmentItemView item in _equipmentItemViewsContainer)
            {
                if (item.ItemParameters.GetHashCode() == parameters.GetHashCode())
                    return item;
            }
            return null;
        }
    }
}
