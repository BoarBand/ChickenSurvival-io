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

        [SerializeField] private SaveLoadData _saveLoadData;

        public void Initialize()
        {
            _saveLoadData.Gems += 100;
            _saveLoadData.SaveGame();
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
    }
}
