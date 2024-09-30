using UnityEngine;
using TMPro;
using SurvivalChicken.SaveLoadDatas;

namespace SurvivalChicken.Controllers
{
    public class ValuesView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinsTxt;
        [SerializeField] private TextMeshProUGUI _gemsTxt;
        [SerializeField] private TextMeshProUGUI _energyTxt;

        private SaveLoadData _saveLoadData = new SaveLoadData();

        public void Initialize()
        {
            if (_saveLoadData.TryGetCurrencyValue(out int coins, SaveLoadData.CurrencyTypes.Coins))
                UpdateCoinView(coins);

            if (_saveLoadData.TryGetCurrencyValue(out int gems, SaveLoadData.CurrencyTypes.Gems))
                UpdateGemsView(gems);

            if (_saveLoadData.TryGetCurrencyValue(out int energy, SaveLoadData.CurrencyTypes.Energy))
                UpdateEnergyView(energy);
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
    }
}
