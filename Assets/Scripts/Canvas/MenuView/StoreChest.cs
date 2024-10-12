using UnityEngine;
using SurvivalChicken.SaveLoadDatas;

namespace SurvivalChicken.Controllers
{
    public sealed class StoreChest : MonoBehaviour
    {
        [SerializeField] private int _chanceToGetCommonItem;
        [SerializeField] private int _chanceToGetRareItem;
        [SerializeField] private int _chanceToGetEpicItem;
        [SerializeField] private int _chanceToGetLegendaryItem;

        [SerializeField] private OpenChest _openChest;
        [SerializeField] private Sprite _chestSprite;
        [SerializeField] private ValuesView _valuesView;
        [SerializeField] private SaveLoadData _saveLoadData;

        [Header("Costs")]
        [SerializeField] private int _gemsCost;
        [SerializeField] private int _coinsCost;

        public void TryInvokeChestForCoins()
        {
            if (_saveLoadData.Coins < _coinsCost)
                return;

            _saveLoadData.Coins -= _coinsCost;
            _saveLoadData.SaveGame();

            _valuesView.UpdateAllValues();

            _openChest.Initialize(_chestSprite, 
                _chanceToGetCommonItem, 
                _chanceToGetRareItem,
                _chanceToGetEpicItem,
                _chanceToGetLegendaryItem);
        }

        public void TryInvokeChestForGems()
        {
            if (_saveLoadData.Gems < _gemsCost)
                return;

            _saveLoadData.Gems -= _gemsCost;
            _saveLoadData.SaveGame();

            _valuesView.UpdateAllValues();

            _openChest.Initialize(_chestSprite,
                _chanceToGetCommonItem,
                _chanceToGetRareItem,
                _chanceToGetEpicItem,
                _chanceToGetLegendaryItem);
        }
    }
}
