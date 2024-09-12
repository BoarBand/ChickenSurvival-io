using UnityEngine;
using System;
using System.Collections;
using SurvivalChicken.CollectItems.Collector;

namespace SurvivalChicken.CollectItems.BoostItems
{
    public abstract class BoostItem : MonoBehaviour
    {
        [Header("Pickup Animation")]
        [SerializeField] private AnimationCurve _pickupAnimationCurve;
        [SerializeField] private float _pickupAnimationDuraction;

        private Coroutine _pickupAnimationCoroutine;

        public abstract void Initialize(Vector3 pos, Quaternion rot, Action pickupAction = null);

        public virtual void Pickup()
        {
            Disactivate();
        }

        public void Disactivate()
        {
            Destroy(gameObject);
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
            if (collision.TryGetComponent(out FeedCollector feedCollector))
            {
                InvokePickup(feedCollector);
            }
        }
    }
}
