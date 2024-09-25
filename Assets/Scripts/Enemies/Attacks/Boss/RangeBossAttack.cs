using System.Collections;
using UnityEngine;
using SurvivalChicken.Bullets;
using SurvivalChicken.Tools.Pool;

namespace SurvivalChicken.EnemiesObject.Attack
{
    public class RangeBossAttack : BossAttack
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private Transform _shootPoint;

        [Header("Parameters")]
        [SerializeField] private float _bulletMoveSpeed;
        [SerializeField] private float _bulletLifetime;
        [SerializeField] private int _bulletAmounts;
        [SerializeField] private float _attackFrequency;

        private ObjectsPool<Bullet> _objectsPool;

        private int _damage;

        private Coroutine _attackingCoroutine;

        private readonly uint InitAmount = 0;

        private void Start()
        {
            Initialize();
        }

        private void OnEnable()
        {
            if (_attackingCoroutine != null)
                StopCoroutine(_attackingCoroutine);
            _attackingCoroutine = StartCoroutine(Attacking());
        }

        private void OnDisable()
        {
            StopCoroutine(_attackingCoroutine);
            _attackingCoroutine = null;
        }

        private IEnumerator Attacking()
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(_attackFrequency);

            while (gameObject.activeInHierarchy)
            {
                yield return waitForSeconds;
                Attack();
            }

            _attackingCoroutine = null;
        }

        private void Initialize()
        {
            _objectsPool = new ObjectsPool<Bullet>(Create, Add, Get, InitAmount);

            _damage = EnemyParameters.Damage;
        }

        public override void Attack()
        {
            AttackAction();
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
            float angleStep = 360f / _bulletAmounts;

            for (int i = 0; i < _bulletAmounts; i++)
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
                0, _bulletLifetime, _bulletMoveSpeed,
                () => _objectsPool.Add(bullet));
        }
    }
}