using UnityEngine;
using System.Collections;
using SurvivalChicken.Controllers;
using UnityEngine.UI;
using SurvivalChicken.BossObject;

namespace SurvivalChicken.Spawner
{
    public sealed class BossSpawner : MonoBehaviour
    {
        [SerializeField] private Boss _bosses;
        [SerializeField] private Transform _bossRing;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private TimerView _timer;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private EliteEnemySpawner _eliteEnemySpawner;
        [SerializeField] private Slider _healthBar;

        private readonly int TimePointToSpawnBoss = 600;

        private Coroutine _waitForSpawnBossCoroutine;

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
            SpawnBoss();
            _enemySpawner.StopSpawn();
            _eliteEnemySpawner.ClearEnemies();

            _healthBar.gameObject.SetActive(true);

            _waitForSpawnBossCoroutine = null;
        }
    }
}
