using UnityEngine;
using SurvivalChicken.Interfaces;
using SurvivalChicken.Spawner;
using SurvivalChicken.Structures;

namespace SurvivalChicken.Bullets
{
    public class SaberBullet : Bullet
    {
        private int _damage;
        private int _critChance;
        private int _critValue;

        public override void Initialize(Vector3 pos, Quaternion rot, int damage, int critChance, int critValue)
        {
            base.Initialize(pos, rot, damage, critChance, critValue);

            _damage = damage;
            _critChance = critChance;
            _critValue = critValue;
        }

        protected override void Move() {}

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IGetDamagable getDamagable))
            {
                Damage = new Damage(_damage, _critChance, _critValue);

                DamageNumberSpawner.Instance.SpawnDamageNumber(collision.transform.position, Damage.TotalDamage, Damage.IsCritical);
                getDamagable.GetDamage(Damage);
            }
        }
    }
}
