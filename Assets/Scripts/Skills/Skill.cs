using UnityEngine;
using SurvivalChicken.Structures;
using SurvivalChicken.Spawner;
using SurvivalChicken.PlayerObject;

namespace SurvivalChicken.Skills
{
    public abstract class Skill : MonoBehaviour
    {
        protected Player Player;

        protected EquipmentRarities.EquipmentRarity EquipmentRarity;

        public virtual void Initialize(EquipmentRarities.EquipmentRarity equipmentRarity)
        {
            EquipmentRarity = equipmentRarity;
            Player = PlayerSpawner.Instance.CurrentPlayer;

            transform.SetParent(Player.SkillsContainer);
            transform.localScale = Vector3.one;
            transform.localPosition = Vector3.zero;
        }
    }
}