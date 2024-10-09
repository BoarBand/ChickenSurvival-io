using UnityEngine;
using SurvivalChicken.Structures;
using SurvivalChicken.Controllers;
using SurvivalChicken.Spawner;
using SurvivalChicken.PlayerObject;
using System.Collections;

namespace SurvivalChicken.Skills
{
    public class ArmorSkill : Skill
    {
        private readonly float _healthBuffInterval = 60f;
        private Coroutine _healthBuffCoroutine;

        public override void Initialize(EquipmentRarities.EquipmentRarity equipmentRarity)
        {
            base.Initialize(equipmentRarity);

            switch (equipmentRarity)
            {
                case EquipmentRarities.EquipmentRarity.Common:
                    Player.Health = (int)(Player.Health * 1.2);
                    break;
                case EquipmentRarities.EquipmentRarity.Rare:
                    _healthBuffCoroutine = StartCoroutine(ApplyHealthBuff());
                    break;  
                case EquipmentRarities.EquipmentRarity.Epic:
                    _healthBuffCoroutine = StartCoroutine(ApplyHealthBuff());
                    break;
            }
        }

        private IEnumerator ApplyHealthBuff()
        {
            while (true) 
            {
                Player.Health += 50;
                yield return new WaitForSeconds(_healthBuffInterval); 
            }
        }

        public void StopHealthBuff()
        {
            if (_healthBuffCoroutine != null)
            {
                StopCoroutine(_healthBuffCoroutine);
                _healthBuffCoroutine = null;
            }
        }
    }
}
