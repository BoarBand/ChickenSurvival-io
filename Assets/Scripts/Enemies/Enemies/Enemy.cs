using UnityEngine;
using SurvivalChicken.CharactersActions;
using SurvivalChicken.Interfaces;
using System;
using SurvivalChicken.ScriptableObjects.CharactersParameters.Enemy;
using SurvivalChicken.EnemiesObject.Animations;
using SurvivalChicken.Spawner;
using SurvivalChicken.Structures;

namespace SurvivalChicken.EnemiesObject
{
    public abstract class Enemy : MonoBehaviour, IGetDamagable, IGetCurrentPosition
    {
        [HideInInspector] protected int Health;
        [HideInInspector] protected int Damage;

        [SerializeField] protected EnemyCharacterParameters Parameters;
        [SerializeField] protected DamageFlasher DamageFlasher;

        [SerializeField] private ParticleSystem _getDamageEffect;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private EnemyAnimator _animator;

        protected MovementAction MovementAction = new MovementAction();

        protected Vector3 MoveDiraction;

        protected Action DisactivateAction;

        protected float BoostItemSpawnChance = 0.005f;

        private readonly float PushForce = 200f;

        public bool CanMove
        {
            set
            {
                MovementAction.CanMove = value;
            }
        }

        public virtual void Initialize(Vector3 pos, Action disactiveAction)
        {
            Health = Parameters.Health;
            Damage = Parameters.Damage;

            _collider.enabled = true;
            _rigidbody.velocity = Vector3.zero;

            transform.position = new Vector3(pos.x, pos.y, 0f);

            DisactivateAction = disactiveAction;

            gameObject.SetActive(true);
            _animator.SetRunAnimation();
        }

        protected abstract void Move();

        private void FixedUpdate()
        {
            Move();
        }

        public void Disactivate()
        {
            gameObject.SetActive(false);
        }

        public virtual void GetDamage(Damage damage)
        {
            if (Health <= 0)
                return;

            DamageFlasher.Play();
            _getDamageEffect.Play();

            Health -= damage.TotalDamage;

            if(Health <= 0)
            {
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.AddForce(-MoveDiraction * PushForce);

                SpawnBoostItem(BoostItemSpawnChance);
                FeedSpawner.Instance.SpawnFeedByPos().transform.position = transform.position;
                DamageFlasher.ResetFlasher();
                _collider.enabled = false;
                _animator.SetDeadAnimation();
            }
        }

        protected void SpawnBoostItem(float spawnChance)
        {
            float chance = UnityEngine.Random.Range(0, 1f);

            if(chance <= spawnChance)
                BoostItemsSpawner.Instance.CreateRandomBoostItemInPoint(transform.position);
        }

        public void InvokeDisactivateAction()
        {
            DisactivateAction();
        }

        public Vector3 GetCurrentPosition()
        {
            return transform.position;
        }
    }
}
