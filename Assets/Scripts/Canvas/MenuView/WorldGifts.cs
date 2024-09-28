using UnityEngine;
using UnityEngine.UI;

namespace SurvivalChicken.Controllers
{
    public class WorldGifts : MonoBehaviour
    {
        [SerializeField] private Sprite _openGift;
        [SerializeField] private Sprite _closeGift;
        [SerializeField] private Image[] _giftImgs;
    }
}