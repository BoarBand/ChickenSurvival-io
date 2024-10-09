using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SurvivalChicken.Controllers
{
    public sealed class OpenChest : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _itemLabel;
        [SerializeField] private Image _frame;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _typeIcon;

        [Header("Rarity Frames")]
        [SerializeField] private Sprite _commonFrame;
        [SerializeField] private Sprite _rareFrame;
        [SerializeField] private Sprite _epicFrame;
        [SerializeField] private Sprite _legendaryFrame;

        [Header("Type Imgs")]
        [SerializeField] private Sprite _helmetIcon;
        [SerializeField] private Sprite _armorIcon;
        [SerializeField] private Sprite _bootsIcon;
        [SerializeField] private Sprite _attributeIcon;
        [SerializeField] private Sprite _weaponModuleIcon;
        [SerializeField] private Sprite _petIcon;
    }
}
