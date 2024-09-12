using UnityEngine;
using TMPro;

namespace SurvivalChicken.Controllers
{
    public class GameOverView : MonoBehaviour
    {
        public static GameOverView Instance;

        [SerializeField] private TimerView _timer;

        [Header("Died Image")]
        [SerializeField] private GameObject _diedImage;
        [SerializeField] private TextMeshProUGUI _diedTimeTxt;

        [Header("Time Out Image")]
        [SerializeField] private GameObject _timeOutImage;
        [SerializeField] private TextMeshProUGUI _outTimeTxt;

        private void Awake()
        {
            Instance = this;
        }

        public void EnableDiedImage()
        {
            int time = _timer.Time;

            int minutes = time / 60;
            int seconds = time - (minutes * 60);

            string minutesTxt = minutes <= 9 ? $"0{minutes}" : $"{minutes}";
            string secondsTxt = seconds <= 9 ? $"0{seconds}" : $"{seconds}";

            _diedTimeTxt.text = $"Your time: \n{minutesTxt}:{secondsTxt}";
            _diedImage.SetActive(true);
            Time.timeScale = 0;
        }

        public void EnableTimeOutImage()
        {
            int time = _timer.Time;

            int minutes = time / 60;
            int seconds = time - (minutes * 60);

            string minutesTxt = minutes <= 9 ? $"0{minutes}" : $"{minutes}";
            string secondsTxt = seconds <= 9 ? $"0{seconds}" : $"{seconds}";

            _outTimeTxt.text = $"Your time: \n{minutesTxt}:{secondsTxt}";
            _timeOutImage.SetActive(true);
            Time.timeScale = 0;
        }

        public void ResetTime()
        {
            Time.timeScale = 1f;
        }
    }
}