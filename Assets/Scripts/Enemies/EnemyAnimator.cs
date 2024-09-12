using UnityEngine;

namespace SurvivalChicken.EnemiesObject.Animations
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public readonly int RunLabel = Animator.StringToHash("Run");
        public readonly int DeadLabel = Animator.StringToHash("IsDead");
        public readonly int AttackLabel = Animator.StringToHash("Attack");

        public void SetRunAnimation()
        {
            _animator.SetBool(RunLabel, true);
        }

        public void SetDeadAnimation()
        {
            _animator.SetTrigger(DeadLabel);
        }

        public void SetAttackAnimation()
        {
            _animator.SetTrigger(AttackLabel);
        }
    }
}
