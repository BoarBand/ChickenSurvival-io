using UnityEngine;
using SurvivalChicken.SaveLoadDatas;
using TMPro;

namespace SurvivalChicken.Controllers
{
    public sealed class StoreChest : MonoBehaviour
    {
        public enum ValueTypes { Coins, Gems, GoldKeys }

        [SerializeField] private int _chanceToGetCommonItem;
        [SerializeField] private int _chanceToGetRareItem;
        [SerializeField] private int _chanceToGetEpicItem;
        [SerializeField] private int _chanceToGetLegendaryItem;

        [SerializeField] private OpenChest _openChest;
        [SerializeField] private Sprite _chestSprite;
        [SerializeField] private ValuesView _valuesView;
        [SerializeField] private SaveLoadData _saveLoadData;
        [SerializeField] private TextMeshProUGUI _costTxt;

        [Header("Costs")]
        [SerializeField] private ValueTypes _selectedValue;
        [SerializeField] private int _cost;

        private void OnEnable()
        {
            CostView();
        }

        public void TryInvokeChest()
        {
            switch (_selectedValue)
            {
                case ValueTypes.Coins:
                    TryInvokeChestForCoins();
                    return;
                case ValueTypes.Gems:
                    TryInvokeChestForGems();
                    return;
                case ValueTypes.GoldKeys:
                    TryInvokeChestForGoldKeys();
                    CostView();
                    return;
            }
        }

        private void TryInvokeChestForCoins()
        {
            if (_saveLoadData.Coins < _cost)
                return;

            _saveLoadData.Coins -= _cost;

            InvokeChest();
        }

        private void TryInvokeChestForGems()
        {
            if (_saveLoadData.Gems < _cost)
                return;

            _saveLoadData.Gems -= _cost;

            InvokeChest();
        }

        private void TryInvokeChestForGoldKeys()
        {
            if (_saveLoadData.GoldKeys < _cost)
                return;

            _saveLoadData.GoldKeys -= _cost;

            InvokeChest();
        }

        private void InvokeChest()
        {
            _saveLoadData.SaveGame();

            _valuesView.UpdateAllValues();

            _openChest.Initialize(_chestSprite,
                _chanceToGetCommonItem,
                _chanceToGetRareItem,
                _chanceToGetEpicItem,
                _chanceToGetLegendaryItem);
        }

        private void CostView()
        {
            switch (_selectedValue)
            {
                case ValueTypes.Coins:
                    _costTxt.text = _cost.ToString();
                    return;
                case ValueTypes.Gems:
                    _costTxt.text = _cost.ToString();
                    return;
                case ValueTypes.GoldKeys:
                    _costTxt.text = $"{_saveLoadData.GoldKeys}/{_cost}";
                    return;
            }
        }
    }
}
