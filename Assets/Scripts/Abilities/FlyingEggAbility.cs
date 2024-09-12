using UnityEngine;
using SurvivalChicken.PlayerObject;
using SurvivalChicken.Bullets;
using SurvivalChicken.Tools.Pool;

namespace SurvivalChicken.Abilities
{
    public class FlyingEggAbility : Ability
    {
        [SerializeField] private Bullet _bullet;

        private ObjectsPool<Bullet> _objectsPool;

        private int _damage;

        private delegate void AttackAction();
        private AttackAction _attackAction;

        private readonly uint InitAmount = 0;
        private readonly float BulletMoveSpeed = 0.45f;
        private readonly float BulletLifetime = 2f;
        private readonly float AngleSpread = 0f;

        public override void Initialize()
        {
            transform.SetParent(Player.AbilitiesContainer);
            transform.localScale = Vector3.one;
            transform.localPosition = Vector3.zero;

            _objectsPool = new ObjectsPool<Bullet>(Create, Add, Get, InitAmount);

            Upgrade();

            _damage = AbilityParameters.Damage;
        }

        public override void Attack()
        {
            _attackAction?.Invoke();
        }

        public override void Upgrade()
        {
            if (AbilityParameters.Level < AbilityParameters.MaxLevel)
                AbilityParameters.Level++;

            switch (AbilityParameters.Level)
            {
                case 1:
                    _attackAction += () => _objectsPool.Get();
                    _damage = AbilityParameters.Damage;
                    break;
                case 2:
                    _attackAction += () => _objectsPool.Get();
                    _damage += _damage / 2;
                    break;
                case 3:
                    _attackAction += () => _objectsPool.Get();
                    _damage += _damage / 2;
                    break;
                case 4:
                    _attackAction += () => _objectsPool.Get();
                    _damage += _damage / 2;
                    break;
                case 5:
                    _attackAction += () => _objectsPool.Get();
                    _damage += _damage / 2;
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

            EnemiesObject.Enemy enemy = AreaCounter.GetRandomEnemyFromArea();

            Vector3 diraction = (enemy != null) ? enemy.transform.position : Vector3.one;

            Vector3 targetPosToScreen = Camera.main.WorldToScreenPoint(diraction);
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(targetPosToScreen.x, Screen.height + 150f, 10f));

            bullet.Initialize(pos,
                diraction,
                _damage,
                Player.CritDamageChance,
                Player.CritDamageValue, BulletLifetime, BulletMoveSpeed,
                () => _objectsPool.Add(bullet));
        }
    }
}
