using UnityEngine;
using SurvivalChicken.PlayerObject;
using SurvivalChicken.EnemiesObject;
using SurvivalChicken.Bullets;
using SurvivalChicken.Tools.Pool;
using EpicToonFX;

namespace SurvivalChicken.Abilities
{
    public class ShotgunAbility : Ability
    {
        [SerializeField] private SpriteRenderer _wing;
        [SerializeField] private SpriteRenderer _wing2;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Bullet _bullet;
        [SerializeField] private ParticleSystem _muzzleEffect;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private ETFXPitchRandomizer _pitchRandomizer; 
        [SerializeField] private Vector3 _abilityLocalPosition;

        private ObjectsPool<Bullet> _objectsPool;
        
        private int _damage;

        private readonly uint InitAmount = 0;
        private readonly float BulletMoveSpeed = 14f;
        private readonly float BulletLifetime = 0.2f;
        private readonly float AngleSpread = 20f;

        public override void Initialize()
        {
            _wing.sprite = Player.WingSprite;
            _wing2.sprite = Player.WingSprite;

            transform.SetParent(Player.AbilitiesContainer);
            transform.localScale = Vector3.one;
            transform.localPosition = _abilityLocalPosition;

            _objectsPool = new ObjectsPool<Bullet>(Create, Add, Get, InitAmount);

            Upgrade();

            _damage = AbilityParameters.Damage;
        }

        public override void Attack()
        {
            if (AreaCounter.Amount <= 0 || AreaCounter == null)
                return;

            _objectsPool.Get();
            _objectsPool.Get();
            _objectsPool.Get();
            _objectsPool.Get();
            _objectsPool.Get();

            _muzzleEffect.Play();
            _audioSource.Play();

            _pitchRandomizer.ChangePitch(_audioSource);
        }

        public override void Upgrade()
        {
            if (AbilityParameters.Level < AbilityParameters.MaxLevel)
                AbilityParameters.Level++;

            switch (AbilityParameters.Level)
            {
                case 1:
                    _damage = AbilityParameters.Damage;
                    break;
                case 2:
                    _damage += _damage / 2;
                    break;
                case 3:
                    _damage += _damage / 2;
                    break;
                case 4:
                    _damage += _damage / 2;
                    break;
                case 5:
                    _damage += _damage / 2;
                    break;
            }
        }

        private void FixedUpdate()
        {
            Enemy enemy = AreaCounter.GetTheClosestEnemyFromArea();

            LookAtEmeny(enemy);
        }

        private Bullet Create()
        {
            return Instantiate(_bullet, _shootPoint.transform.position, Quaternion.identity);
        }

        private void Add(Bullet bullet)
        {
            bullet.Disactivate();
        }

        private void Get(Bullet bullet)
        {
            float rotZ = transform.eulerAngles.z + Random.Range(-AngleSpread, AngleSpread);

            bullet.Initialize(_shootPoint.transform.position,
                new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotZ),
                _damage,
                Player.CritDamageChance,
                Player.CritDamageValue, BulletLifetime, BulletMoveSpeed,
                () => _objectsPool.Add(bullet));
        }
    }
}

