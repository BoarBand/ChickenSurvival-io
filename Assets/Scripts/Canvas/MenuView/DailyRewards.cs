using UnityEngine;
using UnityEngine.UI;
using SurvivalChicken.SaveLoadDatas;
using System.Collections.Generic;
using SurvivalChicken.CollectItems;
using SurvivalChicken.Interfaces;
using SurvivalChicken.Controllers;
using SurvivalChicken.Structures;
using SurvivalChicken.ScriptableObjects.EquipmentsParameters;
using System;
using DG.Tweening;

namespace SurvivalChicken.DailyReward
{
    public class DailyRewards : MonoBehaviour
    {
        [SerializeField] private CollectedPanelView _collectedPanelView;
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private SortEquipmentItems _sortEquipmentItems;
        [SerializeField] private SaveLoadData _saveLoadData;
        [SerializeField] private Transform _root;
        [SerializeField] private Sprite _selectedFrameSprite;
        [SerializeField] private Sprite _frameSprite;
        [SerializeField] private ValuesView _valuesView;
        [SerializeField] private EquipmentParameters _lastDayEquipment;
        [SerializeField] private EquipmentContainer _equipmentContainer;
        [SerializeField] private Transform _tip;
        [SerializeField] private Image _claimButton;

        [SerializeField] private Image[] _frames;
        [SerializeField] private Image[] _completeTicks;

        [Header("Last Day Equipemnt")]
        [SerializeField] private Image _iconImg;
        [SerializeField] private Image _frameImg;
        [SerializeField] private Sprite _commonFrame;
        [SerializeField] private Sprite _rareFrame;
        [SerializeField] private Sprite _epicFrame;
        [SerializeField] private Sprite _legendaryFrame;

        [Header("CollectItems")]
        [SerializeField] private CollectItem _coinCollectItem;
        [SerializeField] private CollectItem _gemCollectItem;
        [SerializeField] private CollectItem _goldKeyCollectItem;
        [SerializeField] private CollectItem _equipmentCollectItem;

        private Dictionary<int, (CollectItem, int, Action)> _rewardsByDays = new Dictionary<int, (CollectItem, int, Action)>();

        private Tween _claimButtonTween;

        public void Initialize()
        {
            _root.gameObject.SetActive(true);

            if (CheckForDailyReward())
            {
                if(_saveLoadData.NextDayToGetDailyReward > 0)
                    _saveLoadData.DailyRewardID++;
                SetDateTimeForNextDailyReward();
            }

            if(_saveLoadData.ClaimedDailyRewards[_saveLoadData.DailyRewardID] == 1)
            {
                _tip.gameObject.SetActive(true);

                _root.gameObject.SetActive(false);

                if (_claimButtonTween != null)
                    _claimButtonTween.Kill();
            }
            else
            {
                _tip.gameObject.SetActive(false);

                if (_claimButtonTween != null)
                    _claimButtonTween.Kill();

                _claimButtonTween = DOTween.Sequence()
                    .Append(_claimButton.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.5f))
                    .Append(_claimButton.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f)).SetLoops(-1);
            }

            InitializeRewards();
            UpdateSelectedFrame(_saveLoadData.DailyRewardID);
            UpdateCompleteTicks(_saveLoadData.DailyRewardID);
            UpdateLastEquipemntItem();
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
            if (_saveLoadData.ClaimedDailyRewards.Length <= _saveLoadData.DailyRewardID)
                return;

            if (_saveLoadData.ClaimedDailyRewards[_saveLoadData.DailyRewardID] == 1)
                return;

            _collectedPanelView.Initialize(CreateCollectItem(_rewardsByDays[_saveLoadData.DailyRewardID].Item1,
                _rewardsByDays[_saveLoadData.DailyRewardID].Item2,
                _rewardsByDays[_saveLoadData.DailyRewardID].Item3, _saveLoadData.DailyRewardID == 6));

            _frames[_saveLoadData.DailyRewardID].sprite = _frameSprite;
            _completeTicks[_saveLoadData.DailyRewardID].gameObject.SetActive(true);
            _saveLoadData.ClaimedDailyRewards[_saveLoadData.DailyRewardID] = 1;
            _saveLoadData.SaveGame();

            _valuesView.UpdateAllValues();

            _tip.gameObject.SetActive(true);

            if (_claimButtonTween != null)
                _claimButtonTween.Kill();
        }

        private ICollect CreateCollectItem(CollectItem collectItem, int amount, Action collected, bool isEquipment = false)
        {
            CollectItem item = Instantiate(collectItem);
            if (isEquipment)
            {
                item.EquipmentRarity = _lastDayEquipment.EquipmentRarity;
                item.Icon = _lastDayEquipment.Icon;
            }
            item.Initialize(_saveLoadData, amount, collected);
            return item;
        }

        private void UpdateLastEquipemntItem()
        {
            _iconImg.sprite = _lastDayEquipment.Icon;
            _frameImg.sprite = _lastDayEquipment.EquipmentRarity switch
            {
                EquipmentRarities.EquipmentRarity.Common => _commonFrame,
                EquipmentRarities.EquipmentRarity.Rare => _rareFrame,
                EquipmentRarities.EquipmentRarity.Epic => _epicFrame,
                EquipmentRarities.EquipmentRarity.Legendary => _legendaryFrame,
                EquipmentRarities.EquipmentRarity.Default => _commonFrame,
                _ => throw new NotImplementedException()
            };
        }

        private void InitializeRewards()
        {
            _rewardsByDays.Add(0, (_coinCollectItem, 500, () => _saveLoadData.Coins += 500));
            _rewardsByDays.Add(1, (_gemCollectItem, 50, () => _saveLoadData.Gems += 50));
            _rewardsByDays.Add(2, (_goldKeyCollectItem, 1, () => _saveLoadData.GoldKeys += 1));
            _rewardsByDays.Add(3, (_gemCollectItem, 150, () => _saveLoadData.Gems += 150));
            _rewardsByDays.Add(4, (_coinCollectItem, 3000, () => _saveLoadData.Coins += 3000));
            _rewardsByDays.Add(5, (_goldKeyCollectItem, 5, () => _saveLoadData.GoldKeys += 5));
            _rewardsByDays.Add(6, (_equipmentCollectItem, 1, () => 
            {
                _equipmentContainer.AddItem(_lastDayEquipment);
                _inventoryView.CreateItemView(_lastDayEquipment);
                _sortEquipmentItems.SortBySelected();
            }
            ));
        }

        private bool CheckForDailyReward()
        {
            int day = GetTime();
            int nextDay = _saveLoadData.NextDayToGetDailyReward;

            if (nextDay == 0)
                return true;

            if (day >= nextDay)
                return true;

            return false;
        }

        private void SetDateTimeForNextDailyReward()
        {
            _saveLoadData.CurrentDayToGetDailyReward = GetTime();
            _saveLoadData.NextDayToGetDailyReward = GetTime() + 1;
            _saveLoadData.SaveGame();
        }

        private int GetTime()
        {
            return DateTime.Now.Day * DateTime.Now.Month * DateTime.Now.Year;
        }

        private void OnDisable()
        {
            if (_claimButtonTween != null)
                _claimButtonTween.Kill();
        }
    }
}
