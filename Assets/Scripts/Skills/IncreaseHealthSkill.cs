using UnityEngine;
using SurvivalChicken.Structures;

namespace SurvivalChicken.Skills
{
    public class IncreaseHealthSkill : Skill
    {
        public override void Initialize(EquipmentRarities.EquipmentRarity equipmentRarity)
        {
            base.Initialize(equipmentRarity);

            switch (equipmentRarity)
            {
                case EquipmentRarities.EquipmentRarity.Common:
                    Player.Health = (int)(Player.Health * 1.2);
                    break;
                case EquipmentRarities.EquipmentRarity.Rare:
                    Player.Health = (int)(Player.Health * 1.35);
                    break;
                case EquipmentRarities.EquipmentRarity.Epic:
                    Player.Health = (int)(Player.Health * 1.6);
                    break;
                case EquipmentRarities.EquipmentRarity.Legendary:
                    Player.Health = Player.Health * 2;
                    break;
            }
        }
    }
}
