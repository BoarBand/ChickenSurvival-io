using UnityEngine;
using SurvivalChicken.Structures;
using SurvivalChicken.PlayerObject;
using System.Collections;

namespace SurvivalChicken.Skills
{
    public class HealthArmorSkill : Skill
    {
        [SerializeField] private float _healthBuffIntervalRare;
        [SerializeField] private float _healthBuffIntervalLegendary;
        [SerializeField] private int _healValue;

        private int _maxHealth;

        private Coroutine _healthBuffRareCoroutine;
        private Coroutine _healthBuffLegendaryCoroutine;

        public override void Initialize(EquipmentRarities.EquipmentRarity equipmentRarity)
        {
            base.Initialize(equipmentRarity);

            _maxHealth = Player.Health;
        }

        public override void InvokeCommonAction()
        {
            Player.Health = (int)(Player.Health * 1.2);
        }

        public override void InvokeRareAction()
        {
            if (_healthBuffRareCoroutine != null)
                StopCoroutine(_healthBuffRareCoroutine);
            _healthBuffRareCoroutine = StartCoroutine(ApplyHealthBuffRare());
        }

        public override void InvokeEpicAction()
        {
            Player.Health = (int)(Player.Health * 1.4);
        }

        public override void InvokeLegendaryAction()
        {
            if (_healthBuffLegendaryCoroutine != null)
                StopCoroutine(_healthBuffLegendaryCoroutine);
            _healthBuffLegendaryCoroutine = StartCoroutine(ApplyHealthBuffLegendary());
        }

        private IEnumerator ApplyHealthBuffRare()
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(_healthBuffIntervalRare);

            while (true) 
            {
                yield return waitForSeconds; 

                Player.Heal(_healValue);
            }
        }
        
        private IEnumerator ApplyHealthBuffLegendary()
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(_healthBuffIntervalLegendary);

            while (true)
            {
                yield return new WaitUntil(() => Player.Health < _maxHealth / 2);

                while (Player.Health < _maxHealth / 2)
                {
                    yield return waitForSeconds;

                    Player.Heal(_healValue);
                }
            }
        }
    }
}
