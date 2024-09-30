using System;
using UnityEngine;
using UnityEngine.UI;
using SurvivalChicken.ScriptableObjects.CharactersParameters.Player;
using SurvivalChicken.Interfaces;
using SurvivalChicken.Structures;

namespace SurvivalChicken.PlayerObject
{
    public class Player : MonoBehaviour, IGetDamagable, IChangeCritDamageValue, IChangeCritDamageChance, IGetCurrentPosition
    {
        [SerializeField] private Slider _hpBar;

        [field: SerializeField] public Sprite WingSprite { get; private set; }
        [field: SerializeField] public Transform AbilitiesContainer { get; private set; }
        [field: SerializeField] public PlayerCharacterParameters PlayerParameters { get; private set; }

        private event Action DiedAction;

        private int _health;

        public int CritDamageValue { get; private set; }
        public int CritDamageChance { get; private set; }

        private void Awake()
        {
            _health = PlayerParameters.Health;
            CritDamageChance = PlayerParameters.CritDamageChance;
            CritDamageValue = PlayerParameters.CritDamageValue;
        }

        public void Initialize(Action diedAction)
        {
            DiedAction = diedAction;
        }

        public void GetDamage(Damage damage)
        {
            _health -= damage.TotalDamage;

            if(_health <= 0)
            {
                DiedAction?.Invoke();
                return;
            }

            UpdateSliderView();
        }

        public void Heal(int value)
        {
            _health += value;

            if (_health >= PlayerParameters.Health)
                _health = PlayerParameters.Health;

            UpdateSliderView();
        }

        private void UpdateSliderView()
        {
            _hpBar.minValue = 0f;
            _hpBar.maxValue = PlayerParameters.Health;

            _hpBar.value = _health;
        }

        public void ChangeCritDamageValue(int amount)
        {
            CritDamageValue += amount;
        }

        public void ChangeCritDamageChance(int amount)
        {
            if (CritDamageChance >= 100)
            {
                CritDamageChance = 100;
                return;
            }
            
            CritDamageChance += amount;

            if (CritDamageChance >= 100)
                CritDamageChance = 100;
        }

        public Vector3 GetCurrentPosition()
        {
            return transform.position;
        }
    }
}
