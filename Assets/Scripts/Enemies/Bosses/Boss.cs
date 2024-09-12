using UnityEngine;
using UnityEngine.UI;
using System;
using SurvivalChicken.Structures;
using SurvivalChicken.EnemiesObject;
using SurvivalChicken.Spawner;

namespace SurvivalChicken.BossObject
{
    public abstract class Boss : Enemy
    {   
        private Slider _healthBar;

        public virtual void Initialize(Vector3 pos, Action disactiveAction, Slider slider)
        {
            base.Initialize(pos, disactiveAction);

            _healthBar = slider;

            slider.minValue = 0; 
            slider.maxValue = Parameters.Health;
            slider.value = Parameters.Health;
        }

        public override void GetDamage(Damage damage)
        {
            base.GetDamage(damage);

            _healthBar.value = Health;

            if (Health <= 0)
                BoostItemsSpawner.Instance.CreateRandomBoostItemInPoint(transform.position);
        }

        public void InvokeDisactivateActionBoss()
        {
            DisactivateAction();
            Destroy(gameObject);
        }
    }
}
