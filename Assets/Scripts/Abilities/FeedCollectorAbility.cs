using UnityEngine;
using SurvivalChicken.CollectItems.Collector;

namespace SurvivalChicken.Abilities
{
    public class FeedCollectorAbility : Ability
    {
        private FeedCollector _collector;

        public override void Initialize()
        {
            transform.SetParent(Player.AbilitiesContainer);
            transform.localScale = Vector3.one;
            transform.localPosition = Vector3.zero;

            _collector = Player.GetComponentInChildren<FeedCollector>(true);

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
                    _collector.ChangeCollectRadius(_collector.CurrentRadius * 0.6f);
                    break;
                case 2:
                    _collector.ChangeCollectRadius(_collector.CurrentRadius * 0.6f);
                    break;
                case 3:
                    _collector.ChangeCollectRadius(_collector.CurrentRadius * 0.6f);
                    break;
                case 4:
                    _collector.ChangeCollectRadius(_collector.CurrentRadius * 0.6f);
                    break;
                case 5:
                    _collector.ChangeCollectRadius(_collector.CurrentRadius * 0.6f);
                    break;
            }
        }
    }
}
