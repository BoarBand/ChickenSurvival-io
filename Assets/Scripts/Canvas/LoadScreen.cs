using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;

namespace SurvivalChicken.SceneLoader
{
    public class LoadScreen : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        [SerializeField] private int _sceneNum;

        private readonly float DelayTime = 2.2f;

        private Coroutine _loadAsyncCoroutine;

        private void OnEnable()
        {
            Initialize();
        }

        public void Initialize()
        {
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

            if (_loadAsyncCoroutine != null)
                StopCoroutine(_loadAsyncCoroutine);
            _loadAsyncCoroutine = StartCoroutine(LoadAsync());
        }

        private IEnumerator LoadAsync()
        {
            _slider.DOValue(1f, DelayTime / 1.3f);

            yield return new WaitForSecondsRealtime(DelayTime);
            SceneManager.LoadScene(_sceneNum);

            _loadAsyncCoroutine = null;
        }
    }
}
