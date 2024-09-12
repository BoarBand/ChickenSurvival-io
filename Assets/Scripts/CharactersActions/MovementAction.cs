using UnityEngine;
using SurvivalChicken.Interfaces;

namespace SurvivalChicken.CharactersActions
{
    public class MovementAction : MonoBehaviour, IMovable
    {
        public bool CanMove { get; set; } = true;

        public void UpdateMove(Transform moveObj, Vector3 diraction, float speed)
        {
            if (!CanMove)
                return;

            moveObj.position += diraction * (speed * Time.deltaTime);
        }

        public void FixedUpdateMove(Transform moveObj, Vector3 diraction, float speed)
        {
            if (!CanMove)
                return;

            moveObj.position += diraction * (speed * Time.fixedDeltaTime);
        }
    }
}
