using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using SurvivalChicken.ScriptableObjects.AbilitiesParameters;
using SurvivalChicken.Controllers;

namespace SurvivalChicken.Abilities.Card
{
    public class AbilityCard : MonoBehaviour
    {
        [SerializeField] private AbilitiesSelector _selector;
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _abilityCardImage;
        [SerializeField] private Image _descriptionFrameImage;
        [SerializeField] private Image _descriptionFrameIcon;
        [SerializeField] private Image[] _stars;

        [Header("Colors")]
        [SerializeField] private Color _abilityCardBoostColor;
        [SerializeField] private Color _abilityCardWeaponColor;
        [SerializeField] private Color _abilityCardSuperWeaponColor;

        [Header("Icons")]
        [SerializeField] private Sprite _abilityCardBoostIcon;
        [SerializeField] private Sprite _abilityCardWeaponIcon;
        [SerializeField] private Sprite _abilityCardSuperWeaponIcon;

        private AbilityParameters _ability;

        private Tween _starAnimation;

        private readonly Color AnimationColor1 = new Color(1f, 1f, 1f, 0f);
        private readonly Color AnimationColor2 = new Color(1f, 1f, 1f, 1f);
        private readonly float AnimationDuraction = 0.6f;

        public void Initialize(AbilityParameters ability)
        {
            if (ability == null)
            {
                gameObject.SetActive(false);
                return;
            }

            _ability = ability;

            _title.text = ability.Title;
            _description.text = ability.DescriptionByLevel[ability.Level];
            _icon.sprite = ability.Icon;

            for (int j = 0; j < 5; j++)
            {
                _stars[j].color = AnimationColor2;

                if (j < ability.Level)
                {
                    _stars[j].gameObject.SetActive(true);
                }
                else
                    _stars[j].gameObject.SetActive(false);
            }

            SetCustomize(ability);

            if (ability.Level < 5)
            {
                _stars[ability.Level].gameObject.SetActive(true);
                SetAnimation(_stars[ability.Level]);
            }
            
            gameObject.SetActive(true);
        }

        private void SetCustomize(AbilityParameters parameters)
        {
            switch (parameters.Type)
            {
                case AbilityParameters.AbilityType.Boost:
                    _abilityCardImage.color = _abilityCardBoostColor;
                    _descriptionFrameImage.color = _abilityCardBoostColor;
                    _descriptionFrameIcon.sprite = _abilityCardBoostIcon;

                    foreach(Image image in _stars)
                        image.transform.parent.gameObject.SetActive(true);
                    break;
                case AbilityParameters.AbilityType.Weapon:
                    _abilityCardImage.color = _abilityCardWeaponColor;
                    _descriptionFrameImage.color = _abilityCardWeaponColor;
                    _descriptionFrameIcon.sprite = _abilityCardWeaponIcon;

                    foreach (Image image in _stars)
                        image.transform.parent.gameObject.SetActive(true);
                    break;
                case AbilityParameters.AbilityType.SuperWeapon:
                    _abilityCardImage.color = _abilityCardSuperWeaponColor;
                    _descriptionFrameImage.color = _abilityCardSuperWeaponColor;
                    _descriptionFrameIcon.sprite = _abilityCardSuperWeaponIcon;

                    foreach (Image image in _stars)
                        image.transform.parent.gameObject.SetActive(false);
                    _stars[0].transform.parent.gameObject.SetActive(true);
                    break;
            }
        }

        public void Select()
        {
            if (_ability == null)
                return;
            _selector.AddOrUpgradeAbility(_ability.Ability);
        }

        private void SetAnimation(Image img)
        {
            if (_starAnimation != null)
                _starAnimation.Kill();

            _starAnimation = DOTween.Sequence()
                .Append(img.DOColor(AnimationColor1, AnimationDuraction))
                .Append(img.DOColor(AnimationColor2, AnimationDuraction))
                .SetLoops(-1)
                .SetUpdate(true);
        }

        private void OnDisable()
        {
            if (_starAnimation != null)
                _starAnimation.Kill();
        }
    }
}
