using System;
using System.Collections.Generic;
using SurvivalChicken.Controllers;
using UnityEngine;
using SurvivalChicken.EliteEnemiesObject;
using Random = UnityEngine.Random;
using System.Collections;
using SurvivalChicken.Tools.Pool;

namespace SurvivalChicken.Spawner
{
    public sealed class EliteEnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<EliteEnemy> _availableEliteEnemies = new List<EliteEnemy>();

        [SerializeField] private TimerView _timer;

        private ObjectsPool<EliteEnemy> _objectsPool;

        private Dictionary<int, Action> _actionsByTime = new Dictionary<int, Action>();

        private readonly float MaxSpawnOffScreenOffset = 150f;
        private readonly uint InitEnemiesAmount = 0;

        private Coroutine _waitForCreateEliteEnemyCoroutine;

        public void Initialize()
        {
            _objectsPool = new ObjectsPool<EliteEnemy>(Create, Add, Get, InitEnemiesAmount);

            _actionsByTime.Add(30, () => CreateEliteEnemy());
            _actionsByTime.Add(35, () => CreateEliteEnemy());

            if (_waitForCreateEliteEnemyCoroutine != null)
                StopCoroutine(_waitForCreateEliteEnemyCoroutine);
            _waitForCreateEliteEnemyCoroutine = StartCoroutine(WaitForCreateEliteEnemy());
        }

        private IEnumerator WaitForCreateEliteEnemy()
        {
            IEnumerator<int> timeMarks = _actionsByTime.Keys.GetEnumerator();

            while(timeMarks.MoveNext())
            {
                int currentMark = timeMarks.Current;

                yield return new WaitUntil(() => _timer.Time >= currentMark);
                
                if(_actionsByTime.TryGetValue(currentMark, out Action action))
                    action?.Invoke();
            }

            _waitForCreateEliteEnemyCoroutine = null;
        }

        public void CreateEliteEnemy()
        {
            _objectsPool.Get();
        }

        private EliteEnemy Create()
        {
            return Instantiate(_availableEliteEnemies[Random.Range(0, _availableEliteEnemies.Count)]);
        }

        private void Add(EliteEnemy enemy)
        {
            enemy.Disactivate();
        }

        private void Get(EliteEnemy eliteEnemy)
        {
            eliteEnemy.Initialize(GetRandomPointOffscreen(), () => {
                if (StatisticsView.Instance != null)
                    StatisticsView.Instance.IncreaseKillsAmount();
                _objectsPool.Add(eliteEnemy);
            });
        }

        public void ClearEnemies()
        {
            _objectsPool.AddAll();
        }

        private Vector3 GetRandomPointOffscreen()
        {
            float posX = Random.Range(-MaxSpawnOffScreenOffset, Screen.width + MaxSpawnOffScreenOffset);

            float posY;

            if (posX <= 0f || posX >= Screen.width)
                posY = Random.Range(0f, Screen.height);
            else
            {
                posY = Random.Range(-MaxSpawnOffScreenOffset, MaxSpawnOffScreenOffset);

                if (posY >= 0f)
                    posY += Screen.height;
            }

            return Camera.main.ScreenToWorldPoint(new Vector3(posX, posY, 0f));
        }
    }
}
