using UnityEngine;

namespace SurvivalChicken.ScriptableObjects.CharactersParameters.Enemy
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObjects/Characters/Enemy")]
    public class EnemyCharacterParameters : CharacterParameters
    {
        public int Damage;
        public float AttackFrequency;
    }
}
