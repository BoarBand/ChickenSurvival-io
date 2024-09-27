using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using SurvivalChicken.EnemiesObject;
using SurvivalChicken.Tools.Pool;
using SurvivalChicken.Controllers;

namespace SurvivalChicken.Spawner
{
    public sealed class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<Enemy> _availableEnemies = new List<Enemy>();
        [SerializeField] private EliteEnemySpawner _eliteEnemySpawner;

        public ObjectsPool<Enemy> _objectsPool;

        private float _spawnFrequency;
        private float _spawnEliteEnemyChance;

        private bool _canSpawn;

        private Coroutine _spawnEnemiesCoroutine;
        private Coroutine _increaseFrequencyCoroutine;
        private Coroutine _spawnCrowdCoroutine;
        private Coroutine _spawnEnemiesBySquare;

        private readonly uint InitEnemiesAmount = 0;
        private readonly float MaxSpawnOffScreenOffset = 150f;

        private readonly float MinSpawnFrequency = 1.0f;
        private readonly float MaxSpawnFrequency = 0.15f;
        private readonly float TimeToMaxFrequency = 320f;

        private readonly float MinChanceToSpawnEliteEnemy = 0.01f;
        private readonly float MaxChanceToSpawnEliteEnemy = 0.7f;
        private readonly float TimeToMaxChanceToSpawnEliteEnemy = 500f;

        public void Initialize()
        {
            _objectsPool = new ObjectsPool<Enemy>(Create, Add, Get, InitEnemiesAmount);
            _canSpawn = true;

            _spawnFrequency = MinSpawnFrequency;
            _spawnEliteEnemyChance = MinChanceToSpawnEliteEnemy;

            if (_spawnCrowdCoroutine != null)
                StopCoroutine(_spawnCrowdCoroutine);
            _spawnCrowdCoroutine = StartCoroutine(SpawnCrowdByBorder());

            if (_spawnEnemiesBySquare != null)
                StopCoroutine(_spawnEnemiesBySquare);
            _spawnEnemiesBySquare = StartCoroutine(SpawnCrowdBySquare());
        }

        private void FixedUpdate()
        {
            if (_spawnEnemiesCoroutine == null && _canSpawn)
                _spawnEnemiesCoroutine = StartCoroutine(SpawnEnemies());

            if (_increaseFrequencyCoroutine == null && _canSpawn)
                _increaseFrequencyCoroutine = StartCoroutine(IncreaseFrequency());
        }

        private IEnumerator SpawnEnemies()
        {
            yield return new WaitForSeconds(_spawnFrequency);

            if (_canSpawn)
            {
                float chance = Random.Range(0, 1f);

                if (chance <= _spawnEliteEnemyChance)
                    _eliteEnemySpawner.CreateEliteEnemy();
                else
                    _objectsPool.Get();
            }

            _spawnEnemiesCoroutine = null;
        }

        private IEnumerator IncreaseFrequency()
        {
            yield return new WaitForSeconds(1f);

            if (_spawnFrequency > MaxSpawnFrequency)
                _spawnFrequency -= (MinSpawnFrequency - MaxSpawnFrequency) / TimeToMaxFrequency;

            if (_spawnEliteEnemyChance < MaxChanceToSpawnEliteEnemy)
                _spawnEliteEnemyChance += (MaxChanceToSpawnEliteEnemy - MinChanceToSpawnEliteEnemy) / TimeToMaxChanceToSpawnEliteEnemy;

            _increaseFrequencyCoroutine = null;
        }

        private IEnumerator SpawnCrowdByBorder()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(25f, 35f));

                if (_canSpawn)
                    SpawnEnemyByBorder();
            }
        }

        private IEnumerator SpawnCrowdBySquare()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(10f, 20f));

                if (_canSpawn)
                    SpawnEnemySquare();
            }
        }

        public void StopSpawn()
        {
            _canSpawn = false;
            _objectsPool.AddAll();
        }

        public void StartSpawn()
        {
            _canSpawn = true;
        }

        private Enemy Create()
        {
            return Instantiate(_availableEnemies[Random.Range(0, _availableEnemies.Count)]);
        }

        private void Add(Enemy enemy)
        {
            enemy.Disactivate();
        }

        private void Get(Enemy enemy)
        {
            enemy.Initialize(GetRandomPointOffscreen(), () => 
            {
                if(StatisticsView.Instance != null)
                    StatisticsView.Instance.IncreaseKillsAmount();
                _objectsPool.Add(enemy);
            });
        }

        private void SpawnEnemySquare()
        {
            float size_width = Random.Range(2f, 7f);
            float size_height = Random.Range(2f, 7f);
            float step = Random.Range(0.75f, 1f);

            Vector3 spawnPoint = GetRandomPointOffscreen();

            for (float i = spawnPoint.x; i < spawnPoint.x + size_width; i += step)
            {
                for (float j = spawnPoint.y; j < spawnPoint.y + size_height; j += step)
                {
                    _objectsPool.Get().transform.position = new Vector3(i, j, 0f);
                }
            }
        }

        private void SpawnEnemyByBorder()
        {
            float step = Random.Range(150f, 200f);

            Vector3 topBorder = new Vector3(0f, 0f, 0f);

            for (float i = topBorder.x; i < topBorder.x + Screen.width; i += step)
                _objectsPool.Get().transform.position = Camera.main.ScreenToWorldPoint(new Vector3(i, topBorder.y, 0f));

            Vector3 downBorder = new Vector3(0f, Screen.height, 0f);

            for (float i = downBorder.x; i < downBorder.x + Screen.width; i += step)
                _objectsPool.Get().transform.position = Camera.main.ScreenToWorldPoint(new Vector3(i, downBorder.y, 0f));

            Vector3 leftBorder = new Vector3(0f, 0f, 0f);

            for (float i = leftBorder.y; i < leftBorder.y + Screen.height; i += step)
                _objectsPool.Get().transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, i, 0f));

            Vector3 rightBorder = new Vector3(Screen.width, 0f, 0f);

            for (float i = rightBorder.y; i < rightBorder.y + Screen.height; i += step)
                _objectsPool.Get().transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, i, 0f));
        }

        private Vector3 GetRandomPointOffscreen()
        {
            float posX = Random.Range(-MaxSpawnOffScreenOffset, Screen.width + MaxSpawnOffScreenOffset);

            float posY;

            if(posX <= 0f || posX >= Screen.width)
                posY = Random.Range(0f, Screen.height);
            else
            {
                posY = Random.Range(-MaxSpawnOffScreenOffset, MaxSpawnOffScreenOffset);

                if(posY >= 0f)
                    posY += Screen.height;
            }

            return Camera.main.ScreenToWorldPoint(new Vector3(posX, posY, 0f));
        }
    }
}
