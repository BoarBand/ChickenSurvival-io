using System;
using UnityEngine;

namespace SurvivalChicken.CollectItems.BoostItems
{
    public class FortuneWheelBoostItem : BoostItem
    {
        private Action _pickupAction;

        public override void Initialize(Vector3 pos, Quaternion rot, Action action)
        {
            transform.SetPositionAndRotation(pos, rot);

            _pickupAction = action;
        }

        public override void Pickup()
        {
            _pickupAction?.Invoke();
            base.Pickup();
        }
    }
}