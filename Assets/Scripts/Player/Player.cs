using System;
using UnityEngine;
using UnityEngine.UI;
using SurvivalChicken.ScriptableObjects.CharactersParameters.Player;
using SurvivalChicken.Interfaces;
using SurvivalChicken.Structures;
using SurvivalChicken.Skills;

namespace SurvivalChicken.PlayerObject
{
    public class Player : MonoBehaviour, IGetDamagable, IChangeCritDamageValue, IChangeCritDamageChance, IGetCurrentPosition
    {
        [SerializeField] private Slider _hpBar;

        [field: SerializeField] public Sprite WingSprite { get; private set; }
        [field: SerializeField] public Transform AbilitiesContainer { get; private set; }
        [field: SerializeField] public Transform SkillsContainer { get; private set; }
        [field: SerializeField] public PlayerCharacterParameters PlayerParameters { get; private set; }

        private event Action DiedAction;

        private int _maxHealth;

        public int Health { get; set; }
        public int CritDamageValue { get; private set; }
        public int CritDamageChance { get; private set; }

        private void Awake()
        {
            Health = PlayerParameters.Health;
            CritDamageChance = PlayerParameters.CritDamageChance;
            CritDamageValue = PlayerParameters.CritDamageValue;
        }

        public void Initialize(Action diedAction)
        {
            DiedAction = diedAction;

            AddSkillsToPlayer();

            _maxHealth = Health;
        }

        public void GetDamage(Damage damage)
        {
            Health -= damage.TotalDamage;

            if(Health <= 0)
            {
                DiedAction?.Invoke();
                return;
            }

            UpdateSliderView();
        }

        public void Heal(int value)
        {
            Health += value;

            if (Health >= PlayerParameters.Health)
                Health = PlayerParameters.Health;

            UpdateSliderView();
        }

        private void UpdateSliderView()
        {
            _hpBar.minValue = 0f;
            _hpBar.maxValue = _maxHealth;

            _hpBar.value = Health;
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

        private void AddSkillsToPlayer()
        {
            if (PlayerParameters == null)
                return;

            if (PlayerParameters.HelmetEquipment != null)
                if (PlayerParameters.HelmetEquipment.Skill != null)
                {
                    Skill skill = Instantiate(PlayerParameters.HelmetEquipment.Skill);
                    skill.Initialize(PlayerParameters.HelmetEquipment.EquipmentRarity);
                }

            if(PlayerParameters.ArmorEquipment != null)
                if (PlayerParameters.ArmorEquipment.Skill != null)
                {
                    Skill skill = Instantiate(PlayerParameters.ArmorEquipment.Skill);
                    skill.Initialize(PlayerParameters.ArmorEquipment.EquipmentRarity);
                }

            if (PlayerParameters.BootsEquipment != null)
                if (PlayerParameters.BootsEquipment.Skill != null)
                {
                    Skill skill = Instantiate(PlayerParameters.BootsEquipment.Skill);
                    skill.Initialize(PlayerParameters.BootsEquipment.EquipmentRarity);
                }

            if (PlayerParameters.AttributeEquipment != null)
                if (PlayerParameters.AttributeEquipment.Skill != null)
                {
                    Skill skill = Instantiate(PlayerParameters.AttributeEquipment.Skill);
                    skill.Initialize(PlayerParameters.AttributeEquipment.EquipmentRarity);
                }
        }
    }
}
