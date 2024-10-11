using UnityEngine;
using UnityEngine.UI;
using SurvivalChicken.Interfaces;
using SurvivalChicken.Structures;
using TMPro;

namespace SurvivalChicken.CollectItems
{
    public class CollectedItemView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _amountTxt;
        [SerializeField] private Image _frame;

        [Header("Rarity Frames")]
        [SerializeField] private Sprite _commonFrame;
        [SerializeField] private Sprite _rareFrame;
        [SerializeField] private Sprite _epicFrame;
        [SerializeField] private Sprite _legendaryFrame;

        public void Initialize(ICollect collectItem)
        {
            _icon.sprite = collectItem.Icon;
            _amountTxt.text = collectItem.Amount.ToString();
            UpdateFrame(collectItem.EquipmentRarity);
        }

        private void UpdateFrame(EquipmentRarities.EquipmentRarity equipmentRarity)
        {
            switch (equipmentRarity)
            {
                case EquipmentRarities.EquipmentRarity.Common:
                    _frame.sprite = _commonFrame;
                    return;
                case EquipmentRarities.EquipmentRarity.Rare:
                    _frame.sprite = _rareFrame;
                    return;
                case EquipmentRarities.EquipmentRarity.Epic:
                    _frame.sprite = _epicFrame;
                    return;
                case EquipmentRarities.EquipmentRarity.Legendary:
                    _frame.sprite = _legendaryFrame;
                    return;
            }
        }
    }
}
