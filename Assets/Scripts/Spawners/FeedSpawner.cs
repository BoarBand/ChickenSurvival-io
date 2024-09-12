using System.Collections.Generic;
using UnityEngine;
using SurvivalChicken.Tools.Pool;
using SurvivalChicken.CollectItems.Feed;
using SurvivalChicken.CollectItems.Collector;
using SurvivalChicken.Controllers;
using System.Collections;

namespace SurvivalChicken.Spawner
{
    public sealed class FeedSpawner : MonoBehaviour
    {
        public static FeedSpawner Instance;

        [SerializeField] private Feed[] _feed;
        [SerializeField] private TimerView _timer;

        private List<Feed> _currentFeeds = new List<Feed>();

        private ObjectsPool<Feed> _objectsPool;

        private FeedCollector _feedCollector;

        private Coroutine _waitByTimerCoroutine;

        private readonly uint InitAmount = 25;
        private readonly float TimeToIncreaseFeed = 240f;

        public void Initialize()
        {
            Instance = this;

            _currentFeeds.Add(_feed[0]);

            _objectsPool = new ObjectsPool<Feed>(Create, Add, Get, InitAmount);
            _objectsPool.GetAll();

            _feedCollector = PlayerSpawner.Instance.CurrentPlayer.GetComponentInChildren<FeedCollector>(true);

            if (_waitByTimerCoroutine != null)
                StopCoroutine(_waitByTimerCoroutine);
            _waitByTimerCoroutine = StartCoroutine(WaitByTimer());
        }

        private IEnumerator WaitByTimer()
        {
            WaitUntil waitUntil = new WaitUntil(() => _timer.Time >= TimeToIncreaseFeed);

            yield return waitUntil;

            _currentFeeds.Add(_feed[1]);

            for(int i = 0; i < InitAmount; i++)
            {
                Feed feed = Create();
                _objectsPool.Add(feed);
            }

            _waitByTimerCoroutine = null;
        }

        private Feed Create()
        {
            return Instantiate(_feed[Random.Range(0, _currentFeeds.Count)]);
        }

        private void Add(Feed item)
        {
            item.InvokePickup(_feedCollector);
        }

        private void Get(Feed item)
        {
            item.Initialize(() => {
                ProgressBarAbilities.Instance.IncreaseProgressBar(item.BoostValue);
                _objectsPool.Add(item);
            });
        }

        public void CollectAll()
        {
            _objectsPool.AddAll();
        }

        public Feed SpawnFeedByPos() 
        {
            return _objectsPool.Get();
        }
    }
}