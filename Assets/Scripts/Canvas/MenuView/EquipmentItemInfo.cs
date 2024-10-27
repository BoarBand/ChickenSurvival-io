using UnityEngine;
using SurvivalChicken.ScriptableObjects.EquipmentsParameters;
using SurvivalChicken.Structures;
using UnityEngine.UI;
using TMPro;
using SurvivalChicken.SaveLoadDatas;

namespace SurvivalChicken.Controllers
{
    public sealed class EquipmentItemInfo : MonoBehaviour
    {
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private SaveLoadData _saveLoadData;
        [SerializeField] private Image _iconType;
        [SerializeField] private Image _frame;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _topBG;
        [SerializeField] private Image _valueTypeImg;
        [SerializeField] private Sprite _damageIcon;
        [SerializeField] private Sprite _healthIcon;
        [SerializeField] private ValuesView _valuesView;
        [SerializeField] private TextMeshProUGUI _costTxt;
        [SerializeField] private TextMeshProUGUI _labelTxt;
        [SerializeField] private TextMeshProUGUI _rarityTxt;
        [SerializeField] private TextMeshProUGUI _descriptionTxt;
        [SerializeField] private TextMeshProUGUI _levelTxt;
        [SerializeField] private TextMeshProUGUI _commonSkillDescription;
        [SerializeField] private TextMeshProUGUI _rareSkillDescription;
        [SerializeField] private TextMeshProUGUI _epicSkillDescription;
        [SerializeField] private TextMeshProUGUI _legendarySkillDescription;
        [SerializeField] private TextMeshProUGUI _valueAddition;
        [SerializeField] private TextMeshProUGUI _upgradeValueAddition;
        [SerializeField] private Transform _levelUpNotification;

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

            _descriptionTxt.text = equipmentParameters.Description;
            _commonSkillDescription.text = equipmentParameters.CommonSkillDescription;
            _rareSkillDescription.text = equipmentParameters.RareSkillDescription;
            _epicSkillDescription.text = equipmentParameters.EpicSkillDescription;
            _legendarySkillDescription.text = equipmentParameters.LegendarySkillDescription;

            UpdateCost(out _);
            UpdateIconsView(equipmentParameters);
            UpdateRarityView(equipmentParameters.EquipmentRarity);
            UpdateAdditionValues(equipmentParameters);
        }

        public void Equip()
        {
            _inventoryView.SetEquipment(EquipmentParameters);
            Disactivate();
        }

        public void Upgrade()
        {
            void Successful() 
            {
                _levelUpNotification.gameObject.SetActive(false);
                _levelUpNotification.gameObject.SetActive(true);
            }

            UpdateCost(out int cost);

            if (EquipmentParameters is HelmetEquipmentParameters)
            {
                if(_valuesView.TrySpendCoins(cost, Successful))
                {
                    _saveLoadData.HelmetUpgradeLevel++;
                    UpdateActions();
                    UpdateCost(out cost);
                }
                return;
            }

            if (EquipmentParameters is ArmorEquipmentParameters)
            {
                if(_valuesView.TrySpendCoins(cost, Successful))
                {
                    _saveLoadData.ArmorUpgradeLevel++;
                    UpdateActions();
                    UpdateCost(out cost);
                }
                return;
            }

            if (EquipmentParameters is BootsEquipmentParameters)
            {
                if(_valuesView.TrySpendCoins(cost, Successful))
                {
                    _saveLoadData.BootsUpgradeLevel++;
                    UpdateActions();
                    UpdateCost(out cost);
                }
                return;
            }

            if (EquipmentParameters is AttributeEquipmentParameters)
            {
                if(_valuesView.TrySpendCoins(cost, Successful))
                {
                    _saveLoadData.AttributeUpgradeLevel++;
                    UpdateActions();
                    UpdateCost(out cost);
                }
                return;
            }

            if (EquipmentParameters is WeaponModuleEquipmentParameters)
            {
                if(_valuesView.TrySpendCoins(cost, Successful))
                {
                    _saveLoadData.WeaponModuleUpgradeLevel++;
                    UpdateActions();
                    UpdateCost(out cost);
                }
                return;
            }

            if (EquipmentParameters is PetEquipmentParameters)
            {
                if(_valuesView.TrySpendCoins(cost, Successful))
                {
                    _saveLoadData.PetUpgradeLevel++;
                    UpdateActions();
                    UpdateCost(out cost);
                }
                return;
            }
        }

        private void UpdateActions()
        {
            _saveLoadData.SaveGame();
            UpdateIconsView(EquipmentParameters);
            _inventoryView.UpdateInventoryCellView(_inventoryView.PlayerParameters);
            _inventoryView.PlayerParameters.UpdateAdditionValues(_saveLoadData);
            UpdateAdditionValues(EquipmentParameters);
        }

        public void Remove()
        {
            EquipmentContainer.AddItem(EquipmentParameters);
            _inventoryView.RemoveEquipment(EquipmentParameters);
            Disactivate();
        }

        public void FastUpgrade()
        {
            for (int i = 0; i < 5; i++)
                Upgrade();
        }

