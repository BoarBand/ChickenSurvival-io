using UnityEngine;
using SurvivalChicken.Bullets;
using System.Collections;
using SurvivalChicken.Tools.Pool;
using SurvivalChicken.BossObject;

namespace SurvivalChicken.EnemiesObject.Attack
{
    public class ShootBossAttack : BossAttack
    {
        [SerializeField] private BulletLifeTime _bullet;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private ParticleSystem _shootEffect;

        [Header("Parameters")]
        [SerializeField] private float _shootFrequency;
        [SerializeField] private float _bulletMoveSpeed;
        [SerializeField] private float _bulletLifetime;

        private Coroutine _shootingCoroutine;
        private Coroutine _invokeShootingCoroutine;

        private ObjectsPool<Bullet> _bulletsPool;

        private readonly uint InitAmount = 0;

        private void Awake()
        {
            _bulletsPool = new ObjectsPool<Bullet>(Create, Add, Get, InitAmount);

            if (_invokeShootingCoroutine != null)
                StopCoroutine(_invokeShootingCoroutine);
            _invokeShootingCoroutine = StartCoroutine(InvokeShooting());
        }

        public override void Attack()
        {
            if (_shootingCoroutine != null)
                StopCoroutine(_shootingCoroutine);
            _shootingCoroutine = StartCoroutine(Shooting());

            Boss.CanMove = false;
        }

        public override void AttackAction()
        {
            if (transform.localScale.x < 0)
                _shootPoint.localEulerAngles = Vector3.zero;
            
            if(transform.localScale.x > 0)
                _shootPoint.localEulerAngles = new Vector3(0f, 0f, 180f);

            _bulletsPool.Get();
            _shootEffect.Play();
        }

        private IEnumerator Shooting()
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(_shootFrequency);

            while (true)
            {
                yield return waitForSeconds;
                AttackAction();
            }
        }

        private IEnumerator InvokeShooting()
        {
            yield return new WaitForSeconds(EnemyParameters.AttackFrequency);
            Animator.SetAttackAnimation();
            _invokeShootingCoroutine = null;
        }

        public void StopShooting()
        {
            if(_shootingCoroutine != null)
                StopCoroutine(_shootingCoroutine);
            _shootingCoroutine = null;

            if (_invokeShootingCoroutine != null)
                StopCoroutine(_invokeShootingCoroutine);
            _invokeShootingCoroutine = StartCoroutine(InvokeShooting());

            Boss.CanMove = true;
        }

        private Bullet Create()
        {
            return Instantiate(_bullet, _shootPoint.transform.position, Quaternion.identity);
        }

        private void Add(Bullet bullet)
        {
            bullet.Disactivate();
        }

        private void Get(Bullet bullet)
        {
            bullet.Initialize(_shootPoint.transform.position,
                new Vector3(0f, 0f, _shootPoint.eulerAngles.z),
                EnemyParameters.Damage,
                0,
                0, _bulletLifetime, _bulletMoveSpeed,
                () => _bulletsPool.Add(bullet));
        }
    }
}
