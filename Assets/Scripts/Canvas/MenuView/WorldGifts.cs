using System;
using UnityEngine;
using UnityEngine.UI;
using SurvivalChicken.SaveLoadDatas;
using SurvivalChicken.CollectItems;
using SurvivalChicken.Interfaces;

namespace SurvivalChicken.Controllers
{
    public sealed class WorldGifts : MonoBehaviour
    {
        [SerializeField] private CollectedPanelView _collectedPanelView;
        [SerializeField] private ValuesView _valuesView;
        [SerializeField] private Image[] _giftImgs;
        [SerializeField] private CollectItem _coinCollectItems;
        [SerializeField] private CollectItem _gemCollectItems;
        [SerializeField] private WorldsSwitcher _worldSwitcher;
        [SerializeField] private Sprite _openGift;
        [SerializeField] private Sprite _closeGift;
        [SerializeField] private SaveLoadData _saveLoadData;
        [SerializeField] private Image[] _giftRays;
        [SerializeField] private WorldsSwitcher _worldsSwitcher;

        private readonly int TimeToFirstGift = 60;
        private readonly int TimeToSecondGift = 300;
        private readonly int TimeToThirdGift = 900;

        public void Initialize()
        {
            UpdateView(_saveLoadData.StagePlayTimes[_worldsSwitcher.CurrentSelectedWorld]);
        }

        public void GetWorldGifts()
        {
            int worldNum = _worldsSwitcher.CurrentSelectedWorld;

            int coins;
            int gems;

            if (!_giftRays[0].gameObject.activeInHierarchy && !_giftRays[1].gameObject.activeInHierarchy && !_giftRays[2].gameObject.activeInHierarchy)
                return;

            if (_saveLoadData.StagePlayTimes[worldNum] > TimeToThirdGift)
            {
                coins = 300;
                gems = 30;

                _collectedPanelView.Initialize(CreateCollectItem(_coinCollectItems, coins, () => _saveLoadData.Coins += coins), 
                    CreateCollectItem(_gemCollectItems, gems, () => { _saveLoadData.Gems += gems; }));
                _saveLoadData.OpenedWorldGifts[worldNum, 2] = 1;
                _saveLoadData.OpenedWorldGifts[worldNum, 1] = 1;
                _saveLoadData.OpenedWorldGifts[worldNum, 0] = 1;
            }

            if (_saveLoadData.StagePlayTimes[worldNum] > TimeToSecondGift && _saveLoadData.StagePlayTimes[worldNum] < TimeToThirdGift)
            {
                coins = 200;
                gems = 20;

                _collectedPanelView.Initialize(CreateCollectItem(_coinCollectItems, coins, () => _saveLoadData.Coins += coins),
                    CreateCollectItem(_gemCollectItems, gems, () => _saveLoadData.Gems += gems));
                _saveLoadData.OpenedWorldGifts[worldNum, 1] = 1;
                _saveLoadData.OpenedWorldGifts[worldNum, 0] = 1;
            }

            if (_saveLoadData.StagePlayTimes[worldNum] > TimeToFirstGift && _saveLoadData.StagePlayTimes[worldNum] < TimeToSecondGift)
            {
                coins = 100;
                gems = 10;

                _collectedPanelView.Initialize(CreateCollectItem(_coinCollectItems, coins, () => _saveLoadData.Coins += coins), 
                    CreateCollectItem(_gemCollectItems, gems, () => _saveLoadData.Gems += gems));
                _saveLoadData.OpenedWorldGifts[worldNum, 0] = 1;
            }

            _saveLoadData.SaveGame();
            _valuesView.UpdateAllValues();
            UpdateView(_saveLoadData.StagePlayTimes[worldNum]);
        }

        private ICollect CreateCollectItem(CollectItem collectItem, int amount, Action collected)
        {
            CollectItem item = Instantiate(collectItem);
            item.Initialize(_saveLoadData, amount, collected);
            return item;
        }

        private void UpdateView(int time)
        {
            CheckToDisplayRays(time);
            CheckToSetSprites();
        }

        private void CheckToSetSprites()
        {
            _giftImgs[0].sprite = _saveLoadData.OpenedWorldGifts[_worldsSwitcher.CurrentSelectedWorld, 0] == 1 ? _openGift : _closeGift;
            _giftImgs[1].sprite = _saveLoadData.OpenedWorldGifts[_worldsSwitcher.CurrentSelectedWorld, 1] == 1 ? _openGift : _closeGift;
            _giftImgs[2].sprite = _saveLoadData.OpenedWorldGifts[_worldsSwitcher.CurrentSelectedWorld, 2] == 1 ? _openGift : _closeGift;
        }

        private void CheckToDisplayRays(int time)
        {
            _giftRays[0].gameObject.SetActive(time >= TimeToFirstGift &&
                _saveLoadData.OpenedWorldGifts[_worldsSwitcher.CurrentSelectedWorld, 0] == 0 ? true : false);
            _giftRays[1].gameObject.SetActive(time >= TimeToSecondGift && 
                _saveLoadData.OpenedWorldGifts[_worldsSwitcher.CurrentSelectedWorld, 1] == 0 ? true : false);
            _giftRays[2].gameObject.SetActive(time >= TimeToThirdGift && 
                _saveLoadData.OpenedWorldGifts[_worldsSwitcher.CurrentSelectedWorld, 2] == 0 ? true : false);
        }
    }
}