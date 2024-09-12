using System;
using System.Collections;
using UnityEngine;
using SurvivalChicken.CollectItems.Collector;

namespace SurvivalChicken.CollectItems.Feed
{
    public class Feed : MonoBehaviour
    {
        [field: SerializeField] public int BoostValue { get; private set; }

        [Header("Pickup Animation")]
        [SerializeField] private AnimationCurve _pickupAnimationCurve;
        [SerializeField] private float _pickupAnimationDuraction;

        private readonly float ScreenWidthOffset = 100f;
        private readonly float ScreenHeightOffset = 100f;

        private event Action _pickedUp;

        private Coroutine _pickupAnimationCoroutine; 

        public void Initialize(Action pickedUp)
        {
            _pickedUp = pickedUp;

            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(
                        UnityEngine.Random.Range(-ScreenWidthOffset, Screen.width + ScreenWidthOffset),
                        UnityEngine.Random.Range(-ScreenHeightOffset, Screen.height + ScreenHeightOffset),
                        10f));

            gameObject.SetActive(true);
        }

        public void Initialize(Action pickedUp, Vector3 pos)
        {
            _pickedUp = pickedUp;

            transform.position = pos;

            gameObject.SetActive(true);
        }

        public void Pickup()
        {
            _pickedUp?.Invoke();
            Disactivate();
        }

        public void Disactivate()
        {
            gameObject.SetActive(false);
        }

        private IEnumerator PickupAnimation(FeedCollector target)
        {
            var waitForEndFrame = new WaitForFixedUpdate();

            float duraction = 0f;

            while (duraction < 1f)
            {
                duraction += Time.fixedDeltaTime * _pickupAnimationDuraction;
                transform.position = Vector3.LerpUnclamped(transform.position, target.transform.position, _pickupAnimationCurve.Evaluate(duraction));
                yield return waitForEndFrame;
            }

            _pickupAnimationCoroutine = null;
            Pickup();
        }

        public void InvokePickup(FeedCollector feedCollector)
        {
            if (_pickupAnimationCoroutine != null)
                StopCoroutine(_pickupAnimationCoroutine);
            _pickupAnimationCoroutine = StartCoroutine(PickupAnimation(feedCollector));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out FeedCollector feedCollector))
            {
                InvokePickup(feedCollector);
            }
        }
    }
}
