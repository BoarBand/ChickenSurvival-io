using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SurvivalChicken.Controllers
{
    public class ProgressBarAbilities : MonoBehaviour
    {
        public static ProgressBarAbilities Instance;

        [SerializeField] private AbilitiesSelector _selector;
        [SerializeField] private Slider _progressBarAbility;
        [SerializeField] private TextMeshProUGUI _levelTxt;

        private readonly float IncreaseValueStepPerLevel = 100f;
        private readonly float InitMaxValue = 100f;
        private readonly float IncreaseProgressBarValueDuraction = 4f;

        private float _maxValue;
        private int _level = 1;

        private int _currentValue = 0;

        private Coroutine _inreaseValueCoroutine;

        public void Initialize()
        {
            Instance = this;

            SetMaxProgressBarValue(_level);
        }

        public void IncreaseProgressBar(int value)
        {
            _currentValue += value;

            if (_progressBarAbility.value >= _progressBarAbility.maxValue)
            {
                _selector.Initialize();

                _level++;
                SetMaxProgressBarValue(_level);

                SetText($"Lv.{_level}");
            }

            if (_inreaseValueCoroutine != null)
                StopCoroutine(_inreaseValueCoroutine);
            _inreaseValueCoroutine = StartCoroutine(IncreaseProgressBarValue());
        }

        private void SetMaxProgressBarValue(int level)
        {
            _maxValue = _currentValue + InitMaxValue + (IncreaseValueStepPerLevel * (level - 1) * 0.75f);

            _progressBarAbility.minValue = _currentValue;
            _progressBarAbility.maxValue = _maxValue;

            _progressBarAbility.value = _currentValue;
        }

        private void SetText(string txt)
        {
            _levelTxt.text = txt;
        }

        #region Coroutines
        private IEnumerator IncreaseProgressBarValue()
        {
            var waitForEndFrame = new WaitForFixedUpdate();

            float duraction = 0f;

            while(_progressBarAbility.value <= _currentValue)
            {
                duraction += Time.fixedDeltaTime * IncreaseProgressBarValueDuraction;

                //_progressBarAbility.value = Mathf.MoveTowards(_progressBarAbility.value, _currentValue, duraction);

                _progressBarAbility.value = _currentValue;

                if (duraction >= 1f)
                    duraction = 0f;

                yield return waitForEndFrame;
            }

            _inreaseValueCoroutine = null;
        }
        #endregion
    }
}