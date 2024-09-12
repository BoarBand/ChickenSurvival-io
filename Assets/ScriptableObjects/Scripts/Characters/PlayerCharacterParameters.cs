using UnityEngine;

namespace SurvivalChicken.ScriptableObjects.CharactersParameters.Player
{
    [CreateAssetMenu(fileName = "New Player", menuName = "ScriptableObjects/Characters/Player")]
    public class PlayerCharacterParameters : CharacterParameters
    {
        public int CritDamageChance;
        public int CritDamageValue;

        public AbilitiesParameters.AbilityParameters InitialAbility;
    }
}
