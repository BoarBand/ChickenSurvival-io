using UnityEngine;

namespace SurvivalChicken.Animations
{
    public class RotateAnimation : MonoBehaviour
    {
        [SerializeField] private float _diraction;

        private void FixedUpdate()
        {
            transform.Rotate(new Vector3(0f, 0f, _diraction * Time.fixedDeltaTime));
        }
    }
}
