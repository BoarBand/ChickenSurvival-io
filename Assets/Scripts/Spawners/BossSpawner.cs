using UnityEngine;
using System.Collections;
using SurvivalChicken.Controllers;
using UnityEngine.UI;
using SurvivalChicken.BossObject;
using SurvivalChicken.Buffs;

namespace SurvivalChicken.Spawner
{
    public sealed class BossSpawner : MonoBehaviour
    {
        [SerializeField] private Boss _bosses;
        [SerializeField] private Transform _bossRing;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private TimerView _timer;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private EliteEnemySpawner _eliteEnemySpawner;
        [SerializeField] private ParticleSystem _spawnEffect;
        [SerializeField] private Slider _healthBar;
        [SerializeField] private Transform[] _corners;

        private readonly int TimePointToSpawnBoss = 600;
        private readonly float DelayToSpawn = 3f;
        
        private Coroutine _waitForSpawnBossCoroutine;

        private ClampMovementBuff _clampMovementBuff;

        public void Initialize()
        {
            if (_waitForSpawnBossCoroutine != null)
                StopCoroutine(_waitForSpawnBossCoroutine);
            _waitForSpawnBossCoroutine = StartCoroutine(WaitForSpawnBoss());
        }

        private void SetBossRing()
        {
            _bossRing.gameObject.SetActive(true);
            _bossRing.position = PlayerSpawner.Instance.CurrentPlayer.transform.position;
        }

        private void ResetBoss()
        {
            _timer.StartTimer();
            _enemySpawner.StartSpawn();
            _bossRing.gameObject.SetActive(false);
            _healthBar.gameObject.SetActive(false);

            Destroy(_clampMovementBuff);
        }

        private void SpawnBoss()
        {
            Boss boss = Instantiate(_bosses);
            boss.Initialize(_spawnPoint.position, () => ResetBoss(), _healthBar);
        }

        private IEnumerator WaitForSpawnBoss()
        {
            var waitUntil = new WaitUntil(() => _timer.Time >= TimePointToSpawnBoss);

            yield return waitUntil;

            SetBossRing();
            _timer.StopTimer();
            _enemySpawner.StopSpawn();
            _eliteEnemySpawner.ClearEnemies();

            _clampMovementBuff = _playerSpawner.CurrentPlayer.gameObject.AddComponent<ClampMovementBuff>();
            _clampMovementBuff.Initialize(_corners[0].position, _corners[1].position, _corners[2].position, _corners[3].position);

            _healthBar.gameObject.SetActive(true);

            yield return new WaitForSeconds(DelayToSpawn);
            _spawnEffect.gameObject.SetActive(true);
            SpawnBoss();

            _waitForSpawnBossCoroutine = null;
        }
    }
}
