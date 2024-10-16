using System;
using UnityEngine;
using UnityEngine.UI;
using SurvivalChicken.ScriptableObjects.EquipmentsParameters;
using SurvivalChicken.Structures;
using TMPro;
    
namespace SurvivalChicken.Controllers
{
    public class InventoryCellView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _defaultIcon;
        [SerializeField] private Image _frame;
        [SerializeField] private TextMeshProUGUI _levelTxt;
        [SerializeField] private EquipmentTypes.EquipmentType _equipmentType;

        [Header("Rarity Frames")]
        [SerializeField] private Sprite _defaultFrame;
        [SerializeField] private Sprite _commonFrame;
        [SerializeField] private Sprite _rareFrame;
        [SerializeField] private Sprite _epicFrame;
        [SerializeField] private Sprite _legendaryFrame;

        public EquipmentParameters SelectedItem { get; private set; }
        
        private event Action<EquipmentParameters> InvokedItemInfo;

        public void Initialize(EquipmentParameters equipmentParameters, int level, Action<EquipmentParameters> invokeItemInfo)
        {
            if(equipmentParameters == null)
            {
                UpdateView(null);
                UpdateLvlTxtView(level);

                SelectedItem = null;
                return;
            }

            if (equipmentParameters.EquipmentType != _equipmentType)
                return;

            UpdateView(equipmentParameters);
            UpdateLvlTxtView(level);

            InvokedItemInfo += invokeItemInfo;

            SelectedItem = equipmentParameters;
        }

        private void UpdateView(EquipmentParameters equipmentParameters)
        {
            if (equipmentParameters == null)
            {
                _icon.gameObject.SetActive(false);
                _defaultIcon.gameObject.SetActive(true);
                SetFrame(EquipmentRarities.EquipmentRarity.Default);
                return;
            }

            SetFrame(equipmentParameters.EquipmentRarity);

            _icon.sprite = equipmentParameters.Icon;

            _icon.gameObject.SetActive(true);
            _defaultIcon.gameObject.SetActive(false);
        }

        private void SetFrame(EquipmentRarities.EquipmentRarity equipmentRarity)
        {
            _frame.sprite = equipmentRarity switch
            {
                EquipmentRarities.EquipmentRarity.Common => _commonFrame,
                EquipmentRarities.EquipmentRarity.Rare => _rareFrame,
                EquipmentRarities.EquipmentRarity.Epic => _epicFrame,
                EquipmentRarities.EquipmentRarity.Legendary => _legendaryFrame,
                _ => _defaultFrame,
            };
        }

        public void OnClick()
        {
            if (SelectedItem == null)
                return;

            InvokedItemInfo?.Invoke(SelectedItem);
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
    }
}