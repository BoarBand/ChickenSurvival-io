using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SurvivalChicken.Controllers
{
    public class WorldsSwitcher : MonoBehaviour
    {
        [SerializeField] private string[] _worldTitles;
        [SerializeField] private Sprite[] _worldSprites;
        [SerializeField] private Sprite[] _worldFrames;
        [SerializeField] private Sprite[] _bossSprites;
        [SerializeField] private Image _worldImg;
        [SerializeField] private TextMeshProUGUI _titleWorldTxt;
        [SerializeField] private Image _bossImg;
        [SerializeField] private Image _worldFrameImg;
        [SerializeField] private Button _prevWorldButton;
        [SerializeField] private Button _nextWorldButton;
        [SerializeField] private Button _playButton;

        private int _currentSelectedWorld;

        public void Initialize()
        {
            CheckToSwitchButtons(_currentSelectedWorld);
            UpdateView(_currentSelectedWorld);
        }

        public void NextWorld()
        {
            _currentSelectedWorld++;

            if (_currentSelectedWorld > _worldTitles.Length)
                _currentSelectedWorld = _worldTitles.Length - 1;

            CheckToSwitchButtons(_currentSelectedWorld);
            UpdateView(_currentSelectedWorld);
        }

        public void PrevWorld()
        {
            _currentSelectedWorld--;

            if (_currentSelectedWorld < 0)
                _currentSelectedWorld = 0;

            CheckToSwitchButtons(_currentSelectedWorld);
            UpdateView(_currentSelectedWorld);
        }

        private void CheckToSwitchButtons(int worldNum)
        {
            if (worldNum <= 0)
                _prevWorldButton.gameObject.SetActive(false);
            else if (worldNum > 0)
                _prevWorldButton.gameObject.SetActive(true);

            if (worldNum + 1 >= _worldTitles.Length)
                _nextWorldButton.gameObject.SetActive(false);
            else if (worldNum < _worldTitles.Length)
                _nextWorldButton.gameObject.SetActive(true);
        }

        private void UpdateView(int index)
        {
            _titleWorldTxt.text = _worldTitles[index];
            _worldImg.sprite = _worldSprites[index];
            _worldFrameImg.sprite = _worldFrames[index];
            _bossImg.sprite = _bossSprites[index];
        }
    }
}
