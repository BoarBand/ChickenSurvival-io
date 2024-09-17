using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using SurvivalChicken.Abilities;
using SurvivalChicken.FortuneWheel.Cell;
using TMPro;
using SurvivalChicken.Controllers;
using SurvivalChicken.Abilities.Card;
using SurvivalChicken.ScriptableObjects.AbilitiesParameters;

namespace SurvivalChicken.FortuneWheel
{
    public class FortuneWheelView : MonoBehaviour
    {
        [SerializeField] private StatisticsView _statisticsView;
        [SerializeField] private FortuneWheelCell[] _fortuneWheelCells;
        [SerializeField] private TextMeshProUGUI _coinsTxt;
        [SerializeField] private AbilitiesSelector _abilitiesSelector;
        [SerializeField] private Image _winAbilityImage;
        [SerializeField] private AbilityView[] _abilityViews;

        private Coroutine _fortuneWheelPlayingCoroutine;

        private readonly float MinFortuneWheelPlayTime = 0.2f;
        private readonly float MaxFortuneWheelPlayTime = 0.0085f;

        private readonly int MinRewardCoins = 10;
        private readonly int MaxRewardCoins = 40;

        public int RewardCoins { get; private set; }

        public void Initialize()
        {
            Ability[] createdAbilities = _abilitiesSelector.GetCreatedAbilities();
            List<Ability> availableAbilities = new List<Ability>();

            foreach (Ability ability in createdAbilities)
                if (ability.AbilityParameters.Level < ability.AbilityParameters.MaxLevel)
                    availableAbilities.Add(ability);

            if(availableAbilities.Count <= 0)
            {
                gameObject.SetActive(false);
                return;
            }

            foreach (FortuneWheelCell cell in _fortuneWheelCells)
                cell.Initialize(availableAbilities[Random.Range(0, availableAbilities.Count)].AbilityParameters);

            RewardCoins = 0;
            _coinsTxt.text = RewardCoins.ToString();

            gameObject.SetActive(true);

            if (_fortuneWheelPlayingCoroutine != null)
                StopCoroutine(_fortuneWheelPlayingCoroutine);
            _fortuneWheelPlayingCoroutine = StartCoroutine(FortuneWheelPlaying());

            Time.timeScale = 0f;
        }

        private IEnumerator FortuneWheelPlaying()
        {
            float time = MaxFortuneWheelPlayTime;

            int cellIndex = 0;

            RewardCoins = 0;

            while (time < MinFortuneWheelPlayTime)
            {
                _fortuneWheelCells[cellIndex].EnableSelect();
                IncreaseCoins();

                yield return new WaitForSecondsRealtime(time);

                time += Random.Range(0.0005f, 0.0075f);
                if (time >= MinFortuneWheelPlayTime)
                {
                    DisplayWinAbility(_fortuneWheelCells[cellIndex].SelectedAbility);
                    break;
                }

                _fortuneWheelCells[cellIndex].DisableSelect();

                cellIndex++;
                if(cellIndex >= _fortuneWheelCells.Length)
                    cellIndex = 0;

            }

            _fortuneWheelPlayingCoroutine = null;
        }

        private void DisplayWinAbility(AbilityParameters abilityParameters)
        {
            foreach(AbilityView view in _abilityViews)
            {
                view.Disable();
            }

            _abilitiesSelector.AddOrUpgradeAbility(abilityParameters.Ability);
            _winAbilityImage.gameObject.SetActive(true);
            _abilityViews[0].Initialize(abilityParameters);
        }

        private void IncreaseCoins()
        {
            int rand = Random.Range(MinRewardCoins, MaxRewardCoins);
            RewardCoins += rand;
            _coinsTxt.text = RewardCoins.ToString();
            _statisticsView.IncreaseCoinsAmount(rand);
        }

        public void CloseView()
        {
            if (_fortuneWheelPlayingCoroutine != null)
            {
                StopCoroutine(_fortuneWheelPlayingCoroutine);

                FortuneWheelCell fortuneWheelCell = _fortuneWheelCells[Random.Range(0, _fortuneWheelCells.Length)];
                foreach (FortuneWheelCell cell in _fortuneWheelCells)
                    cell.DisableSelect();
                fortuneWheelCell.EnableSelect();
                DisplayWinAbility(fortuneWheelCell.SelectedAbility);
                _fortuneWheelPlayingCoroutine = null;
                return;
            }
            gameObject.SetActive(false);

            Time.timeScale = 1f;
        }

        public void CloseWinAbilityImage()
        {
            _winAbilityImage.gameObject.SetActive(false);
        }
    }
}
