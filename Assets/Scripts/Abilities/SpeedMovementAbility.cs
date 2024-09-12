using UnityEngine;
using SurvivalChicken.Interfaces;

namespace SurvivalChicken.Abilities
{
    public class SpeedMovementAbility : Ability
    {
        private IChangeMovementSpeed _changeMovement;

        public override void Initialize()
        {
            transform.SetParent(Player.AbilitiesContainer);
            transform.localScale = Vector3.one;
            transform.localPosition = Vector3.zero;

            _changeMovement = Player.GetComponent<IChangeMovementSpeed>();

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
                    _changeMovement.ChangeMovementSpeed(5f);
                    break;
                case 2:
                    _changeMovement.ChangeMovementSpeed(10f);
                    break;
                case 3:
                    _changeMovement.ChangeMovementSpeed(15f);
                    break;
                case 4:
                    _changeMovement.ChangeMovementSpeed(20f);
                    break;
                case 5:
                    _changeMovement.ChangeMovementSpeed(25f);
                    break;
            }
        }
    }
}
