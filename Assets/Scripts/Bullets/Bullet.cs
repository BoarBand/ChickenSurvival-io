using UnityEngine;
using SurvivalChicken.CharactersActions;
using SurvivalChicken.Interfaces;
using SurvivalChicken.Spawner;
using System;
using SurvivalChicken.Structures;

namespace SurvivalChicken.Bullets
{
    public abstract class Bullet : MonoBehaviour
    {
        protected float LifeTime;
        protected float MoveSpeed;
        protected Vector3 Diraction;

        protected MovementAction MoveAction = new MovementAction();

        public Damage Damage;

        private event Action _disactivateAction;

        public virtual void Initialize(Vector3 pos, Vector3 diraction, int damage, int critChance, int critValue, float lifetime, float movespeed, Action disactivateAction)
        {
            Damage = new Damage(damage, critChance, critValue);
            LifeTime = lifetime;
            MoveSpeed = movespeed;
            Diraction = diraction;

            transform.position = pos;
            _disactivateAction = disactivateAction;

            gameObject.SetActive(true);
        }

        public virtual void Initialize(Vector3 pos, Quaternion rot, int damage, int critChance, int critValue)
        {
            Damage = new Damage(damage, critChance, critValue);

            transform.SetLocalPositionAndRotation(pos, rot);

            gameObject.SetActive(true);
        }

        public virtual void Initialize(Vector3 pos, Vector3 diraction, IGetCurrentPosition target, int damage, int critChance, int critValue, float movespeed, Action disactivateAction)
        {
            Damage = new Damage(damage, critChance, critValue);
            MoveSpeed = movespeed;
            Diraction = diraction;

            transform.position = pos;
            _disactivateAction = disactivateAction;

            gameObject.SetActive(true);
        }

        public void Disactivate()
        {
            gameObject.SetActive(false);
        }

        public void DisactivateActionInvoke()
        {
            _disactivateAction();
        }

        protected abstract void Move();

        private void FixedUpdate()
        {
            Move();
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out IGetDamagable getDamagable))
            {
                DamageNumberSpawner.Instance.SpawnDamageNumber(collision.transform.position, Damage.TotalDamage, Damage.IsCritical);
                getDamagable.GetDamage(Damage);
            }
        }

    }
}
