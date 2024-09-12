using UnityEngine;
using SurvivalChicken.Interfaces;

namespace SurvivalChicken.Abilities
{
    public class CritDamageValueAbility : Ability
    {
        private IChangeCritDamageValue _changeCritDamageValue;

        public override void Initialize()
        {
            transform.SetParent(Player.AbilitiesContainer);
            transform.localScale = Vector3.one;
            transform.localPosition = Vector3.zero;

            _changeCritDamageValue = Player.GetComponent<IChangeCritDamageValue>();

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
                    _changeCritDamageValue.ChangeCritDamageValue(20);
                    break;
                case 2:
                    _changeCritDamageValue.ChangeCritDamageValue(30);
                    break;
                case 3:
                    _changeCritDamageValue.ChangeCritDamageValue(40);
                    break;
                case 4:
                    _changeCritDamageValue.ChangeCritDamageValue(50);
                    break;
                case 5:
                    _changeCritDamageValue.ChangeCritDamageValue(80);
                    break;
            }
        }
    }
}
