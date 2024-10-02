using UnityEngine;
using UnityEngine.UI;
using SurvivalChicken.ScriptableObjects.EquipmentsParameters;
using SurvivalChicken.Structures;
    
namespace SurvivalChicken.Controllers
{
    public class InventoryCellView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _defaultIcon;
        [SerializeField] private Image _frame;
        [SerializeField] private EquipmentTypes.EquipmentType _equipmentType;

        [Header("Rarity Frames")]
        [SerializeField] private Sprite _commonFrame;
        [SerializeField] private Sprite _rareFrame;
        [SerializeField] private Sprite _epicFrame;
        [SerializeField] private Sprite _legendaryFrame;

        public EquipmentParameters SelectedItem { get; private set; }

        public void Initialize(EquipmentParameters equipmentParameters)
        {
            if(equipmentParameters == null)
            {
                UpdateView(null);

                SelectedItem = null;
                return;
            }

            if (equipmentParameters.EquipmentType != _equipmentType)
                return;

            UpdateView(equipmentParameters);

            SelectedItem = equipmentParameters;
        }

        private void UpdateView(EquipmentParameters equipmentParameters)
        {
            if (equipmentParameters == null)
            {
                _icon.gameObject.SetActive(false);
                _defaultIcon.gameObject.SetActive(true);
                return;
            }

            SetFrame(equipmentParameters.EquipmentRarity);

            _icon.sprite = equipmentParameters.Icon;

            _icon.gameObject.SetActive(true);
            _defaultIcon.gameObject.SetActive(false);
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
    }
}