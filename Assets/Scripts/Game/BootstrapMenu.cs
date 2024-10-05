using UnityEngine;
using SurvivalChicken.Controllers;
using SurvivalChicken.SaveLoadDatas;

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

        private void Awake()
        {
            Time.timeScale = 1f;

            _saveLoadData.Initialize();
            _worldSwitcher.Initialize();
            _worldGifts.Initialize();
            _valuesView.Initialize();
            _menuWindowsSwitcher.Initialize();
            _inventoryView.Initalize();
        }
    }
}
