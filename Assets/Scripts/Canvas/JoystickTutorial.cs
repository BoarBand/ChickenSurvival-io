using UnityEngine;

namespace SurvivalChicken.Tutorials
{
    public class JoystickTutorial : MonoBehaviour
    {
        [SerializeField] private TutorialParameters _tutorial;
        [SerializeField] private Transform _tip;

        public void Initialize()
        {
            if (_tutorial.IsComplete())
                _tip.gameObject.SetActive(false);
            else
                _tip.gameObject.SetActive(true);
        }
    }
}