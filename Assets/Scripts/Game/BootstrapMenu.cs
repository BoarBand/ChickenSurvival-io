using UnityEngine;
using SurvivalChicken.Controllers;

namespace SurvivalChicken.Bootstrap
{
    public class BootstrapMenu : MonoBehaviour
    {
        [SerializeField] private WorldsSwitcher _worldSwitcher;

        private void Awake()
        {
            _worldSwitcher.Initialize();
        }
    }
}
