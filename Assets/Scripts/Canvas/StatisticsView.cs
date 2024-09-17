using UnityEngine;
using TMPro;

namespace SurvivalChicken.Controllers
{
    public class StatisticsView : MonoBehaviour
    {
        public static StatisticsView Instance;

        [SerializeField] private TextMeshProUGUI _killsTxt;
        [SerializeField] private TextMeshProUGUI _coinsTxt;

        public int KillsAmount { get; private set; }
        public int CoinsAmount { get; private set; }

        public void Initialize()
        {
            Instance = this;

            KillsAmount = 0;

            UpdateTxtView();
        }

        public void IncreaseKillsAmount()
        {
            KillsAmount++;
            UpdateTxtView();
        }

        public void IncreaseCoinsAmount(int value)
        {
            CoinsAmount += value;
            _coinsTxt.text = CoinsAmount.ToString();
        }

        private void UpdateTxtView()
        {
            _killsTxt.text = KillsAmount.ToString();
        }
    }
}