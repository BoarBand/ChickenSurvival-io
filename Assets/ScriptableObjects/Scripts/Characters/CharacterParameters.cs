using UnityEngine;

namespace SurvivalChicken.ScriptableObjects.CharactersParameters
{
    public abstract class CharacterParameters : ScriptableObject
    {
        public int Health;
        public int Damage;
        public float MoveSpeed;
    }
}
