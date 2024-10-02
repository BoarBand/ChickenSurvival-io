using UnityEngine;
using SurvivalChicken.ScriptableObjects.EquipmentsParameters;

namespace SurvivalChicken.Controllers
{
    public class EquipmentItemInfo : MonoBehaviour
    {
        public void Initialize(EquipmentParameters equipmentParameters)
        {
            gameObject.SetActive(true);
        }
    }
}
