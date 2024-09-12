using UnityEngine;

namespace SurvivalChicken.Interfaces
{
    public interface IMovable
    {
        public void UpdateMove(Transform moveObj, Vector3 diraction, float speed);
        public void FixedUpdateMove(Transform moveObj, Vector3 diraction, float speed);
    }
}
