using UnityEngine;
using SurvivalChicken.ScriptableObjects.EquipmentsParameters;

namespace SurvivalChicken.Controllers
{
    public class EquipmentItemInfo : MonoBehaviour
    {
        [field: SerializeField] public EquipmentParameters EquipmentParameters { get; private set; }

        public void Initialize(EquipmentParameters equipmentParameters)
        {
            EquipmentParameters = equipmentParameters;

            gameObject.SetActive(true);
        }
    }
}
