using UnityEngine;
using SurvivalChicken.Interfaces;

namespace SurvivalChicken.Abilities
{
    public class CritDamageChanceAbility : Ability
    {
        private IChangeCritDamageChance _changeCritDamageChance;

        public override void Initialize()
        {
            transform.SetParent(Player.AbilitiesContainer);
            transform.localScale = Vector3.one;
            transform.localPosition = Vector3.zero;

            _changeCritDamageChance = Player.GetComponent<IChangeCritDamageChance>();

            Upgrade();
        }

        public override void Attack()
        {
            // no attack
        }

        public override void Upgrade()
        {
            if (AbilityParameters.Level < AbilityParameters.MaxLevel)
                AbilityParameters.Level++;

            switch (AbilityParameters.Level)
            {
                case 1:
                    _changeCritDamageChance.ChangeCritDamageChance(5);
                    break;
                case 2:
                    _changeCritDamageChance.ChangeCritDamageChance(10);
                    break;
                case 3:
                    _changeCritDamageChance.ChangeCritDamageChance(15);
                    break;
                case 4:
                    _changeCritDamageChance.ChangeCritDamageChance(20);
                    break;
                case 5:
                    _changeCritDamageChance.ChangeCritDamageChance(25);
                    break;
            }
        }
    }
}
