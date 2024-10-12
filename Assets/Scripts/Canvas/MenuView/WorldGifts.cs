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
        [SerializeField] private CollectItem[] _collectItems;
        [SerializeField] private WorldsSwitcher _worldSwitcher;
        [SerializeField] private Sprite _openGift;
        [SerializeField] private Sprite _closeGift;
        [SerializeField] private SaveLoadData _saveLoadData;
        [SerializeField] private Image[] _giftRays;

        private readonly int TimeToFirstGift = 60;
        private readonly int TimeToSecondGift = 300;
        private readonly int TimeToThirdGift = 900;

        public void Initialize()
        {
            UpdateView(_saveLoadData.WorldTimes[0]);
        }

        public void GetWorldGifts()
        {
            int worldNum = 0;

            if (!_giftRays[0].gameObject.activeInHierarchy && !_giftRays[1].gameObject.activeInHierarchy && !_giftRays[2].gameObject.activeInHierarchy)
                return;

            if (_saveLoadData.WorldTimes[worldNum] > TimeToThirdGift)
            {
                _collectedPanelView.Initialize(CreateCollectItem(_collectItems[0], 300), CreateCollectItem(_collectItems[1], 30));
                _saveLoadData.OpenedWorldGifts[worldNum, 2] = 1;
                _saveLoadData.OpenedWorldGifts[worldNum, 1] = 1;
                _saveLoadData.OpenedWorldGifts[worldNum, 0] = 1;
            }

            if (_saveLoadData.WorldTimes[worldNum] > TimeToSecondGift && _saveLoadData.WorldTimes[worldNum] < TimeToThirdGift)
            {
                _collectedPanelView.Initialize(CreateCollectItem(_collectItems[0], 200), CreateCollectItem(_collectItems[1], 20));
                _saveLoadData.OpenedWorldGifts[worldNum, 1] = 1;
                _saveLoadData.OpenedWorldGifts[worldNum, 0] = 1;
            }

            if (_saveLoadData.WorldTimes[worldNum] > TimeToFirstGift && _saveLoadData.WorldTimes[worldNum] < TimeToSecondGift)
            {
                _collectedPanelView.Initialize(CreateCollectItem(_collectItems[0], 100), CreateCollectItem(_collectItems[1], 10));
                _saveLoadData.OpenedWorldGifts[worldNum, 0] = 1;
            }

            _saveLoadData.SaveGame();
            _valuesView.UpdateAllValues();
            UpdateView(_saveLoadData.WorldTimes[worldNum]);
        }

        private ICollect CreateCollectItem(CollectItem collectItem, int amount)
        {
            CollectItem item = Instantiate(collectItem);
            item.Initialize(_saveLoadData, amount);
            return item;
        }

        private void UpdateView(int time)
        {
            CheckToDisplayRays(time);
            CheckToSetSprites(time);
        }

        private void CheckToSetSprites(int time)
        {
            _giftImgs[0].sprite = _saveLoadData.OpenedWorldGifts[0, 0] == 1 ? _openGift : _closeGift;
            _giftImgs[1].sprite = _saveLoadData.OpenedWorldGifts[0, 1] == 1 ? _openGift : _closeGift;
            _giftImgs[2].sprite = _saveLoadData.OpenedWorldGifts[0, 2] == 1 ? _openGift : _closeGift;
        }

        private void CheckToDisplayRays(int time)
        {
            _giftRays[0].gameObject.SetActive(time >= TimeToFirstGift && _saveLoadData.OpenedWorldGifts[0, 0] == 0 ? true : false);
            _giftRays[1].gameObject.SetActive(time >= TimeToSecondGift && _saveLoadData.OpenedWorldGifts[0, 1] == 0 ? true : false);
            _giftRays[2].gameObject.SetActive(time >= TimeToThirdGift && _saveLoadData.OpenedWorldGifts[0, 2] == 0 ? true : false);
        }
    }
}