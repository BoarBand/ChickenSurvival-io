using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SurvivalChicken.ScriptableObjects.EquipmentsParameters;
using SurvivalChicken.Structures;
using SurvivalChicken.SaveLoadDatas;

namespace SurvivalChicken.Controllers
{
    public sealed class OpenChest : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _itemLabel;
        [SerializeField] private Image _frame;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _typeIcon;

        [SerializeField] private AllEquipmentItemsContainer _allItemsContainer;
        [SerializeField] private EquipmentContainer _equipmentContainer;
        [SerializeField] private SaveLoadData _saveLoadData;
        [SerializeField] private InventoryView _inventoryView;

        [Header("Rarity Frames")]
        [SerializeField] private Sprite _commonFrame;
        [SerializeField] private Sprite _rareFrame;
        [SerializeField] private Sprite _epicFrame;
        [SerializeField] private Sprite _legendaryFrame;

        [Header("Type Imgs")]
        [SerializeField] private Sprite _helmetIcon;
        [SerializeField] private Sprite _armorIcon;
        [SerializeField] private Sprite _bootsIcon;
        [SerializeField] private Sprite _attributeIcon;
        [SerializeField] private Sprite _weaponModuleIcon;
        [SerializeField] private Sprite _petIcon;

        [Header("Texts Color")]
        [SerializeField] private Color32 _commonTextColor;
        [SerializeField] private Color32 _rareTextColor;
        [SerializeField] private Color32 _epicTextColor;
        [SerializeField] private Color32 _legendaryTextColor;

        private void OnEnable()
        {
            Initialize();
        }

        public void Initialize()
        {
            EquipmentParameters equipmentParameters = _allItemsContainer.GetRandomEquipment();

            if (equipmentParameters == null)
                return;

            UpdateLabel(equipmentParameters);
            UpdateRarityView(equipmentParameters.EquipmentRarity);
            UpdateIconsView(equipmentParameters);

            _equipmentContainer.AddItem(equipmentParameters);

            _inventoryView.CreateItemView(equipmentParameters);

            _saveLoadData.SaveGame();
        }

        private void UpdateIconsView(EquipmentParameters equipmentParameters)
        {
            _icon.sprite = equipmentParameters.Icon;

            if(equipmentParameters is HelmetEquipmentParameters)
            {
                _typeIcon.sprite = _helmetIcon;
                return;
            }

            if (equipmentParameters is ArmorEquipmentParameters)
            {
                _typeIcon.sprite = _armorIcon;
                return;
            }

            if (equipmentParameters is BootsEquipmentParameters)
            {
                _typeIcon.sprite = _bootsIcon;
                return;
            }

            if (equipmentParameters is AttributeEquipmentParameters)
            {
                _typeIcon.sprite = _attributeIcon;
                return;
            }

            if (equipmentParameters is WeaponModuleEquipmentParameters)
            {
                _typeIcon.sprite = _weaponModuleIcon;
                return;
            }

            if (equipmentParameters is PetEquipmentParameters)
            {
                _typeIcon.sprite = _petIcon;
                return;
            }
        }

        private void UpdateRarityView(EquipmentRarities.EquipmentRarity equipmentRarity)
        {
            switch (equipmentRarity)
            {
                case EquipmentRarities.EquipmentRarity.Common:
                    _itemLabel.color = _commonTextColor;
                    _frame.sprite = _commonFrame;
                    break;
                case EquipmentRarities.EquipmentRarity.Rare:
                    _itemLabel.color = _rareTextColor;
                    _frame.sprite = _rareFrame;
                    break;
                case EquipmentRarities.EquipmentRarity.Epic:
                    _itemLabel.color = _epicTextColor;
                    _frame.sprite = _epicFrame;
                    break;
                case EquipmentRarities.EquipmentRarity.Legendary:
                    _itemLabel.color = _legendaryTextColor;
                    _frame.sprite = _legendaryFrame;
                    break;
            }
        }

        private void UpdateLabel(EquipmentParameters equipmentParameters)
        {
            switch (equipmentParameters.EquipmentRarity)
            {
                case EquipmentRarities.EquipmentRarity.Common:
                    _itemLabel.text = $"Common\n{equipmentParameters.Label}";
                    break;
                case EquipmentRarities.EquipmentRarity.Rare:
                    _itemLabel.text = $"Rare\n{equipmentParameters.Label}";
                    break;
                case EquipmentRarities.EquipmentRarity.Epic:
                    _itemLabel.text = $"Epic\n{equipmentParameters.Label}";
                    break;
                case EquipmentRarities.EquipmentRarity.Legendary:
                    _itemLabel.text = $"Legendary\n{equipmentParameters.Label}";
                    break;
            }
        }
    }
}
