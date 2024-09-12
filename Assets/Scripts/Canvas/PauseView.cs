using UnityEngine;

namespace SurvivalChicken.Controllers
{
    public class PauseView : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseImage;

        public void EnablePauseView()
        {
            _pauseImage.SetActive(true);
            Time.timeScale = 0;
        }

        public void DisablePauseView()
        {
            _pauseImage.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
