using UnityEngine;
using SurvivalChicken.Controllers;
using SurvivalChicken.SaveLoadDatas;
using SurvivalChicken.DailyReward;

namespace SurvivalChicken.Bootstrap
{
    public class BootstrapMenu : MonoBehaviour
    {
        [SerializeField] private WorldsSwitcher _worldSwitcher;
        [SerializeField] private WorldGifts _worldGifts;
        [SerializeField] private ValuesView _valuesView;
        [SerializeField] private MenuWindowsSwitcher _menuWindowsSwitcher;
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private SaveLoadData _saveLoadData;
        [SerializeField] private SortEquipmentItems _sortEquipmentItems;
        [SerializeField] private DailyRewards _dailyRewards;
        [SerializeField] private SettingsView _settingsView;

        private void Awake()
        {
            Time.timeScale = 1f;

            _saveLoadData.Initialize();
            _valuesView.Initialize();
            _worldGifts.Initialize();
            _worldSwitcher.Initialize();
            _menuWindowsSwitcher.Initialize();
            _dailyRewards.Initialize();
            _inventoryView.Initalize();
            _sortEquipmentItems.Sort(SortEquipmentItems.SortType.Rarity);
            _settingsView.Initialize();
        }
    }
}
