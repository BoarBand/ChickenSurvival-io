using UnityEngine;
using SurvivalChicken.PlayerObject;
using SurvivalChicken.Bullets;
using SurvivalChicken.Tools.Pool;


namespace SurvivalChicken.Abilities
{
    public class DragonAbility : Ability
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Animator _animator;

        private ObjectsPool<Bullet> _objectsPool;

        private int _damage;

        private readonly uint InitAmount = 0;
        private readonly float BulletMoveSpeed = 8f;
        private readonly float AngleSpread = 360f;

        private delegate void AttackAction();
        private AttackAction _attackAction;

        private readonly int RoarLabel = Animator.StringToHash("Roar");

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
            if (AreaCounter.Amount <= 0 || AreaCounter == null)
                return;

            _animator.SetTrigger(RoarLabel);
        }

        public void AttackBehavior()
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
                    break;
                case 2:
                    _damage += _damage / 3;
                    _attackAction += () => _objectsPool.Get();
                    break;
                case 3:
                    _damage += _damage / 3;
                    _attackAction += () => _objectsPool.Get();
                    break;
                case 4:
                    _damage += _damage / 3;
                    _attackAction += () => _objectsPool.Get();
                    break;
                case 5:
                    _damage += _damage / 3;
                    _attackAction += () => _objectsPool.Get();
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

            if (enemy.transform.position.x >= transform.position.x)
                transform.localScale = new Vector3(-1f, 1f, 1f);
            else
                transform.localScale = Vector3.one;

            bullet.Initialize(transform.position,
                new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotZ), enemy,
                _damage,
                Player.CritDamageChance,
                Player.CritDamageValue, BulletMoveSpeed,
                () => _objectsPool.Add(bullet));
        }
    }
}