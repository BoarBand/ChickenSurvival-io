using UnityEngine;
using UnityEngine.UI;
using SurvivalChicken.SaveLoadDatas;

namespace SurvivalChicken.Controllers
{
    public sealed class WorldGifts : MonoBehaviour
    {
        [SerializeField] private WorldsSwitcher _worldSwitcher;
        [SerializeField] private Sprite _openGift;
        [SerializeField] private Sprite _closeGift;
        [SerializeField] private SaveLoadData _saveLoadData;
        [SerializeField] private Image[] _giftImgs;

        private readonly int TimeToFirstGift = 60;
        private readonly int TimeToSecondGift = 300;
        private readonly int TimeToThirdGift = 900;

        public void Initialize()
        {
            CheckToSetSprites(_saveLoadData.WorldTimes[0]);
        }

        private void CheckToSetSprites(int time)
        {
            _giftImgs[0].sprite = time >= TimeToFirstGift ? _openGift : _closeGift;
            _giftImgs[1].sprite = time >= TimeToSecondGift ? _openGift : _closeGift;
            _giftImgs[2].sprite = time >= TimeToThirdGift ? _openGift : _closeGift;
        }
    }
}