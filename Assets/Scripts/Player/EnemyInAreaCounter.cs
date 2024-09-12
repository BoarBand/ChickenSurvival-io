using System.Collections.Generic;
using SurvivalChicken.EnemiesObject;
using UnityEngine;

namespace SurvivalChicken.CharactersActions
{
    public class EnemyInAreaCounter : MonoBehaviour
    {
        [SerializeField] private List<Enemy> _enemiesInArea = new List<Enemy>();

        public int Amount => _enemiesInArea.Count;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Enemy enemy))
                _enemiesInArea.Add(enemy);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Enemy enemy))
                if(_enemiesInArea.Contains(enemy))
                    _enemiesInArea.Remove(enemy);
        }

        public Enemy GetFirstEnemyFromArea()
        {
            if (_enemiesInArea.Count <= 0)
                return null;

            return _enemiesInArea[0];
        }

        public Enemy GetRandomEnemyFromArea()
        {
            if (_enemiesInArea.Count <= 0)
                return null;

            return _enemiesInArea[Random.Range(0, _enemiesInArea.Count)];
        }

        public Enemy GetTheClosestEnemyFromArea()
        {
            if (_enemiesInArea.Count <= 0)
                return null;

            Enemy closestEnemy = _enemiesInArea[0];

            foreach (Enemy enemy in _enemiesInArea)
            {
                float diff = (enemy.transform.position - transform.position).sqrMagnitude;
                if((closestEnemy.transform.position - transform.position).sqrMagnitude > diff)
                {
                    closestEnemy = enemy;
                }
            }
            return closestEnemy;
        }
    }
}
