using UnityEngine;
using SurvivalChicken.CollectItems.BoostItems;

namespace SurvivalChicken.Spawner
{
    public sealed class BoostItemsSpawner : MonoBehaviour
    {
        public static BoostItemsSpawner Instance;

        [SerializeField] private BoostItem[] _boostItems;

        [Header("Fortune Wheel")]
        [SerializeField] private FortuneWheelBoostItem _fortuneWheelBoost;
        [SerializeField] private FortuneWheel.FortuneWheelView _fortuneWheelView;

        private readonly float ScreenWidthOffset = 100f;
        private readonly float ScreenHeightOffset = 100f;

        public void Initialize()
        {
            Instance = this;
        }

        public void CreateRandomBoostItem()
        {
            BoostItem boostItem = Instantiate(_boostItems[Random.Range(0, _boostItems.Length)]);
            boostItem.Initialize(Camera.main.ScreenToWorldPoint(new Vector3(
                        Random.Range(-ScreenWidthOffset, Screen.width + ScreenWidthOffset),
                        Random.Range(-ScreenHeightOffset, Screen.height + ScreenHeightOffset),
                        10f)), Quaternion.identity);
        }

        public void CreateRandomBoostItemInPoint(Vector3 pos)
        {
            BoostItem boostItem = Instantiate(_boostItems[Random.Range(0, _boostItems.Length)]);
            boostItem.Initialize(pos, Quaternion.identity, () => {
                _fortuneWheelView.Initialize();
            });
        }

        public void CreateFortuneWheel(Vector3 pos)
        {
            BoostItem boostItem = Instantiate(_fortuneWheelBoost);
            boostItem.Initialize(pos, Quaternion.identity, () => {
                _fortuneWheelView.Initialize();
            });
        }
    }
}
