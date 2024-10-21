using UnityEngine;
using SurvivalChicken.Controllers;
using SurvivalChicken.SaveLoadDatas;

namespace SurvivalChicken.Bootstrap
{
    public class BootstrapLoading : MonoBehaviour
    {
        [SerializeField] private FirstSceneInit _firstSceneInit;
        [SerializeField] private SaveLoadData _saveLoadData;

        public void Awake()
        {
            _saveLoadData.Initialize();
            _firstSceneInit.Initialize();
        }
    }
}