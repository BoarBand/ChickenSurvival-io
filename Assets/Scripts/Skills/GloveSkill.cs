using UnityEngine;
using SurvivalChicken.Structures;
using SurvivalChicken.Controllers;
using SurvivalChicken.Spawner;
using SurvivalChicken.PlayerObject;
using System.Collections;

namespace SurvivalChicken.Skills
{
    public class GloveSkill : Skill
    {
        private Damage _damage;
        private readonly float _damageBuffDuration = 3f;
        private Coroutine _deathCheckCoroutine;

        public override void Initialize(EquipmentRarities.EquipmentRarity equipmentRarity)
        {
            base.Initialize(equipmentRarity);

            switch (equipmentRarity)
            {
                case EquipmentRarities.EquipmentRarity.Rare:
                    _damage.TotalDamage = (int)(_damage.TotalDamage * 1.1);
                    break;
                case EquipmentRarities.EquipmentRarity.Epic:
                    _deathCheckCoroutine = StartCoroutine(DeathCheck());
                    break;
                case EquipmentRarities.EquipmentRarity.Legendary:
                    _damage.TotalDamage = (int)(_damage.TotalDamage * 1.15);
                    break;
            }
        }

        private IEnumerator DeathCheck()
        {
            while (StatisticsView.Instance.KillsAmount < 200)
            {
                yield return null;
            }

            StartCoroutine(ApplyDamageBuff());
        }

        private IEnumerator ApplyDamageBuff()
        {
            int temporaryDamage = _damage.TotalDamage;
            _damage.TotalDamage = (int)(_damage.TotalDamage * 1.1);

            yield return new WaitForSeconds(_damageBuffDuration);

            _damage.TotalDamage = temporaryDamage;
        }

        public void StopDeathCheck()
        {
            if (_deathCheckCoroutine != null)
            {
                StopCoroutine(_deathCheckCoroutine);

                _deathCheckCoroutine = null;
            }
        }
    }
}

