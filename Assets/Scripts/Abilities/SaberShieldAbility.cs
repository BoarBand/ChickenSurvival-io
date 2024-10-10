using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SurvivalChicken.Bullets;

namespace SurvivalChicken.Abilities
{
    public class SaberShieldAbility : Ability
    {
        [SerializeField] private SaberBullet _bullet;
        [SerializeField] private Transform _container;
        [SerializeField] private Vector3 _abilityLocalPosition;

        private List<SaberBullet> _bullets = new List<SaberBullet>();

        private int _bulletAmounts = 2;

        private Coroutine _activeSabersCoroutine;
        
        private readonly float Radius = 1.4f;
        private readonly float AngleOffset = -90f;
        private readonly float ActiveDuraction = 5f;

        public override void Initialize()
        {
            transform.SetParent(Player.AbilitiesContainer);
            transform.localScale = Vector3.one;
            transform.localPosition = _abilityLocalPosition;

            Upgrade();

            if (_activeSabersCoroutine != null)
                StopCoroutine(_activeSabersCoroutine);
            _activeSabersCoroutine = StartCoroutine(ActiveSabers());
        }

        private void InitSpawn()
        {
            float angle = 0;
            float angleStep = 360f / _bulletAmounts;

            for (int i = 0; i < _bulletAmounts; i++)
            {
                SaberBullet bullet = Instantiate(_bullet);
                bullet.transform.SetParent(_container);
                bullet.Initialize(new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * Radius, Mathf.Sin(angle * Mathf.Deg2Rad) * Radius, 0f), Quaternion.Euler(0f, 0f, angle + AngleOffset), 
                    AbilityParameters.Damage, 
                    Player.CritDamageChance, 
                    Player.CritDamageValue);

                _bullets.Add(bullet);

                angle += angleStep;
            }
        }

        public override void Attack(){}

        public override void Upgrade()
        {
            if (AbilityParameters.Level < AbilityParameters.MaxLevel)
                AbilityParameters.Level++;

            switch (AbilityParameters.Level)
            {
                case 1:
                    _bulletAmounts = 2;
                    break;
                case 2:
                    _bulletAmounts = 4;
                    break;
                case 3:
                    _bulletAmounts = 6;
                    break;
                case 4:
                    _bulletAmounts = 8;
                    break;
                case 5:
                    _bulletAmounts = 10;
                    break;
            }

            ClearAllSabers();
            InitSpawn();
        }

        private IEnumerator ActiveSabers()
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(ActiveDuraction);

            while (true)
            {
                _container.gameObject.SetActive(true);

                yield return waitForSeconds;

                _container.gameObject.SetActive(false);

                yield return waitForSeconds;
            }
        }

        private void ClearAllSabers()
        {
            foreach (SaberBullet bullet in _bullets)
            {
                Destroy(bullet.gameObject);
            }
            _bullets.Clear();
        }
    }
}
