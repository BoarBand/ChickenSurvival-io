using UnityEngine;
using SurvivalChicken.CharactersActions;
using SurvivalChicken.PlayerObject.Animations;
using SurvivalChicken.Spawner;
using SurvivalChicken.Interfaces;
using SurvivalChicken.ScriptableObjects.CharactersParameters.Player;

namespace SurvivalChicken.PlayerObject.Movement
{
    public class PlayerMovement : MonoBehaviour, IChangeMovementSpeed
    {
        [SerializeField] private PlayerAnimator _animator;
        [SerializeField] private PlayerCharacterParameters _playerParameters;
        [SerializeField] private Transform _object;

        private Joystick _joystick;

        private readonly MovementAction _movementAction = new MovementAction();

        public float MovementSpeed { get; set; }

        private void Start()
        {
            MovementSpeed = _playerParameters.MoveSpeed;
        }

        private void OnEnable()
        {
            _joystick = PlayerSpawner.Instance.Joystick;

            _joystick.Dragged += () =>
            {
                _animator.SetRunAnimation();
                TurnPlayer();
            };
            _joystick.PointerUp += () => _animator.SetIdleAnimation();
        }

        private void OnDisable()
        {
            _joystick.Dragged -= () =>
            {
                _animator.SetRunAnimation();
                TurnPlayer();
            };
            _joystick.PointerUp -= () => _animator.SetIdleAnimation();
        }

        private void FixedUpdate()
        {
            _movementAction.FixedUpdateMove(transform, _joystick.Direction, MovementSpeed);
        }

        private void TurnPlayer()
        {
            if (_joystick.Direction.x < 0f)
                _object.localScale = new Vector3(-1f, 1f, 1f);
            else if (_joystick.Direction.x > 0f)
                _object.localScale = new Vector3(1f, 1f, 1f);
        }

        public void ChangeMovementSpeed(float percent)
        {
            MovementSpeed += MovementSpeed * (percent / 100f);
        }
    }
}
