using UnityEngine;
using System;
using SurvivalChicken.Structures;
using SurvivalChicken.EnemiesObject;

namespace SurvivalChicken.EliteEnemiesObject
{
    public abstract class EliteEnemy : Enemy
    {
        public override void Initialize(Vector3 pos, Action disactiveAction)
        {
            base.Initialize(pos, disactiveAction);

            BoostItemSpawnChance = 0.005f;
        }

        public override void GetDamage(Damage damage)
        {
            base.GetDamage(damage);
        }
    }
}
