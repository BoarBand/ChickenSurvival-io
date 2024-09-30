using System;
using UnityEngine;
using SurvivalChicken.Spawner;

namespace SurvivalChicken.CollectItems.BoostItems
{
    public class MedkitBoostItem : BoostItem
    {
        [SerializeField] private int _healValue;

        private PlayerSpawner _playerSpanwer;

        public override void Initialize(Vector3 pos, Quaternion rot, Action action)
        {
            transform.SetPositionAndRotation(pos, rot);

            _playerSpanwer = PlayerSpawner.Instance;
        }

        public override void Pickup()
        {
            _playerSpanwer.CurrentPlayer.Heal(_healValue);

            base.Pickup();
        }
    }
}
