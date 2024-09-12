using UnityEngine;
using SurvivalChicken.ScriptableObjects.CharactersParameters.Enemy;
using SurvivalChicken.Interfaces;
using SurvivalChicken.BossObject;
using SurvivalChicken.EnemiesObject.Animations;

namespace SurvivalChicken.EnemiesObject.Attack
{
    public abstract class BossAttack : MonoBehaviour, IAttackable, IAttackAction
    {
        [SerializeField] protected EnemyCharacterParameters EnemyParameters;
        [SerializeField] protected Boss Boss;
        [SerializeField] protected EnemyAnimator Animator;

        public abstract void Attack();

        public abstract void AttackAction();
    }
}

