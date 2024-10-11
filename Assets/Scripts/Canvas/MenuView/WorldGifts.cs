using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using SurvivalChicken.SaveLoadDatas;
using SurvivalChicken.CollectItems;
using SurvivalChicken.Interfaces;

namespace SurvivalChicken.Controllers
{
    public sealed class WorldGifts : MonoBehaviour
    {
        [SerializeField] private WorldsSwitcher _worldSwitcher;
        [SerializeField] private Sprite _openGift;
        [SerializeField] private Sprite _closeGift;
        [SerializeField] private SaveLoadData _saveLoadData;
        [SerializeField] private CollectedPanelView _collectedPanelView;
        [SerializeField] private ValuesView _valuesView;
        [SerializeField] private Image[] _giftImgs;
        [SerializeField] private CollectItem[] _collectItems;

        private readonly int TimeToFirstGift = 60;
        private readonly int TimeToSecondGift = 300;
        private readonly int TimeToThirdGift = 900;

        public void Initialize()
        {
            CheckToSetSprites(_saveLoadData.WorldTimes[0]);
        }

        public void GetWorldGifts()
        {

            _collectedPanelView.Initialize(CreateCollectItem(_collectItems[0], 1000), CreateCollectItem(_collectItems[1], 200));
            _saveLoadData.SaveGame();
            _valuesView.UpdateAllValues();
        }

        private ICollect CreateCollectItem(CollectItem collectItem, int amount)
        {
            CollectItem item = Instantiate(collectItem);
            item.Initialize(_saveLoadData, amount);
            return item;
        }

        private void CheckToSetSprites(int time)
        {
            _giftImgs[0].sprite = time >= TimeToFirstGift ? _openGift : _closeGift;
            _giftImgs[1].sprite = time >= TimeToSecondGift ? _openGift : _closeGift;
            _giftImgs[2].sprite = time >= TimeToThirdGift ? _openGift : _closeGift;
        }
    }
}