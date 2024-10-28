using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SurvivalChicken.SaveLoadDatas;
using UnityEngine.Audio;

namespace SurvivalChicken.Controllers
{
    public sealed class SettingsView : MonoBehaviour
    {
        [SerializeField] private Image _soundImg;
        [SerializeField] private Image _musicImg;

        [SerializeField] private Sprite _onButtonSprite;
        [SerializeField] private Sprite _offButtonSprite;

        [SerializeField] private TextMeshProUGUI _soundTxt;
        [SerializeField] private TextMeshProUGUI _musicTxt;

        [SerializeField] private string _onText;
        [SerializeField] private string _offText;

        [SerializeField] private AudioMixerGroup _backgroundMusicGroup;
        [SerializeField] private AudioMixerGroup _soundsGroup;
        [SerializeField] private AudioMixer _audioMixer;

        [SerializeField] private SaveLoadData _saveLoadData;

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            UpdateSound();
            UpdateMusicSound();
        }

        public void OnChangeMusicSettings()
        {
            if (_saveLoadData.MusicMute == 0)
            {
                _saveLoadData.MusicMute = 1;

                UpdateMusicSound();
                _saveLoadData.SaveGame();
                return;
            }

            if (_saveLoadData.MusicMute == 1)
                _saveLoadData.MusicMute = 0;

            UpdateMusicSound();
            _saveLoadData.SaveGame();
        }

        public void OnChangeSoundsSettings()
        {
            if (_saveLoadData.SoundMute == 0)
            {
                _saveLoadData.SoundMute = 1;

                UpdateSound();
                _saveLoadData.SaveGame();
                return;
            }

            if (_saveLoadData.SoundMute == 1)
                _saveLoadData.SoundMute = 0;

            UpdateSound();
            _saveLoadData.SaveGame();
        }

        private void UpdateSound()
        {
            if(_saveLoadData.SoundMute == 1)
            {
                _soundsGroup.audioMixer.SetFloat("SoundsVolume", -80f);
                _soundImg.sprite = _offButtonSprite;
                _soundTxt.text = _offText;
                return;
            }

            if (_saveLoadData.SoundMute == 0)
            {
                _soundsGroup.audioMixer.SetFloat("SoundsVolume", 0f);
                _soundImg.sprite = _onButtonSprite;
                _soundTxt.text = _onText;
            }
        }

        private void UpdateMusicSound()
        {
            if (_saveLoadData.MusicMute == 1)
            {
                _backgroundMusicGroup.audioMixer.SetFloat("BackgroundVolume", -80f);
                _musicImg.sprite = _offButtonSprite;
                _musicTxt.text = _offText;
                return;
            }

            if (_saveLoadData.MusicMute == 0)
            {
                _backgroundMusicGroup.audioMixer.SetFloat("BackgroundVolume", 0f);
                _musicImg.sprite = _onButtonSprite;
                _musicTxt.text = _onText;
            }
        }
    }
}
