using UnityEngine;
using System.Collections;
using SurvivalChicken.Bullets;

namespace SurvivalChicken.EnemiesObject.Attack
{
    public class ProjectileEliteEnemyAttack : EliteEnemyAttack
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private Transform _shootPoint;

        [Header("Parameters")]
        [SerializeField] private float _bulletMoveSpeed;
        [SerializeField] private float _bulletLifetime;

        private Coroutine _attackingCoroutine;

        private void Awake()
        {
            if (_attackingCoroutine != null)
                StopCoroutine(_attackingCoroutine);
            _attackingCoroutine = StartCoroutine(Attacking());
        }

        public override void Attack()
        {
            AttackAction();
        }

        public void StartAttack()
        {
            EliteEnemy.CanMove = false;
        }

        public void EndAttack()
        {
            EliteEnemy.CanMove = true;
        }

        private IEnumerator Attacking()
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(EnemyParameters.AttackFrequency);

            while (true)
            {
                yield return waitForSeconds;

                Animator.SetAttackAnimation();
            }
        }

        private void StopAttacking()
        {
            if (_attackingCoroutine != null)
                StopCoroutine(_attackingCoroutine);
            _attackingCoroutine = null;
        }

        public override void AttackAction()
        {
            Bullet bullet = Instantiate(_bullet, _shootPoint.transform.position, Quaternion.Euler(-90f, 0f, 0f));
            bullet.Initialize(_shootPoint.transform.position,
                new Vector3(-90f, 0f, 0f),
                EnemyParameters.Damage,
                0,
                0, _bulletLifetime, _bulletMoveSpeed,
                () => { });
        }

        private void OnDisable()
        {
            StopAttacking();
        }
    }
}
