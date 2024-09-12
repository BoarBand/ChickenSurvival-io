using System.Collections;
using UnityEngine;
using System;

namespace SurvivalChicken.Bullets
{
    public class BombBullet : Bullet
    {
        [SerializeField] private Explosion _explosion;

        private Coroutine _lifeDuractionCoroutine;

        public override void Initialize(Vector3 pos, Vector3 diraction, int damage, int critChance, int critValue, float lifetime, float movespeed, Action disactivateAction)
        {
            base.Initialize(pos, diraction, damage, critChance, critValue, lifetime, movespeed, () => {
                CreateExplosion();
                disactivateAction();
            });

            transform.eulerAngles = diraction;

            if (_lifeDuractionCoroutine != null)
                StopCoroutine(_lifeDuractionCoroutine);
            _lifeDuractionCoroutine = StartCoroutine(LifeDuraction());
        }

        protected override void Move()
        {
            MoveAction.FixedUpdateMove(transform, transform.right, MoveSpeed);
        }

        public void CreateExplosion()
        {
            Explosion explosion = Instantiate(_explosion);
            explosion.Initialize(Damage, transform.position, 1 << 8);
        }

        private IEnumerator LifeDuraction()
        {
            yield return new WaitForSeconds(LifeTime);
            DisactivateActionInvoke();
        }
    }
}
