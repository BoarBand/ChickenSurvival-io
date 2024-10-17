using UnityEngine;
using TMPro;
using SurvivalChicken.SaveLoadDatas;
using System;

namespace SurvivalChicken.Controllers
{
    public class ValuesView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinsTxt;
        [SerializeField] private TextMeshProUGUI _gemsTxt;
        [SerializeField] private TextMeshProUGUI _energyTxt;

        [SerializeField] private SaveLoadData _saveLoadData;

        public void Initialize()
        {
            UpdateAllValues();
        }

        public void UpdateCoinView(int value)
        {
            _coinsTxt.text = value.ToString();
        }

        public void UpdateGemsView(int value)
        {
            _gemsTxt.text = value.ToString();
        }

        public void UpdateEnergyView(int value)
        {
            _energyTxt.text = value.ToString() + "/100";
        }

        public void UpdateAllValues()
        {
            UpdateGemsView(_saveLoadData.Gems);
            UpdateCoinView(_saveLoadData.Coins);
            UpdateEnergyView(_saveLoadData.Energy);
        }

        public bool TrySpendCoins(int amount, Action success = null, Action failed = null)
        {
            if(_saveLoadData.Coins >= amount)
            {
                _saveLoadData.Coins -= amount;
                success?.Invoke();
                _saveLoadData.SaveGame();
                UpdateCoinView(_saveLoadData.Coins);
                return true;
            }

            failed?.Invoke();
            return false;
        }
    }
}
