using UnityEngine;
using DamageNumbersPro;

namespace SurvivalChicken.Spawner
{
    public sealed class DamageNumberSpawner : MonoBehaviour
    {
        public static DamageNumberSpawner Instance;

        [SerializeField] private DamageNumber _damageNumber;

        [Header("Colors")]
        [SerializeField] private Color _commonColor;
        [SerializeField] private Color _critColor;

        public void Initialize()
        {
            Instance = this;
        }

        public void SpawnDamageNumber(Vector3 pos, int number, bool isCrit)
        {
            DamageNumber damageNumber = _damageNumber.Spawn(pos, number);
            damageNumber.SetColor(isCrit ? _critColor : _commonColor);
        }
    }
}
