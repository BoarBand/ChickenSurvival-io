using UnityEngine;
using SurvivalChicken.Spawner;

namespace SurvivalChicken.EliteEnemiesObject
{
    public class EliteOgre : EliteEnemy
    {
        private PlayerSpawner _playerSpawner = PlayerSpawner.Instance;

        protected override void Move()
        {
            MoveDiraction = (_playerSpawner.CurrentPlayer.transform.position - transform.position).normalized;
            MovementAction.FixedUpdateMove(transform, MoveDiraction, Parameters.MoveSpeed);

            if (Health <= 0)
                return;
            if (MoveDiraction.x < 0f)
                transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            else if (MoveDiraction.x > 0f)
                transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
        }
    }
}
