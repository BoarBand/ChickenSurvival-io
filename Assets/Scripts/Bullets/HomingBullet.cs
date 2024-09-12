using System.Collections;
using UnityEngine;
using SurvivalChicken.Interfaces;
using System;

namespace SurvivalChicken.Bullets
{
    public class HomingBullet : Bullet
    {
        private Vector3 _targetDiraction;

        protected readonly float RotateToTargetSpeed = 2f;
        protected readonly float RotationOffset = 0f;

        private Coroutine _rotateToTargetDiractionCoroutine;

        private IGetCurrentPosition _target;

        public override void Initialize(Vector3 pos, Vector3 diraction, IGetCurrentPosition target, int damage, int critChance, int critValue, float movespeed, Action disactivateAction)
        {
            base.Initialize(pos, diraction, target, damage, critChance, critValue, movespeed, disactivateAction);

            transform.eulerAngles = diraction;

            _target = target;

            if (_rotateToTargetDiractionCoroutine != null)
                StopCoroutine(_rotateToTargetDiractionCoroutine);
            _rotateToTargetDiractionCoroutine = StartCoroutine(RotateToTargetDiraction());
        }

        protected override void Move()
        {
            MoveAction.FixedUpdateMove(transform, transform.right, MoveSpeed);
        }

        private void Update()
        {
            Vector3 worldToScreenPos = Camera.main.WorldToScreenPoint(transform.position);

            if(_target != null)
            {
                _targetDiraction = _target.GetCurrentPosition() - transform.position;
                _target = null; // finilize
            }

            if (worldToScreenPos.x > Screen.width + 200f || worldToScreenPos.x < -200f ||
                worldToScreenPos.y > Screen.height + 200f || worldToScreenPos.y < -200f)
                DisactivateActionInvoke();
        }

        private IEnumerator RotateToTargetDiraction()
        {
            WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

            float progress = 0f;

            while (progress < 1f)
            {
                progress += Time.fixedDeltaTime * RotateToTargetSpeed;

                Vector3 targetRotation = new Vector3(0f, transform.eulerAngles.y,
                    Mathf.Atan2(_targetDiraction.y, _targetDiraction.x) * Mathf.Rad2Deg + RotationOffset);

                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotation), progress);

                yield return waitForFixedUpdate;
            }

            _rotateToTargetDiractionCoroutine = null;
        }
    }
}