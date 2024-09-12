using UnityEngine;
using SurvivalChicken.PlayerObject;
using Cinemachine;
using SurvivalChicken.Controllers;

namespace SurvivalChicken.Spawner
{
    public sealed class PlayerSpawner : MonoBehaviour
    {
        public static PlayerSpawner Instance;

        [SerializeField] private Player _player;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;

        [field: SerializeField] public Joystick Joystick { get; private set; }

        public Player CurrentPlayer { get; private set; }

        public void Initialize()
        {
            Instance = this;
            
            CurrentPlayer = Instantiate(_player, _spawnPoint.position, Quaternion.identity);
            CurrentPlayer.Initialize(() => GameOverView.Instance.EnableDiedImage());

            _cinemachineVirtualCamera.Follow = CurrentPlayer.transform;
        }
    }
}
