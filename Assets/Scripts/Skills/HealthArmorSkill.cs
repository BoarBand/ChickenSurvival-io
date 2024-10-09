using UnityEngine;
using SurvivalChicken.Structures;
using SurvivalChicken.PlayerObject;
using System.Collections;

namespace SurvivalChicken.Skills
{
    public class HealthArmorSkill : Skill
    {
        [SerializeField] private float _healthBuffInterval;
        [SerializeField] private int _healValue;

        private Coroutine _healthBuffCoroutine;

        public override void Initialize(EquipmentRarities.EquipmentRarity equipmentRarity)
        {
            base.Initialize(equipmentRarity);
        }

        public override void InvokeCommonAction()
        {
            Player.Health = (int)(Player.Health * 1.2);
        }

        public override void InvokeRareAction()
        {
            if (_healthBuffCoroutine != null)
                StopCoroutine(_healthBuffCoroutine);
            _healthBuffCoroutine = StartCoroutine(ApplyHealthBuff());
        }

        public override void InvokeEpicAction()
        {
            if (_healthBuffCoroutine != null)
                StopCoroutine(_healthBuffCoroutine);
            _healthBuffCoroutine = StartCoroutine(ApplyHealthBuff());
        }

        public override void InvokeLegendaryAction()
        {
            // empty
        }

        private IEnumerator ApplyHealthBuff()
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(_healthBuffInterval);

            while (true) 
            {
                yield return waitForSeconds; 
                Player.Heal(_healValue);
            }
        }
    }
}
