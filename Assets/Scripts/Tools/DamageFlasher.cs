using System.Collections;
using UnityEngine;

namespace SurvivalChicken.CharactersActions
{
    public class DamageFlasher : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color _flashColor;
        [SerializeField] private float _flashDuration;

        private Material _material;

        private Coroutine _damageFlashCoroutine;

        private readonly string FlashColorLabel = "_FlashColor";
        private readonly string FlashAmountLabel = "_FlashAmount";


        private void OnEnable()
        {
            _material = _spriteRenderer.material;
            _material.SetColor(FlashColorLabel, _flashColor);

            ResetFlasher();
        }

        public void Play()
        {
            if (_damageFlashCoroutine != null)
                StopCoroutine(_damageFlashCoroutine);
            _damageFlashCoroutine = StartCoroutine(DamageFlashBehavior());
        }

        public void ResetFlasher()
        {
            if (_damageFlashCoroutine != null)
                StopCoroutine(_damageFlashCoroutine);

            _material.SetFloat(FlashAmountLabel, 0f);
        }

        private IEnumerator DamageFlashBehavior()
        {
            float currentFlashAmount = 0f;
            float elapsedTime = 0f;

            while(elapsedTime < _flashDuration)
            {
                elapsedTime += Time.fixedDeltaTime;

                currentFlashAmount = Mathf.Lerp(1f, 0f, elapsedTime / _flashDuration);
                _material.SetFloat(FlashAmountLabel, currentFlashAmount);

                yield return null;
            }

            _damageFlashCoroutine = null;
        }
    }
}
