using UnityEngine;
using SurvivalChicken.Spawner;
using SurvivalChicken.Controllers;
using SurvivalChicken.SaveLoadDatas;

namespace SurvivalChicken.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        public static Bootstrap Instance;

        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private FeedSpawner _feedSpawner;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private BossSpawner _bossSpawner;
        [SerializeField] private EliteEnemySpawner _eliteEnemySpawner;
        [SerializeField] private DamageNumberSpawner _damageNumberSpawner;
        [SerializeField] private ProgressBarAbilities _progressBarAbilities;
        [SerializeField] private TimerView _timerView;
        [SerializeField] private StatisticsView _statisticsView;
        [SerializeField] private AbilitiesSelector _abilitiesSelector;
        [SerializeField] private BoostItemsSpawner _boostItemsSpawner;
        [SerializeField] private SaveLoadData _saveLoadData;

        private void Awake()
        {
            _saveLoadData.Initialize();
            _playerSpawner.Initialize();
            _progressBarAbilities.Initialize();
            _feedSpawner.Initialize();
            _boostItemsSpawner.Initialize();
            _statisticsView.Initialize();
            _enemySpawner.Initialize();
            _bossSpawner.Initialize();
            _eliteEnemySpawner.Initialize();
            _damageNumberSpawner.Initialize();
            _timerView.Initialize();
            _abilitiesSelector.ResetAllAbilities();
            _abilitiesSelector.Initialize();
        }
    }
}
