using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace SurvivalChicken.SceneLoader
{
    public class LoadScreen : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        [SerializeField] private int _sceneNum;

        private readonly float DelayTime = 1.2f;

        private void OnEnable()
        {
            Initialize(_sceneNum);
        }

        public void Initialize(int sceneNum)
        {
            _sceneNum = sceneNum;

            gameObject.SetActive(true);

			if(_slider != null)
				LoadScene();
        }

	    public void LoadSceneByNumber()
	    {
		    SceneManager.LoadScene(_sceneNum);
	    }

        private void LoadScene()
        {
            _slider.value = 0;

            gameObject.SetActive(true);

            Sequence sequence = DOTween.Sequence()
                .Append(_slider.DOValue(1f, DelayTime)).SetUpdate(true)
                .AppendCallback(() => SceneManager.LoadScene(_sceneNum));
        }
    }
}
