using System;
using UnityEngine;
using SurvivalChicken.Spawner;

namespace SurvivalChicken.CollectItems.BoostItems
{
    public class MagnetBoostItem : BoostItem
    {
        private FeedSpawner _feedSpawner;

        public override void Initialize(Vector3 pos, Quaternion rot, Action action)
        {
            transform.SetPositionAndRotation(pos, rot);

            _feedSpawner = FeedSpawner.Instance;
        }

        public override void Pickup()
        {
            _feedSpawner.CollectAll();

            base.Pickup();
        }
    }
}
