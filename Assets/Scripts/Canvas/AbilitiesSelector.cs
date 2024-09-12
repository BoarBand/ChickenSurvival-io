using UnityEngine;
using System.Collections.Generic;
using SurvivalChicken.ScriptableObjects.AbilitiesParameters;
using SurvivalChicken.Abilities.Card;
using SurvivalChicken.Abilities;

namespace SurvivalChicken.Controllers
{
    public class AbilitiesSelector : MonoBehaviour
    {
        [SerializeField] private AbilityParameters[] _abilities;
        [SerializeField] private AbilityCard[] _abilityCardsObjs;
        [SerializeField] private SelectedAbilityCard[] _selectedAbilityCards;

        private List<Ability> _createdAbilities = new List<Ability>();

        public void Initialize()
        {
            List<AbilityParameters> allAbilities = new List<AbilityParameters>();

            CreateInitAbilityForPlayer();

            foreach (AbilityParameters ability in _abilities)
                if (ability.Level < ability.MaxLevel)
                    allAbilities.Add(ability);

            for (int i = 0; i < _abilityCardsObjs.Length; i++)
            {
                if(allAbilities.Count <= 0)
                {
                    _abilityCardsObjs[i].Initialize(null);
                    continue;
                }

                AbilityParameters ability = allAbilities[Random.Range(0, allAbilities.Count)];
                _abilityCardsObjs[i].Initialize(ability);
                allAbilities.Remove(ability);
            }

            gameObject.SetActive(true);

            Time.timeScale = 0;
        }

        public void AddOrUpgradeAbility(Ability ability)
        {
            bool TryGetAbility(out Ability abilityType)
            {
                foreach (Ability i in _createdAbilities)
                {
                    if (ability.GetType() == i.GetType())
                    {
                        abilityType = i;
                        return true;
                    }
                }
                abilityType = null;
                return false;
            }

            if(TryGetAbility(out Ability currentAbility))
                currentAbility.Upgrade();
            else
            {
                Ability newAbility = Instantiate(ability);
                _createdAbilities.Add(newAbility);
            }

            UpdateSelectedAbilities();
        }

        private void UpdateSelectedAbilities()
        {
            if (_createdAbilities.Count <= 0)
                return;

            foreach (SelectedAbilityCard selectedAbilityCard in _selectedAbilityCards)
                selectedAbilityCard.gameObject.SetActive(false);

            for(int i = 0; i < _createdAbilities.Count; i++)
                _selectedAbilityCards[i].Initialize(_createdAbilities[i].AbilityParameters);
        }

        private void CreateInitAbilityForPlayer()
        {
            if (Spawner.PlayerSpawner.Instance.CurrentPlayer.PlayerParameters.InitialAbility == null)
                return;

            AddOrUpgradeAbility(Spawner.PlayerSpawner.Instance.CurrentPlayer.PlayerParameters.InitialAbility.Ability);
        }

        public Ability[] GetCreatedAbilities()
        {
            return _createdAbilities.ToArray();
        }

        private void OnDisable()
        {
            Time.timeScale = 1;
        }

        public void ResetAllAbilities()
        {
            foreach (AbilityParameters ability in _abilities)
                ability.Level = ability.MinLevel;
        }
    }
}
