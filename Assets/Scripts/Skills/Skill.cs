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

            switch (equipmentRarity)
            {
                case EquipmentRarities.EquipmentRarity.Common:
                    InvokeCommonAction();
                    break;
                case EquipmentRarities.EquipmentRarity.Rare:
                    InvokeCommonAction();
                    InvokeRareAction();
                    break;
                case EquipmentRarities.EquipmentRarity.Epic:
                    InvokeCommonAction();
                    InvokeRareAction();
                    InvokeEpicAction();
                    break;
                case EquipmentRarities.EquipmentRarity.Legendary:
                    InvokeCommonAction();
                    InvokeRareAction();
                    InvokeEpicAction();
                    InvokeLegendaryAction();
                    break;
                default:
                    return;
            }
        }

        public abstract void InvokeCommonAction();
        public abstract void InvokeRareAction();
        public abstract void InvokeEpicAction();
        public abstract void InvokeLegendaryAction();
    }
}