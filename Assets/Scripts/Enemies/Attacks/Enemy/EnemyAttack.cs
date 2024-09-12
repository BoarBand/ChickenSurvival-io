using UnityEngine;
using SurvivalChicken.ScriptableObjects.CharactersParameters.Enemy;
using SurvivalChicken.Interfaces;
using SurvivalChicken.EnemiesObject.Animations;

namespace SurvivalChicken.EnemiesObject.Attack
{
    public abstract class EnemyAttack : MonoBehaviour, IAttackable, IAttackAction
    {
        [SerializeField] protected EnemyCharacterParameters EnemyParameters;
        [SerializeField] protected Enemy Enemy;
        [SerializeField] protected EnemyAnimator Animator;

        public abstract void Attack();

        public abstract void AttackAction();
    }
}
