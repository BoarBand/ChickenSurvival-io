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
            UpdateCoinView(_saveLoadData.Coins);
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
