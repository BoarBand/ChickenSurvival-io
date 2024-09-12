using System.Collections;
using UnityEngine;
using TMPro;

namespace SurvivalChicken.Controllers
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerTxt;

        private int _time;
        private float _timerUpdate = 1f;
        private bool _isTimer;

        private Coroutine _timerCoroutine;

        public int Time => _time;

        public void Initialize()
        {
            _isTimer = true;
            _time = 0;
            UpdateTextView(_time);
        }

        private void FixedUpdate()
        {
            if (_timerCoroutine == null && _isTimer)
                _timerCoroutine = StartCoroutine(Timer());
        }

        private IEnumerator Timer()
        {
            _time++;
            UpdateTextView(_time);
            yield return new WaitForSeconds(_timerUpdate);
            _timerCoroutine = null;
        }

        private void UpdateTextView(int time)
        {
            int minutes = time / 60;
            int seconds = time - (minutes * 60);

            string minutesTxt = minutes <= 9 ? $"0{minutes}" : $"{minutes}";
            string secondsTxt = seconds <= 9 ? $"0{seconds}" : $"{seconds}";

            _timerTxt.text = $"{minutesTxt}:{secondsTxt}";
        }

        public void StopTimer()
        {
            _isTimer = false;
        }

        public void StartTimer()
        {
            _isTimer = true;
        }
    }
}
