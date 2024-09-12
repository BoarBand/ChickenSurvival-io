using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

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
            gameObject.SetActive(true);

            if (_loadAsyncCoroutine != null)
                StopCoroutine(_loadAsyncCoroutine);
            _loadAsyncCoroutine = StartCoroutine(LoadAsync());
        }

        private IEnumerator LoadAsync()
        {
            AsyncOperation loadAsync = SceneManager.LoadSceneAsync(_sceneNum);
            loadAsync.allowSceneActivation = false;

            while (!loadAsync.isDone)
            {
                _slider.value = loadAsync.progress;

                if(loadAsync.progress >= .9f && !loadAsync.allowSceneActivation)
                {
                    yield return new WaitForSeconds(DelayTime);
                    loadAsync.allowSceneActivation = true;
                }
                yield return null;
            }

            _loadAsyncCoroutine = null;
        }
    }
}
