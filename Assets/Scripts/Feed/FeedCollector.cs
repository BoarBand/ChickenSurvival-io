using UnityEngine;

namespace SurvivalChicken.CollectItems.Collector
{
    public class FeedCollector : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D _collider;

        public float CurrentRadius => _collider.radius;

        public void ChangeCollectRadius(float amount)
        {
            _collider.radius += amount;
        }
    }
}
