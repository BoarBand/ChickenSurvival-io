using UnityEngine;
using SurvivalChicken.ScriptableObjects.CharactersParameters.Enemy;
using SurvivalChicken.Interfaces;
using SurvivalChicken.EliteEnemiesObject;
using SurvivalChicken.EnemiesObject.Animations;

namespace SurvivalChicken.EnemiesObject.Attack
{
    public abstract class EliteEnemyAttack : MonoBehaviour, IAttackable, IAttackAction
    {
        [SerializeField] protected EnemyCharacterParameters EnemyParameters;
        [SerializeField] protected EliteEnemy EliteEnemy;
        [SerializeField] protected EnemyAnimator Animator;

        public abstract void Attack();

        public abstract void AttackAction();
    }
}
