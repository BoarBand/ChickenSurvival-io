using System.Collections;
using UnityEngine;
using TMPro;

namespace SurvivalChicken.Controllers
{
    public class GameOverView : MonoBehaviour
    {
        public static GameOverView Instance;

        [SerializeField] private TimerView _timer;
        [SerializeField] private StatisticsView _statistics;

        [Header("Died Image")]
        [SerializeField] private GameObject _diedImage;
        [SerializeField] private TextMeshProUGUI _diedTimeTxt;
        [SerializeField] private TextMeshProUGUI _diedKillsTxt;
        [SerializeField] private TextMeshProUGUI _diedCoinsTxt;

        [Header("Victory Image")]
        [SerializeField] private GameObject _victoryImage;
        [SerializeField] private TextMeshProUGUI _victoryTimeTxt;
        [SerializeField] private TextMeshProUGUI _victoryKillsTxt;
        [SerializeField] private TextMeshProUGUI _victoryCoinsTxt;

        private Coroutine _waitUntilVictoryCoroutine;

        private readonly int TimeToInvokeVictoryImage = 900;

        private void Awake()
        {
            Instance = this;

            if (_waitUntilVictoryCoroutine != null)
                StopCoroutine(_waitUntilVictoryCoroutine);
            _waitUntilVictoryCoroutine = StartCoroutine(WaitUntilVictory());
        }

        private IEnumerator WaitUntilVictory()
        {
            yield return new WaitUntil(() => _timer.Time >= TimeToInvokeVictoryImage);

            EnableVictoryImage();
        }

        public void EnableDiedImage()
        {
            int time = _timer.Time;

            int minutes = time / 60;
            int seconds = time - (minutes * 60);

            string minutesTxt = minutes <= 9 ? $"0{minutes}" : $"{minutes}";
            string secondsTxt = seconds <= 9 ? $"0{seconds}" : $"{seconds}";

            _diedTimeTxt.text = $"{minutesTxt}:{secondsTxt}";
            _diedKillsTxt.text = _statistics.KillsAmount.ToString();
            _diedCoinsTxt.text = _statistics.CoinsAmount.ToString();
            _diedImage.SetActive(true);
            Time.timeScale = 0;
        }

        public void EnableVictoryImage()
        {
            int time = _timer.Time;

            int minutes = time / 60;
            int seconds = time - (minutes * 60);

            string minutesTxt = minutes <= 9 ? $"0{minutes}" : $"{minutes}";
            string secondsTxt = seconds <= 9 ? $"0{seconds}" : $"{seconds}";

            _victoryTimeTxt.text = $"{minutesTxt}:{secondsTxt}";
            _victoryKillsTxt.text = _statistics.KillsAmount.ToString();
            _victoryCoinsTxt.text = _statistics.CoinsAmount.ToString();
            _victoryImage.SetActive(true);
            Time.timeScale = 0;
        }

        public void ResetTime()
        {
            Time.timeScale = 1f;
        }
    }
}