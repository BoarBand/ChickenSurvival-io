using UnityEngine;
using SurvivalChicken.Spawner;

namespace SurvivalChicken.EnemiesObject
{
    public class CommonBat : Enemy
    {
        private PlayerSpawner _playerSpawner = PlayerSpawner.Instance;

        protected override void Move()
        {
            MoveDiraction = (_playerSpawner.CurrentPlayer.transform.position - transform.position).normalized;
            MovementAction.FixedUpdateMove(transform, MoveDiraction, Parameters.MoveSpeed);

            if (Health <= 0)
                return;
            if (MoveDiraction.x < 0f)
                transform.localScale = new Vector3(1f, 1f, 1f);
            else if (MoveDiraction.x > 0f)
                transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
