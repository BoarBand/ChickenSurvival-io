using UnityEngine;
using System;

namespace SurvivalChicken.Bullets
{
    public class BulletOffscreen : Bullet
    {
        public override void Initialize(Vector3 pos, Vector3 diraction, int damage, int critChance, int critValue, float lifetime, float movespeed, Action disactivateAction)
        {
            base.Initialize(pos, diraction, damage, critChance, critValue, lifetime, movespeed, disactivateAction);

            transform.eulerAngles = diraction;
        }

        protected override void Move()
        {
            MoveAction.FixedUpdateMove(transform, transform.right, MoveSpeed);
        }

        private void Update()
        {
            Vector3 worldToScreenPos = Camera.main.WorldToScreenPoint(transform.position);

            if (worldToScreenPos.x > Screen.width || worldToScreenPos.x < 0f || 
                worldToScreenPos.y > Screen.height || worldToScreenPos.y < 0f)
                DisactivateActionInvoke();
        }
    }
}
