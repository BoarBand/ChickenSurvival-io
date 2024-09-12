using UnityEngine;
using SurvivalChicken.ScriptableObjects.AbilitiesParameters;
using UnityEngine.UI;
using TMPro;

namespace SurvivalChicken.Abilities.Card
{
    public class AbilityView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _titleTxt;
        [SerializeField] private TextMeshProUGUI _descriptionTxt;
        [SerializeField] private Image[] _stars;

        public void Initialize(AbilityParameters abilityParameters)
        {
            _icon.sprite = abilityParameters.Icon;
            _titleTxt.text = abilityParameters.Title;
            _descriptionTxt.text = abilityParameters.DescriptionByLevel[abilityParameters.Level - 1];

            for (int j = 0; j < 5; j++)
            {
                if (j < abilityParameters.Level)
                {
                    _stars[j].gameObject.SetActive(true);
                }
                else
                    _stars[j].gameObject.SetActive(false);
            }

            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
