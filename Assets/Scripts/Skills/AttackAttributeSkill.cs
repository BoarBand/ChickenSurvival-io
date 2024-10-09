using UnityEngine;
using SurvivalChicken.Structures;
using SurvivalChicken.Controllers;
using System.Collections;

namespace SurvivalChicken.Skills
{
    public class AttackAttributeSkill : Skill
    {
        [SerializeField] private float _damageBuffDuration;
        [SerializeField] private float _increasePercent;
        [SerializeField] private int _neededKills;

        private Coroutine _killsCheckCoroutine;
        private Coroutine _applyDamageBuffCoroutine;

        public override void Initialize(EquipmentRarities.EquipmentRarity equipmentRarity)
        {
            base.Initialize(equipmentRarity);
        }

        public override void InvokeCommonAction()
        {
        }

        public override void InvokeRareAction()
        {
        }

        public override void InvokeEpicAction()
        {
            if (_killsCheckCoroutine != null)
                StopCoroutine(_killsCheckCoroutine);

            _killsCheckCoroutine = StartCoroutine(KillsCheck());
        }

        public override void InvokeLegendaryAction()
        {
            // empty
        }

        private IEnumerator KillsCheck()
        {
            while (true)
            {
                yield return new WaitUntil(() => (StatisticsView.Instance.KillsAmount % _neededKills == 0) && _applyDamageBuffCoroutine == null);

                if (_applyDamageBuffCoroutine != null)
                    StopCoroutine(_applyDamageBuffCoroutine);

                _applyDamageBuffCoroutine = StartCoroutine(ApplyDamageBuff());
            }
        }

        private IEnumerator ApplyDamageBuff()
        {
            int temporaryDamage = Player.Damage;
            Player.Damage = (int)(Player.Damage * _increasePercent);

            yield return new WaitForSeconds(_damageBuffDuration);

            Player.Damage = temporaryDamage;

            _applyDamageBuffCoroutine = null;
        }
    }
}

