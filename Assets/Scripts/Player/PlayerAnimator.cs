using UnityEngine;

namespace SurvivalChicken.PlayerObject.Animations
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public readonly int RunLabel = Animator.StringToHash("Run");

        public void SetRunAnimation()
        {
            _animator.SetBool(RunLabel, true);
        }

        public void SetIdleAnimation()
        {
            _animator.SetBool(RunLabel, false);
        }
    }
}
