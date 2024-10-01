using UnityEngine;
using UnityEngine.UI;

namespace SurvivalChicken.Controllers
{
    public sealed class MenuWindowsSwitcher : MonoBehaviour
    {
        [SerializeField] private GameObject _playGameWindowCanvas;
        [SerializeField] private GameObject _inventoryWindowCanvas;
        [SerializeField] private GameObject _storeWindowCanvas;

        [SerializeField] private GameObject _productsContianer;
        [SerializeField] private GameObject _dailyShopContianer;

        [SerializeField] private Image _productsContianerImage;
        [SerializeField] private Image _dailyShopImage;

        [SerializeField] private Sprite _activeSprite;
        [SerializeField] private Sprite _inactiveSprite;

        [Header("DownImages")]
        [SerializeField] private Image _playDownImage;
        [SerializeField] private Image _inventoryDownImage;
        [SerializeField] private Image _storeDownImage;

        public void Initialize()
        {
            PlayGameWindowActivate();
        }

        private void UpdateDownImage(Image image)
        {
            _playDownImage.sprite = _inactiveSprite;
            _inventoryDownImage.sprite = _inactiveSprite;
            _storeDownImage.sprite = _inactiveSprite;

            image.sprite = _activeSprite;
        }

        public void InventoryWindowActivate()
        {
            _playGameWindowCanvas.SetActive(false);
            _storeWindowCanvas.SetActive(false);
            _inventoryWindowCanvas.SetActive(true);

            UpdateDownImage(_inventoryDownImage);
        }

        public void PlayGameWindowActivate()
        {
            _playGameWindowCanvas.SetActive(true);
            _storeWindowCanvas.SetActive(false);
            _inventoryWindowCanvas.SetActive(false);

            UpdateDownImage(_playDownImage);
        }

        public void StoreWindowActivate()
        {
            _playGameWindowCanvas.SetActive(false);
            _storeWindowCanvas.SetActive(true);
            _inventoryWindowCanvas.SetActive(false);

            UpdateDownImage(_storeDownImage);
        }      
        
        public void ProductsContianerActivate()
        {
            _productsContianer.SetActive(true);
            _dailyShopContianer.SetActive(false);
        }    
        
        public void DailyShopContianerActivate()
        {
            _productsContianer.SetActive(false);
            _dailyShopContianer.SetActive(true);
        }
    }
}