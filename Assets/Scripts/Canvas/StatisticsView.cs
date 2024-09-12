using UnityEngine;
using TMPro;

namespace SurvivalChicken.Controllers
{
    public class StatisticsView : MonoBehaviour
    {
        public static StatisticsView Instance;

        [SerializeField] private TextMeshProUGUI _killsTxt;

        private int _killsAmount;

        public void Initialize()
        {
            Instance = this;

            _killsAmount = 0;

            UpdateTxtView();
        }

        public void IncreaseKillsAmount()
        {
            _killsAmount++;
            UpdateTxtView();
        }

        private void UpdateTxtView()
        {
            _killsTxt.text = _killsAmount.ToString();
        }
    }
}