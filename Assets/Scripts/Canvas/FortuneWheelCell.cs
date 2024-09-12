using UnityEngine.UI;
using UnityEngine;
using SurvivalChicken.ScriptableObjects.AbilitiesParameters;

namespace SurvivalChicken.FortuneWheel.Cell
{
    public class FortuneWheelCell : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _selectImage;

        public AbilityParameters SelectedAbility { get; private set; }

        public void Initialize(AbilityParameters ability)
        {
            SelectedAbility = ability;

            _icon.sprite = ability.Icon;

            DisableSelect();
        }

        public void EnableSelect()
        {
            _selectImage.gameObject.SetActive(true);
        }

        public void DisableSelect()
        {
            _selectImage.gameObject.SetActive(false);
        }
    }

}