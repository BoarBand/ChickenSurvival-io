using SurvivalChicken.SaveLoadDatas;

namespace SurvivalChicken.CollectItems
{
    public class CoinCollectItem : CollectItem
    {
        public override void Initialize(SaveLoadData saveLoadData, int amount)
        {
            base.Initialize(saveLoadData, amount);

            SaveLoadData.Coins += Amount;
            SaveLoadData.SaveGame();
            Destroy(gameObject);
        }
    }
}