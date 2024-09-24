using UnityEngine;
using SurvivalChicken.Bullets;
using SurvivalChicken.Tools.Pool;

namespace SurvivalChicken.EnemiesObject.Attack
{
    public class RangeEnemyAttack : EnemyAttack
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private Transform _shootPoint;

        private ObjectsPool<Bullet> _objectsPool;

        private int _damage;

        private readonly uint InitAmount = 0;
        private readonly float BulletMoveSpeed = 4.5f;
        private readonly float BulletLifetime = 5f;
        private readonly int BulletAmounts = 6;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _objectsPool = new ObjectsPool<Bullet>(Create, Add, Get, InitAmount);

            _damage = EnemyParameters.Damage;

            Invoke(nameof(AttackAction), 5f);
        }

        public override void Attack()
        {
            Animator.SetAttackAnimation();
        }

        private Bullet Create()
        {
            return Instantiate(_bullet, _shootPoint.transform.position, Quaternion.identity);
        }

        private void Add(Bullet bullet)
        {
            bullet.Disactivate();
        }

        public override void AttackAction()
        {
            float angle = 0;
            float angleStep = 360f / BulletAmounts;

            for (int i = 0; i < BulletAmounts; i++)
            {
                Bullet bullet = _objectsPool.Get();
                bullet.Initialize(_shootPoint.position, Quaternion.Euler(0f, 0f, angle),
                    _damage,
                    0,
                    0);

                angle += angleStep;
            }
        }

        private void Get(Bullet bullet)
        {
            bullet.Initialize(_shootPoint.transform.position,
                new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f),
                _damage,
                0,
                0, BulletLifetime, BulletMoveSpeed,
                () => _objectsPool.Add(bullet));
        }
    }
}
