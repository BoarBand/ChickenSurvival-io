using UnityEngine;
using UnityEngine.UI;
using SurvivalChicken.ScriptableObjects.EquipmentsParameters;
using SurvivalChicken.Structures;

namespace SurvivalChicken.Controllers
{
    public class EquipmentItemView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _typeIcon;
        [SerializeField] private Image _frame;

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

        public EquipmentParameters ItemParameters { get; private set; }

        public void Initialize(EquipmentParameters equipmentParameters)
        {
            ItemParameters = equipmentParameters;

            UpdateView(equipmentParameters);
        }

        private void UpdateView(EquipmentParameters equipmentParameters)
        {
            _icon.sprite = equipmentParameters.Icon;

            SetFrame(equipmentParameters.EquipmentRarity);
            SetTypeIcon(equipmentParameters.EquipmentType);
        }

        private void SetFrame(EquipmentRarities.EquipmentRarity equipmentRarity)
        {
            switch (equipmentRarity)
            {
                case EquipmentRarities.EquipmentRarity.Common:
                    _frame.sprite = _commonFrame;
                    break;
                case EquipmentRarities.EquipmentRarity.Rare:
                    _frame.sprite = _rareFrame;
                    break;
                case EquipmentRarities.EquipmentRarity.Epic:
                    _frame.sprite = _epicFrame;
                    break;
                case EquipmentRarities.EquipmentRarity.Legendary:
                    _frame.sprite = _legendaryFrame;
                    break;
            }
        }

        private void SetTypeIcon(EquipmentTypes.EquipmentType equipmentType)
        {
            switch (equipmentType)
            {
                case EquipmentTypes.EquipmentType.Armor:
                    _typeIcon.sprite = _armorIcon;
                    break;
                case EquipmentTypes.EquipmentType.Attribute:
                    _typeIcon.sprite = _attributeIcon;
                    break;
                case EquipmentTypes.EquipmentType.Boots:
                    _typeIcon.sprite = _bootsIcon;
                    break;
                case EquipmentTypes.EquipmentType.Helmet:
                    _typeIcon.sprite = _helmetIcon;
                    break;
                case EquipmentTypes.EquipmentType.Pet:
                    _typeIcon.sprite = _petIcon;
                    break;
                case EquipmentTypes.EquipmentType.WeaponModule:
                    _typeIcon.sprite = _weaponModuleIcon;
                    break;
            }
        }
    }
}
