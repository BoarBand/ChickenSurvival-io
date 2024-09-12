using System.Collections;
using System;
using UnityEngine;

namespace SurvivalChicken.Bullets
{
    public class FlyingEggBullet : Bullet
    {
        [SerializeField] private Explosion _explosion;

        private Coroutine _lifeDuractionCoroutine;

        public override void Initialize(Vector3 pos, Vector3 diraction, int damage, int critChance, int critValue, float lifetime, float movespeed, Action disactivateAction)
        {
            base.Initialize(pos, diraction, damage, critChance, critValue, lifetime, movespeed, () => {
                CreateExplosion();
                disactivateAction();
            });

            if (_lifeDuractionCoroutine != null)
                StopCoroutine(_lifeDuractionCoroutine);
            _lifeDuractionCoroutine = StartCoroutine(LifeDuraction(diraction));
        }

        protected override void Move()
        {
            
        }

        public void CreateExplosion()
        {
            Explosion explosion = Instantiate(_explosion);
            explosion.Initialize(Damage, transform.position, 1 << 8);
        }

        private IEnumerator LifeDuraction(Vector3 target)
        {
            WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

            while (transform.position.y > target.y)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, MoveSpeed);
                yield return waitForFixedUpdate;
            }

            DisactivateActionInvoke();

            _lifeDuractionCoroutine = null;
        }
    }

}