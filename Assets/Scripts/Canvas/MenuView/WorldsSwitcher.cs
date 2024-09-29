using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SurvivalChicken.Controllers
{
    public sealed class WorldsSwitcher : MonoBehaviour
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

        public int CurrentSelectedWorld { get; private set; }

        public void Initialize()
        {
            CheckToSwitchButtons(CurrentSelectedWorld);
            UpdateView(CurrentSelectedWorld);
        }

        public void NextWorld()
        {
            CurrentSelectedWorld++;

            if (CurrentSelectedWorld > _worldTitles.Length)
                CurrentSelectedWorld = _worldTitles.Length - 1;

            CheckToSwitchButtons(CurrentSelectedWorld);
            UpdateView(CurrentSelectedWorld);
        }

        public void PrevWorld()
        {
            CurrentSelectedWorld--;

            if (CurrentSelectedWorld < 0)
                CurrentSelectedWorld = 0;

            CheckToSwitchButtons(CurrentSelectedWorld);
            UpdateView(CurrentSelectedWorld);
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
