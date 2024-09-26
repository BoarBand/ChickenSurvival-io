using UnityEngine;

namespace SurvivalChicken.Buffs
{
    public class ClampMovementBuff : MonoBehaviour
    {
        private Vector3 _corner_up_L;
        private Vector3 _corner_down_L;
        private Vector3 _corner_up_R;
        private Vector3 _corner_down_R;

        public void Initialize(Vector3 cornerUpL, Vector3 cornerDownL, Vector3 cornerUpR, Vector3 cornerDownR)
        {
            _corner_up_L = cornerUpL;
            _corner_down_L = cornerDownL;
            _corner_up_R = cornerUpR;
            _corner_down_R = cornerDownR;
        }

        private void FixedUpdate()
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, _corner_up_L.x, _corner_up_R.x), 
                Mathf.Clamp(transform.position.y, _corner_down_R.y, _corner_up_R.y),
                0f);
        }
    }
}