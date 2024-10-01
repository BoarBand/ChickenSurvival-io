using UnityEngine;
using UnityEngine.U2D;
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

        [SerializeField] private Image _playGameImage;
        [SerializeField] private Image _inventoryImage;
        [SerializeField] private Image _storeImage; 
        
        [SerializeField] private Image _productsContianerImage;
        [SerializeField] private Image _dailyShopImage;

        [SerializeField] private Sprite _activeSprite;
        [SerializeField] private Sprite _inactiveSprite;

        public void Update()
        {
            UpdateImage(_playGameWindowCanvas, _playGameImage);
            UpdateImage(_inventoryWindowCanvas, _inventoryImage);
            UpdateImage(_storeWindowCanvas, _storeImage);

            UpdateImage(_productsContianer, _productsContianerImage);
            UpdateImage(_dailyShopContianer, _dailyShopImage);
        }

        private void UpdateImage(GameObject windowCanvas, Image image)
        {
            if (windowCanvas.activeSelf)
            {
                image.sprite = _activeSprite;
            }
            else
            {
                image.sprite = _inactiveSprite; 
            }
        }

        public void InventoryWindowActive()
        {
            _playGameWindowCanvas.SetActive(false);
            _storeWindowCanvas.SetActive(false);
            _inventoryWindowCanvas.SetActive(true);
        }

        public void PlayGameWindowActive()
        {
            _playGameWindowCanvas.SetActive(true);
            _storeWindowCanvas.SetActive(false);
            _inventoryWindowCanvas.SetActive(false);
        }

        public void StoreWindowActive()
        {
            _playGameWindowCanvas.SetActive(false);
            _storeWindowCanvas.SetActive(true);
            _inventoryWindowCanvas.SetActive(false);
        }      
        
        public void ProductsContianerActive()
        {
            _productsContianer.SetActive(true);
            _dailyShopContianer.SetActive(false);
        }    
        
        public void DailyShopContianerActive()
        {
            _productsContianer.SetActive(false);
            _dailyShopContianer.SetActive(true);
        }
    }
}