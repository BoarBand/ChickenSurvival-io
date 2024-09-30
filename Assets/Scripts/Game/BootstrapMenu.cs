using UnityEngine;
using SurvivalChicken.Controllers;

namespace SurvivalChicken.Bootstrap
{
    public class BootstrapMenu : MonoBehaviour
    {
        [SerializeField] private WorldsSwitcher _worldSwitcher;
        [SerializeField] private WorldGifts _worldGifts;
        [SerializeField] private ValuesView _valuesView;

        private void Awake()
        {
            Time.timeScale = 1f;

            _worldSwitcher.Initialize();
            _worldGifts.Initialize();
            _valuesView.Initialize();
        }
    }
}
