using SurvivalChicken.SaveLoadDatas;

namespace SurvivalChicken.CollectItems
{
    public class GemsCollectItem : CollectItem
    {
        public override void Initialize(SaveLoadData saveLoadData, int amount)
        {
            base.Initialize(saveLoadData, amount);

            SaveLoadData.Gems += Amount;
            SaveLoadData.SaveGame();
            Destroy(gameObject);
        }
    }
}