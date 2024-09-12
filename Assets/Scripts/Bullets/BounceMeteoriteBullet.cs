using System;
using UnityEngine;
using SurvivalChicken.Interfaces;
using SurvivalChicken.Spawner;
using SurvivalChicken.Structures;

namespace SurvivalChicken.Bullets
{
    public class BounceMeteoriteBullet : Bullet
    {
        private int _damage;
        private int _critChance;
        private int _critValue;

        public override void Initialize(Vector3 pos, Vector3 diraction, int damage, int critChance, int critValue, float lifetime, float movespeed, Action disactivateAction)
        {
            base.Initialize(pos, diraction, damage, critChance, critValue, lifetime, movespeed, disactivateAction);

            transform.eulerAngles = diraction;

            _damage = damage;
            _critChance = critChance;
            _critValue = critValue;
        }

        protected override void Move() 
        {
            MoveAction.FixedUpdateMove(transform, transform.right, MoveSpeed);

            Vector3 worldToScreenPos = Camera.main.WorldToScreenPoint(transform.position);

            if (worldToScreenPos.x > Screen.width)
            {
                transform.eulerAngles = new Vector3(0f, 0f, UnityEngine.Random.Range(95f, 170f));
            }

            if (worldToScreenPos.x < 0f)
            {
                transform.eulerAngles = new Vector3(0f, 0f, UnityEngine.Random.Range(270f, 430f));
            }

            if (worldToScreenPos.y > Screen.height)
            {
                transform.eulerAngles = new Vector3(0f, 0f, UnityEngine.Random.Range(200f, 330f));
            }

            if (worldToScreenPos.y < 0f)
            {
                transform.eulerAngles = new Vector3(0f, 0f, UnityEngine.Random.Range(20f, 160f));
            }
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IGetDamagable getDamagable))
            {
                Damage = new Damage(_damage, _critChance, _critValue);

                DamageNumberSpawner.Instance.SpawnDamageNumber(collision.transform.position, Damage.TotalDamage, Damage.IsCritical);
                getDamagable.GetDamage(Damage);
            }
        }
    }
}