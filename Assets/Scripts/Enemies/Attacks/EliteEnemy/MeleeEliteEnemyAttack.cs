using UnityEngine;
using System.Collections;
using SurvivalChicken.Interfaces;
using SurvivalChicken.Structures;

namespace SurvivalChicken.EnemiesObject.Attack
{
    public class MeleeEliteEnemyAttack : EliteEnemyAttack
    {
        private Coroutine _attackCoroutine;

        private IGetDamagable _getDamagable;

        public override void Attack()
        {
            Animator.SetAttackAnimation();
        }

        public override void AttackAction()
        {
            if (_getDamagable == null)
                return;

            _getDamagable.GetDamage(new Damage(EnemyParameters.Damage));
        }

        private IEnumerator Attacking()
        {
            var waitForSecs = new WaitForSeconds(EnemyParameters.AttackFrequency);

            while (true)
            {
                yield return waitForSecs;
                Attack();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IGetDamagable getDamagable))
            {
                EliteEnemy.CanMove = false;
                _getDamagable = getDamagable;

                if (_attackCoroutine != null)
                    StopCoroutine(_attackCoroutine);
                _attackCoroutine = StartCoroutine(Attacking());
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IGetDamagable getDamagable))
            {
                EliteEnemy.CanMove = true;
                _getDamagable = null;

                if(_attackCoroutine != null)
                    StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }
        }
    }
}
