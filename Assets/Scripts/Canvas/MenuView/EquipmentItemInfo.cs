using UnityEngine;
using SurvivalChicken.ScriptableObjects.EquipmentsParameters;
using SurvivalChicken.Structures;
using UnityEngine.UI;
using TMPro;

namespace SurvivalChicken.Controllers
{
    public sealed class EquipmentItemInfo : MonoBehaviour
    {
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private Image _iconType;
        [SerializeField] private Image _frame;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _topBG;
        [SerializeField] private TextMeshProUGUI _labelTxt;
        [SerializeField] private TextMeshProUGUI _rarityTxt;

        [Header("Buttons")]
        [SerializeField] private Button _selectButton;
        [SerializeField] private Button _removeButton;

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

            UpdateIconsView(equipmentParameters);
            UpdateRarityView(equipmentParameters.EquipmentRarity);
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

        private void UpdateIconsView(EquipmentParameters equipmentParameters)
        {
            _icon.sprite = equipmentParameters.Icon;

            if (equipmentParameters is HelmetEquipmentParameters)
            {
                _iconType.sprite = _helmetIcon;
                return;
            }

            if (equipmentParameters is ArmorEquipmentParameters)
            {
                _iconType.sprite = _armorIcon;
                return;
            }

            if (equipmentParameters is BootsEquipmentParameters)
            {
                _iconType.sprite = _bootsIcon;
                return;
            }

            if (equipmentParameters is AttributeEquipmentParameters)
            {
                _iconType.sprite = _attributeIcon;
                return;
            }

            if (equipmentParameters is WeaponModuleEquipmentParameters)
            {
                _iconType.sprite = _weaponModuleIcon;
                return;
            }

            if (equipmentParameters is PetEquipmentParameters)
            {
                _iconType.sprite = _petIcon;
                return;
            }
        }

        private void UpdateRarityView(EquipmentRarities.EquipmentRarity equipmentRarity)
        {
            switch (equipmentRarity)
            {
                case EquipmentRarities.EquipmentRarity.Common:
                    _labelTxt.color = _commonTextColor;
                    _frame.sprite = _commonFrame;
                    _rarityTxt.color = _commonTextColor;
                    _rarityTxt.text = "Common";
                    _topBG.color = _commonTextColor;
                    break;
                case EquipmentRarities.EquipmentRarity.Rare:
                    _labelTxt.color = _rareTextColor;
                    _frame.sprite = _rareFrame;
                    _rarityTxt.color = _rareTextColor;
                    _rarityTxt.text = "Rare";
                    _topBG.color = _rareTextColor;
                    break;
                case EquipmentRarities.EquipmentRarity.Epic:
                    _labelTxt.color = _epicTextColor;
                    _frame.sprite = _epicFrame;
                    _rarityTxt.color = _epicTextColor;
                    _rarityTxt.text = "Epic";
                    _topBG.color = _epicTextColor;
                    break;
                case EquipmentRarities.EquipmentRarity.Legendary:
                    _labelTxt.color = _legendaryTextColor;
                    _frame.sprite = _legendaryFrame;
                    _rarityTxt.color = _legendaryTextColor;
                    _rarityTxt.text = "Legendary";
                    _topBG.color = _legendaryTextColor;
                    break;
            }
        }

        private void Disactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
