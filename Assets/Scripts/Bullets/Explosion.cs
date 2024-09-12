using UnityEngine;
using SurvivalChicken.Structures;
using SurvivalChicken.Interfaces;
using SurvivalChicken.Spawner;

namespace SurvivalChicken.Bullets
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private Vector3 _startRotation;
        [SerializeField] private CircleCollider2D _collider;

        private Damage _damage;

        public void Initialize(Damage damage, Vector3 pos, int layerMask)
        {
            _damage = damage;

            transform.position = pos;
            transform.rotation = Quaternion.Euler(_startRotation);

            Collider2D[] _colliders = Physics2D.OverlapCircleAll(transform.transform.position, _collider.radius, layerMask);

            foreach(Collider2D collider in _colliders)
                if (collider.TryGetComponent(out IGetDamagable getDamagable))
                {
                    DamageNumberSpawner.Instance.SpawnDamageNumber(collider.transform.position, damage.TotalDamage, damage.IsCritical);
                    getDamagable.GetDamage(_damage);
                }

            _collider.enabled = false;
        }
    }
}
