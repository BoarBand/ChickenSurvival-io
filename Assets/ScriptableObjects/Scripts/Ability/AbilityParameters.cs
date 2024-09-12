using UnityEngine;
using SurvivalChicken.Abilities;

namespace SurvivalChicken.ScriptableObjects.AbilitiesParameters
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "ScriptableObjects/Ability")]
    public class AbilityParameters : ScriptableObject
    {
        public int Damage;
        public float RecoveryTime;
        public int Level;
        public int MaxLevel;
        public int MinLevel;

        public Sprite Icon;
        public string Title;
        public string[] DescriptionByLevel;

        public enum AbilityType
        {
            Boost,
            Weapon,
            SuperWeapon
        }
        public AbilityType Type;

        public Ability Ability;
    }
}
