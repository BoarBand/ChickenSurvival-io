using UnityEngine;
using SurvivalChicken.SceneLoader;
using SurvivalChicken.SaveLoadDatas;
using SurvivalChicken.Tutorials;

namespace SurvivalChicken.Controllers
{
    public class FirstSceneInit : MonoBehaviour
    {
        [SerializeField] private SaveLoadData _saveLoadData;
        [SerializeField] private LoadScreen _loadScreen;
        [SerializeField] private TutorialParameters _firstLoadingTutorial;
        [SerializeField] private int _sceneTutorial;
        [SerializeField] private int _sceneMenu;

        public void Initialize()
        {
            if (!_firstLoadingTutorial.IsComplete())
            {
                _loadScreen.Initialize(_sceneTutorial);
                return;
            }
            
            _loadScreen.Initialize(_sceneMenu);
        }
    }
}
