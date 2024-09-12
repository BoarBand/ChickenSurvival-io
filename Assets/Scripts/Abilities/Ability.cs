using UnityEngine;
using System.Collections;
using SurvivalChicken.Interfaces;
using SurvivalChicken.PlayerObject;
using SurvivalChicken.Spawner;
using SurvivalChicken.EnemiesObject;
using SurvivalChicken.ScriptableObjects.AbilitiesParameters;
using SurvivalChicken.CharactersActions;

namespace SurvivalChicken.Abilities
{
    public abstract class Ability : MonoBehaviour, IAttackable, IUpgradable
    {
        [SerializeField] public AbilityParameters AbilityParameters;

        [SerializeField] protected EnemyInAreaCounter AreaCounter;

        protected Player Player;

        protected readonly float RotateToEnemySpeed = 5f;
        protected readonly float TimeForResetRotation = 0.2f;
        protected readonly float RotationOffset = -10f;

        private Coroutine _rotateToEnemyCoroutine;
        private Coroutine _resetRotationToDefualt;
        private Coroutine _invokeAttacking;

        private void OnEnable()
        {
            Player = PlayerSpawner.Instance.CurrentPlayer;

            Initialize();
        }

        public abstract void Initialize();

        public abstract void Attack();

        public abstract void Upgrade();

        private void Update()
        {
            if (_invokeAttacking == null)
                _invokeAttacking = StartCoroutine(InvokeAttacking());
        }

        #region Attack

        private IEnumerator InvokeAttacking()
        {
            var waitForSecs = new WaitForSeconds(AbilityParameters.RecoveryTime);

            yield return waitForSecs;
            Attack();

            _invokeAttacking = null;
        }

        #endregion

        #region Rotation To Target

        protected void LookAtEmeny(Enemy enemy)
        {
            if (enemy == null)
            {
                ResetRotation();
                return;
            }

            if (_rotateToEnemyCoroutine != null)
                return;

            if (_rotateToEnemyCoroutine != null)
                StopCoroutine(_rotateToEnemyCoroutine);
            _rotateToEnemyCoroutine = StartCoroutine(RotateToEnemy(enemy.transform.position));
        }

        protected void ResetRotation()
        {
            if (_resetRotationToDefualt != null)
                return;

            if (_resetRotationToDefualt != null)
                StopCoroutine(_resetRotationToDefualt);
            _resetRotationToDefualt = StartCoroutine(ResetRotationToDefualt());
        }

        private IEnumerator RotateToEnemy(Vector3 enemyPos)
        {
            if (_resetRotationToDefualt != null)
            {
                StopCoroutine(_resetRotationToDefualt);
                _resetRotationToDefualt = null;
            }

            WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

            float progress = 0;

            Vector3 target = enemyPos - transform.position;

            while(progress < 1f)
            {
                progress += Time.fixedDeltaTime * RotateToEnemySpeed;

                Quaternion targetRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y,
                    Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg + RotationOffset);

                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, progress);

                yield return waitForFixedUpdate;
            }

            _rotateToEnemyCoroutine = null;
        }

        private IEnumerator ResetRotationToDefualt()
        {
            float progress = 0;

            if (_rotateToEnemyCoroutine != null)
            {
                StopCoroutine(_rotateToEnemyCoroutine);
                _rotateToEnemyCoroutine = null;
            }

            yield return new WaitForSeconds(TimeForResetRotation);

            while (progress < 1f)
            {
                progress += Time.fixedDeltaTime * RotateToEnemySpeed;

                Quaternion targetRotation = Quaternion.Euler(0f, 0f, 0f);

                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, progress);

                yield return null;
            }

            _resetRotationToDefualt = null;
        }

        #endregion
    }

}
