using UnityEngine;
using SurvivalChicken.PlayerObject;
using SurvivalChicken.Bullets;
using SurvivalChicken.Tools.Pool;

namespace SurvivalChicken.Abilities
{
    public class BounceMeteoritesAbility : Ability
    {
        [SerializeField] private Bullet _bullet;

        private ObjectsPool<Bullet> _objectsPool;

        private int _damage;

        private readonly uint InitAmount = 0;
        private readonly float BulletMoveSpeed = 6f;
        private readonly float BulletLifetime = 0.0f;
        private readonly float AngleSpread = 360f;

        public override void Initialize()
        {
            transform.SetParent(Player.AbilitiesContainer);
            transform.localScale = Vector3.one;
            transform.localPosition = Vector3.zero;

            _objectsPool = new ObjectsPool<Bullet>(Create, Add, Get, InitAmount);

            _damage = AbilityParameters.Damage;

            Upgrade();

        }

        public override void Attack()
        {
            // no attack
        }

        public override void Upgrade()
        {
            if (AbilityParameters.Level < AbilityParameters.MaxLevel)
                AbilityParameters.Level++;

            switch (AbilityParameters.Level)
            {
                case 1:
                    _objectsPool.Get();
                    break;
                case 2:
                    _damage += _damage / 3;
                    _objectsPool.Get();
                    break;
                case 3:
                    _damage += _damage / 3;
                    _objectsPool.Get();
                    break;
                case 4:
                    _damage += _damage / 3;
                    _objectsPool.Get();
                    break;
                case 5:
                    _damage += _damage / 3;
                    _objectsPool.Get();
                    break;
            }
        }

        private Bullet Create()
        {
            return Instantiate(_bullet);
        }

        private void Add(Bullet bullet)
        {
            bullet.Disactivate();
        }

        private void Get(Bullet bullet)
        {
            float rotZ = transform.eulerAngles.z + Random.Range(-AngleSpread, AngleSpread);

            bullet.Initialize(transform.position,
                new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotZ),
                _damage,
                Player.CritDamageChance,
                Player.CritDamageValue, BulletLifetime, BulletMoveSpeed,
                () => _objectsPool.Add(bullet));
        }
    }
}