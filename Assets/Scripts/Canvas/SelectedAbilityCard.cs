using UnityEngine;
using UnityEngine.UI;
using SurvivalChicken.ScriptableObjects.AbilitiesParameters;

namespace SurvivalChicken.Abilities.Card
{
    public class SelectedAbilityCard : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image[] _stars;

        public void Initialize(AbilityParameters abilityParameters)
        {
            _icon.sprite = abilityParameters.Icon;

            for (int i = 0; i < _stars.Length; i++)
            {
                if(i < abilityParameters.Level)
                {
                    _stars[i].gameObject.SetActive(true);
                    continue;
                }
                _stars[i].gameObject.SetActive(false);
            }

            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }

}