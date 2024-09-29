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
        [SerializeField] private Image[] _giftImgs;

        private SaveLoadData _saveLoadData = new SaveLoadData();

        private readonly int TimeToFirstGift = 60;
        private readonly int TimeToSecondGift = 300;
        private readonly int TimeToThirdGift = 900;

        public void Initialize()
        {
            if(_saveLoadData.TryGetWorldTime(0, out int value))   
                CheckToSetSprites(value);
        }

        private void CheckToSetSprites(int time)
        {
            _giftImgs[0].sprite = time >= TimeToFirstGift ? _openGift : _closeGift;
            _giftImgs[1].sprite = time >= TimeToSecondGift ? _openGift : _closeGift;
            _giftImgs[2].sprite = time >= TimeToThirdGift ? _openGift : _closeGift;
        }
    }
}