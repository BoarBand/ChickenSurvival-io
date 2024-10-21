using System.Collections.Generic;
using UnityEngine;
using SurvivalChicken.ScriptableObjects.EquipmentsParameters;
using TMPro;

namespace SurvivalChicken.Controllers
{
    public sealed class SortEquipmentItems : MonoBehaviour
    {
        public enum SortType
        {
            Rarity,
            Type
        }

        public SortType SelectedSortType;

        [SerializeField] private EquipmentContainer _equipmentContainer;
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private TextMeshProUGUI _buttonTxt;
        [SerializeField] private string _rarityButtonTxt;
        [SerializeField] private string _typeButtonTxt;

        public void OnSort()
        {
            switch (SelectedSortType)
            {
                case SortType.Rarity:
                    SelectedSortType = SortType.Type;
                    break;
                case SortType.Type:
                    SelectedSortType = SortType.Rarity;
                    break;
            }

            Sort(SelectedSortType);
        }

        public void Sort(SortType sortType)
        {
            switch (sortType)
            {
                case SortType.Rarity:
                    RaritySort();
                    _buttonTxt.text = _rarityButtonTxt;
                    SelectedSortType = SortType.Rarity;
                    return;
                case SortType.Type:
                    TypeSort();
                    _buttonTxt.text = _typeButtonTxt;
                    SelectedSortType = SortType.Type;
                    return;
            }
        }

        public void SortBySelected()
        {
            switch (SelectedSortType)
            {
                case SortType.Rarity:
                    RaritySort();
                    return;
                case SortType.Type:
                    TypeSort();
                    return;
            }
        }

        private void RaritySort()
        {
            List<EquipmentParameters> _commonEquipments = new List<EquipmentParameters>();
            List<EquipmentParameters> _rareEquipments = new List<EquipmentParameters>();
            List<EquipmentParameters> _epicEquipments = new List<EquipmentParameters>();
            List<EquipmentParameters> _legendaryEquipments = new List<EquipmentParameters>();

            foreach (EquipmentParameters item in _equipmentContainer.GetList())
            {
                if (item.EquipmentRarity == Structures.EquipmentRarities.EquipmentRarity.Common)
                {
                    _commonEquipments.Add(item);
                    continue;
                }

                if (item.EquipmentRarity == Structures.EquipmentRarities.EquipmentRarity.Rare)
                {
                    _rareEquipments.Add(item);
                    continue;
                }

                if (item.EquipmentRarity == Structures.EquipmentRarities.EquipmentRarity.Epic)
                {
                    _epicEquipments.Add(item);
                    continue;
                }

                if (item.EquipmentRarity == Structures.EquipmentRarities.EquipmentRarity.Legendary)
                {
                    _legendaryEquipments.Add(item);
                    continue;
                }
            }

            _equipmentContainer.ClearList();

            foreach (EquipmentParameters item in _legendaryEquipments) _equipmentContainer.AddItem(item);
            foreach (EquipmentParameters item in _epicEquipments) _equipmentContainer.AddItem(item);
            foreach (EquipmentParameters item in _rareEquipments) _equipmentContainer.AddItem(item);
            foreach (EquipmentParameters item in _commonEquipments) _equipmentContainer.AddItem(item);

            _inventoryView.UpdateEquipmentItemsView();
        }

        private void TypeSort()
        {
            List<EquipmentParameters> _armorEquipments = new List<EquipmentParameters>();
            List<EquipmentParameters> _helmetEquipments = new List<EquipmentParameters>();
            List<EquipmentParameters> _bootsEquipments = new List<EquipmentParameters>();
            List<EquipmentParameters> _attributeEquipments = new List<EquipmentParameters>();
            List<EquipmentParameters> _weaponModuleEquipments = new List<EquipmentParameters>();
            List<EquipmentParameters> _petEquipments = new List<EquipmentParameters>();

            foreach (EquipmentParameters item in _equipmentContainer.GetList())
            {
                if (item.EquipmentType == Structures.EquipmentTypes.EquipmentType.Armor)
                {
                    _armorEquipments.Add(item);
                    continue;
                }

                if (item.EquipmentType == Structures.EquipmentTypes.EquipmentType.Helmet)
                {
                    _helmetEquipments.Add(item);
                    continue;
                }

                if (item.EquipmentType == Structures.EquipmentTypes.EquipmentType.Boots)
                {
                    _bootsEquipments.Add(item);
                    continue;
                }

                if (item.EquipmentType == Structures.EquipmentTypes.EquipmentType.WeaponModule)
                {
                    _weaponModuleEquipments.Add(item);
                    continue;
                }

                if (item.EquipmentType == Structures.EquipmentTypes.EquipmentType.Attribute)
                {
                    _attributeEquipments.Add(item);
                    continue;
                }

                if (item.EquipmentType == Structures.EquipmentTypes.EquipmentType.Pet)
                {
                    _petEquipments.Add(item);
                    continue;
                }
            }

            _equipmentContainer.ClearList();

            foreach (EquipmentParameters item in _armorEquipments) _equipmentContainer.AddItem(item);
            foreach (EquipmentParameters item in _helmetEquipments) _equipmentContainer.AddItem(item);
            foreach (EquipmentParameters item in _bootsEquipments) _equipmentContainer.AddItem(item);
            foreach (EquipmentParameters item in _attributeEquipments) _equipmentContainer.AddItem(item);
            foreach (EquipmentParameters item in _weaponModuleEquipments) _equipmentContainer.AddItem(item);
            foreach (EquipmentParameters item in _petEquipments) _equipmentContainer.AddItem(item);

            _inventoryView.UpdateEquipmentItemsView();
        }
    }
}