        private void UpdateCost(out int cost)
        {
            int defaultCost = 100;

            if (EquipmentParameters is HelmetEquipmentParameters)
            {
                cost = defaultCost * _saveLoadData.HelmetUpgradeLevel;
                _costTxt.text = cost.ToString();
                return;
            }

            if (EquipmentParameters is ArmorEquipmentParameters)
            {
                cost = defaultCost * _saveLoadData.ArmorUpgradeLevel;
                _costTxt.text = cost.ToString();
                return;
            }

            if (EquipmentParameters is BootsEquipmentParameters)
            {
                cost = defaultCost * _saveLoadData.BootsUpgradeLevel;
                _costTxt.text = cost.ToString();
                return;
            }

            if (EquipmentParameters is AttributeEquipmentParameters)
            {
                cost = defaultCost * _saveLoadData.AttributeUpgradeLevel;
                _costTxt.text = cost.ToString();
                return;
            }

            if (EquipmentParameters is WeaponModuleEquipmentParameters)
            {
                cost = defaultCost * _saveLoadData.WeaponModuleUpgradeLevel;
                _costTxt.text = cost.ToString();
                return;
            }

            if (EquipmentParameters is PetEquipmentParameters)
            {
                cost = defaultCost * _saveLoadData.PetUpgradeLevel;
                _costTxt.text = cost.ToString();
                return;
            }

            cost = defaultCost;
        }

        private void UpdateAdditionValues(EquipmentParameters equipmentParameters)
        {
            if (equipmentParameters is HelmetEquipmentParameters)
            {
                _valueAddition.text = $"Health +{_saveLoadData.HelmetUpgradeLevel * equipmentParameters.Value}";
                _upgradeValueAddition.text = $"Upgrade Health +{(_saveLoadData.HelmetUpgradeLevel + 1) * equipmentParameters.Value}";
                _valueTypeImg.sprite = _healthIcon;
                return;
            }

            if (equipmentParameters is ArmorEquipmentParameters)
            {
                _valueAddition.text = $"Health +{_saveLoadData.ArmorUpgradeLevel * equipmentParameters.Value}";
                _upgradeValueAddition.text = $"Upgrade Health +{(_saveLoadData.ArmorUpgradeLevel + 1) * equipmentParameters.Value}";
                _valueTypeImg.sprite = _healthIcon;
                return;
            }

            if (equipmentParameters is BootsEquipmentParameters)
            {
                _valueAddition.text = $"Health +{_saveLoadData.BootsUpgradeLevel * equipmentParameters.Value}";
                _upgradeValueAddition.text = $"Upgrade Health +{(_saveLoadData.BootsUpgradeLevel + 1) * equipmentParameters.Value}";
                _valueTypeImg.sprite = _healthIcon;
                return;
            }

            if (equipmentParameters is AttributeEquipmentParameters)
            {
                _valueAddition.text = $"Damage +{_saveLoadData.AttributeUpgradeLevel * equipmentParameters.Value}";
                _upgradeValueAddition.text = $"Upgrade Damage +{(_saveLoadData.AttributeUpgradeLevel + 1) * equipmentParameters.Value}";
                _valueTypeImg.sprite = _damageIcon;
                return;
            }

            if (equipmentParameters is WeaponModuleEquipmentParameters)
            {
                _valueAddition.text = $"Damage +{_saveLoadData.WeaponModuleUpgradeLevel * equipmentParameters.Value}";
                _upgradeValueAddition.text = $"Upgrade Damage +{(_saveLoadData.WeaponModuleUpgradeLevel + 1) * equipmentParameters.Value}";
                return;
            }

            if (equipmentParameters is PetEquipmentParameters)
            {
                _valueAddition.text = $"Damage +{_saveLoadData.PetUpgradeLevel * equipmentParameters.Value}";
                _upgradeValueAddition.text = $"Upgrade Damage +{(_saveLoadData.PetUpgradeLevel + 1) * equipmentParameters.Value}";
                _valueTypeImg.sprite = _damageIcon;
                return;
            }
        }

        private void UpdateIconsView(EquipmentParameters equipmentParameters)
        {
            _icon.sprite = equipmentParameters.Icon;

            if (equipmentParameters is HelmetEquipmentParameters)
            {
                _iconType.sprite = _helmetIcon;
                UpdateLvlTxtView(_saveLoadData.HelmetUpgradeLevel);
                return;
            }

            if (equipmentParameters is ArmorEquipmentParameters)
            {
                _iconType.sprite = _armorIcon;
                UpdateLvlTxtView(_saveLoadData.ArmorUpgradeLevel);
                return;
            }

            if (equipmentParameters is BootsEquipmentParameters)
            {
                _iconType.sprite = _bootsIcon;
                UpdateLvlTxtView(_saveLoadData.BootsUpgradeLevel);
                return;
            }

            if (equipmentParameters is AttributeEquipmentParameters)
            {
                _iconType.sprite = _attributeIcon;
                UpdateLvlTxtView(_saveLoadData.AttributeUpgradeLevel);
                return;
            }

            if (equipmentParameters is WeaponModuleEquipmentParameters)
            {
                _iconType.sprite = _weaponModuleIcon;
                UpdateLvlTxtView(_saveLoadData.WeaponModuleUpgradeLevel);
                return;
            }

            if (equipmentParameters is PetEquipmentParameters)
            {
                _iconType.sprite = _petIcon;
                UpdateLvlTxtView(_saveLoadData.PetUpgradeLevel);
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

        private void UpdateLvlTxtView(int level)
        {
            if (level < 10)
            {
                _levelTxt.text = $"LV.0{level}";
                return;
            }

            _levelTxt.text = $"LV.{level}";
        }

        private void Disactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
