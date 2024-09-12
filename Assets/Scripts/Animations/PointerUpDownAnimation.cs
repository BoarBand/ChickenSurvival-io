using UnityEngine;

namespace SurvivalChicken.Animations
{
    public class PointerUpDownAnimation : MonoBehaviour
    {
        public void OnPointerDown()
        {
            transform.localScale = new Vector3(0.9f, 0.9f, 1f);
        }

        public void OnPointerUp()
        {
            transform.localScale = Vector3.one;
        }
    }
}
