using UnityEngine;
using UnityEngine.UI;
using SurvivalChicken.SaveLoadDatas;
using System.Collections.Generic;
using System;

namespace SurvivalChicken.DailyReward
{
    public class DailyRewards : MonoBehaviour
    {
        [SerializeField] private SaveLoadData _saveLoadData;
        [SerializeField] private Transform _root;
        [SerializeField] private Sprite _selectedFrameSprite;
        [SerializeField] private Sprite _frameSprite;

        [SerializeField] private Image[] _frames;
        [SerializeField] private Image[] _completeTicks;

        private Dictionary<int, Action> _rewardsByDays = new Dictionary<int, Action>();

        public void Initialize()
        {
            InitializeRewards();
            UpdateSelectedFrame(_saveLoadData.DailyRewardID);
            UpdateCompleteTicks(_saveLoadData.DailyRewardID);
        }

        private void UpdateSelectedFrame(int day)
        {
            for(int i = 0; i < _frames.Length; i++)
            {
                if (i == day && _saveLoadData.ClaimedDailyRewards[i] == 0)
                {
                    _frames[i].sprite = _selectedFrameSprite;
                    continue;
                }

                _frames[i].sprite = _frameSprite;
            }
        }

        private void UpdateCompleteTicks(int day)
        {
            for(int i = 0; i < _completeTicks.Length; i++)
            {
                if(_saveLoadData.ClaimedDailyRewards[i] == 1)
                    _completeTicks[i].gameObject.SetActive(true);
            }
        }

        public void Claim()
        {
            if (_saveLoadData.ClaimedDailyRewards[_saveLoadData.DailyRewardID] == 1)
                return;

            _frames[_saveLoadData.DailyRewardID].sprite = _frameSprite;
            _completeTicks[_saveLoadData.DailyRewardID].gameObject.SetActive(true);
            _saveLoadData.ClaimedDailyRewards[_saveLoadData.DailyRewardID] = 1;
            _saveLoadData.SaveGame();
        }

        private void InitializeRewards()
        {
            _rewardsByDays.Add(0, () => {
                _saveLoadData.Coins += 500;
                _saveLoadData.SaveGame();
            });

            _rewardsByDays.Add(1, () => {
                _saveLoadData.Gems += 50;
                _saveLoadData.SaveGame();
            });

            _rewardsByDays.Add(2, () => {
                _saveLoadData.GoldKeys += 1;
                _saveLoadData.SaveGame();
            });

            _rewardsByDays.Add(3, () => {
                _saveLoadData.Gems += 150;
                _saveLoadData.SaveGame();
            });

            _rewardsByDays.Add(4, () => {
                _saveLoadData.Coins += 3000;
                _saveLoadData.SaveGame();
            });

            _rewardsByDays.Add(5, () => {
                _saveLoadData.GoldKeys += 5;
                _saveLoadData.SaveGame();
            });
        }
    }
}
