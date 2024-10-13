using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SurvivalChicken.SceneLoader;
using SurvivalChicken.SaveLoadDatas;

namespace SurvivalChicken.Controllers
{
    public sealed class WorldsSwitcher : MonoBehaviour
    {
        [SerializeField] private string[] _worldTitles;
        [SerializeField] private Sprite[] _worldSprites;
        [SerializeField] private Sprite[] _worldFrames;
        [SerializeField] private Sprite[] _bossSprites;
        [SerializeField] private int[] _sceneIds;
        [SerializeField] private Image _worldImg;
        [SerializeField] private TextMeshProUGUI _titleWorldTxt;
        [SerializeField] private Image _bossImg;
        [SerializeField] private Image _worldFrameImg;
        [SerializeField] private Button _prevWorldButton;
        [SerializeField] private Button _nextWorldButton;
        [SerializeField] private Button _playButton;
        [SerializeField] private TextMeshProUGUI _playButtonTxt;
        [SerializeField] private LoadScreen _loadScreen;
        [SerializeField] private SaveLoadData _saveLoadData;

        [Header("Unlock & Lock Settings")]
        [SerializeField] private Sprite _unlockPlayButtonSprite;
        [SerializeField] private Sprite _lockPlayButtonSprite;
        [SerializeField] private string _lockPlayButtonTxt;
        [SerializeField] private string _unlockPlayButtonTxt;

        public int CurrentSelectedWorld { get; private set; }

        public void Initialize()
        {
            CheckToSwitchButtons(CurrentSelectedWorld);
            UpdateView(CurrentSelectedWorld);
            print(_saveLoadData);
            print(_saveLoadData.LockedWorlds);
            UpdatePlayButtonView(_saveLoadData.LockedWorlds[CurrentSelectedWorld]);
        }

        public void NextWorld()
        {
            CurrentSelectedWorld++;

            if (CurrentSelectedWorld > _worldTitles.Length)
                CurrentSelectedWorld = _worldTitles.Length - 1;

            CheckToSwitchButtons(CurrentSelectedWorld);
            UpdateView(CurrentSelectedWorld);
            UpdatePlayButtonView(_saveLoadData.LockedWorlds[CurrentSelectedWorld]);
        }

        public void PrevWorld()
        {
            CurrentSelectedWorld--;

            if (CurrentSelectedWorld < 0)
                CurrentSelectedWorld = 0;

            CheckToSwitchButtons(CurrentSelectedWorld);
            UpdateView(CurrentSelectedWorld);
            UpdatePlayButtonView(_saveLoadData.LockedWorlds[CurrentSelectedWorld]);
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

        public void PlayButton()
        {
            if (_saveLoadData.LockedWorlds[CurrentSelectedWorld] == 1)
                return;

            _loadScreen.Initialize(_sceneIds[CurrentSelectedWorld]);
        }

        private void UpdatePlayButtonView(int isLock)
        {
            if (isLock == 1)
            {
                _playButton.image.sprite = _lockPlayButtonSprite;
                _playButtonTxt.text = _lockPlayButtonTxt;
                return;
            }

            _playButton.image.sprite = _unlockPlayButtonSprite;
            _playButtonTxt.text = _unlockPlayButtonTxt;
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
