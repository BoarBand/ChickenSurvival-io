using UnityEngine;
using SurvivalChicken.Structures;
using SurvivalChicken.PlayerObject;
using System.Collections;
using SurvivalChicken.Controllers;

namespace SurvivalChicken.Skills
{
    public class HealthArmorSkill : Skill
    {
        [SerializeField] private float _healthBuffIntervalRare;
        [SerializeField] private float _healthBuffIntervalLegendary;
        [SerializeField] private int _healValue;

        private int _maxHealth;

        private Coroutine _healthBuffCoroutine;

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
            if (_healthBuffCoroutine != null)
                StopCoroutine(_healthBuffCoroutine);

            _healthBuffCoroutine = StartCoroutine(ApplyHealthBuffRare());
        }

        public override void InvokeEpicAction()
        {
            Player.Health = (int)(Player.Health * 1.4);
        }

        public override void InvokeLegendaryAction()
        {
            //empty
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
