using Optimizer.Lookup;
using Optimizer.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Optimizer.Analysis
{
    internal class MatchupCalculator : IMatchupCalculator
    {
        // game constants
        private const double SameTypeAttackBonus = 1.25;

        // custom matchup constants, may be completely wrong
        private const double CPPowerIncreaseModifier = 2;
        private const double FastAttackWeighting = 1;

        private readonly string[] NotAvailable = { "mew", "mewtwo", "moltres", "articuno", "zapdos", "ditto" };

        private readonly ITypeMatchups _typeMatchups;
        private readonly IPokemonTemplates _pokemonTemplates;
        private readonly IPokemonFactory _pokemonFactory;

        public MatchupCalculator(ITypeMatchups typeMatchups, IPokemonTemplates pokemonTemplates, IPokemonFactory pokemonFactory)
        {
            _typeMatchups = typeMatchups;
            _pokemonTemplates = pokemonTemplates;
            _pokemonFactory = pokemonFactory;
        }
        
        public IDictionary<Pokemon, double> FindFavorableAttackMatchups(string defendingPokemonName, bool onlyMaxStage = false, bool adjustForCP = false, int modifierLimit = 1)
        {
            var favorableMatchups = new Dictionary<Pokemon, double>();

            var defendingPokemonTemplate = _pokemonTemplates.GetTemplate(defendingPokemonName);
            var defendingPokemonPermutations = defendingPokemonTemplate.CreatePermutations(_pokemonFactory);

            var attackingPokemonPermutations = _pokemonTemplates.GetAllPermutations(_pokemonFactory, onlyMaxStage);
            foreach (var attackingPokemonPermutation in attackingPokemonPermutations)
            {
                if (NotAvailable.Contains(attackingPokemonPermutation.Name.ToLower()))
                {
                    continue;
                }

                var modifiers = new List<double>();

                foreach (var defendingPokemonPermutation in defendingPokemonPermutations)
                {
                    var modifier = MatchTotal(attackingPokemonPermutation, defendingPokemonPermutation, adjustForCP);

                    modifiers.Add(modifier);
                }

                var totalResult = modifiers.Sum() / modifiers.Count;
                if (totalResult > modifierLimit)
                {
                    favorableMatchups.Add(attackingPokemonPermutation, totalResult);
                }
            }
            

            return SortByValue(favorableMatchups);
        }

        public IDictionary<Pokemon, double> FindFavorableAttackMatchups(Pokemon defendingPokemon, bool onlyMaxStage = false, bool adjustForCP = false, int modifierLimit = 1)
        {
            var favorableMatchups = new Dictionary<Pokemon, double>();
            var attackingPokemonPermutations = _pokemonTemplates.GetAllPermutations(_pokemonFactory, onlyMaxStage);
            foreach (var attackingPokemonPermutation in attackingPokemonPermutations)
            {
                if (NotAvailable.Contains(attackingPokemonPermutation.Name.ToLower()))
                {
                    continue;
                }

                var totalResult = MatchTotal(attackingPokemonPermutation, defendingPokemon, adjustForCP);
                
                if (totalResult > modifierLimit)
                {
                    favorableMatchups.Add(attackingPokemonPermutation, totalResult);
                }
            }

            return SortByValue(favorableMatchups);
        }

        private double MatchTotal(Pokemon attackingPokemon, Pokemon defendingPokemon, bool adjustForCP = false)
        {
            // get attack vs defense type modifiers
            var attackingPokemonFastAttackModifierResult = GetFastAttackModifier(attackingPokemon, defendingPokemon);
            var attackingPokemonSpecialAttackModifierResult = GetSpecialAttackModifier(attackingPokemon, defendingPokemon);
            var defendingPokemonFastAttackModifierResult = GetFastAttackModifier(defendingPokemon, attackingPokemon);
            var defendingPokemonSpecialAttackModifierResult = GetSpecialAttackModifier(defendingPokemon, attackingPokemon);

            // compare fast and special attack DPS
            var fastAttackDPSModifier = attackingPokemon.FastAttack.DPS / defendingPokemon.FastAttack.DPS;
            var specialAttackDPSModifier = attackingPokemon.SpecialAttack.DPS / defendingPokemon.SpecialAttack.DPS;

            var fastAttackModifier = (attackingPokemonFastAttackModifierResult / defendingPokemonFastAttackModifierResult) * fastAttackDPSModifier;
            var specialAttackModifier = (attackingPokemonSpecialAttackModifierResult / defendingPokemonSpecialAttackModifierResult) * specialAttackDPSModifier;

            var totalModifier = (fastAttackDPSModifier * FastAttackWeighting + specialAttackModifier) / (FastAttackWeighting + 1);

            if (adjustForCP)
            {
                var CPmodifier = attackingPokemon.MaxCP / defendingPokemon.MaxCP;
                return totalModifier * Math.Pow(CPmodifier, CPPowerIncreaseModifier);
            }

            return totalModifier;
        }

        private double GetFastAttackModifier(Pokemon attackingPokemon, Pokemon defendingPokemon)
        {
            double fastAttackModifier;
            if (defendingPokemon.SecondType != null)
            {
                fastAttackModifier = _typeMatchups.GetModifier(attackingPokemon.FastAttack.Type, defendingPokemon.FirstType, defendingPokemon.SecondType);
            }
            else
            {
                fastAttackModifier = _typeMatchups.GetModifier(attackingPokemon.FastAttack.Type, defendingPokemon.FirstType);
            }

            if (SameTypeAttackBonusAppliesToFast(attackingPokemon))
            {
                fastAttackModifier = fastAttackModifier * SameTypeAttackBonus;
            }

            return fastAttackModifier;
        }

        private double GetSpecialAttackModifier(Pokemon attackingPokemon, Pokemon defendingPokemon)
        {
            double specialAttackModifier;
            if (defendingPokemon.SecondType != null)
            {
                specialAttackModifier = _typeMatchups.GetModifier(attackingPokemon.SpecialAttack.Type, defendingPokemon.FirstType, defendingPokemon.SecondType);
            }
            else
            {
                specialAttackModifier = _typeMatchups.GetModifier(attackingPokemon.SpecialAttack.Type, defendingPokemon.FirstType);
            }

            if (SameTypeAttackBonusAppliesToFast(attackingPokemon))
            {
                specialAttackModifier = specialAttackModifier * SameTypeAttackBonus;
            }

            return specialAttackModifier;
        }

        private static IDictionary<Pokemon, double> SortByValue(IDictionary<Pokemon, double> dictionary)
        {
            var sorted = from entry in dictionary
                         orderby entry.Value descending
                         select entry;

            return sorted.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        private static bool SameTypeAttackBonusAppliesToFast(Pokemon pokemon)
        {
            return pokemon.FastAttack.Type == pokemon.FirstType || pokemon.FastAttack.Type == pokemon.SecondType;
        }

        public static bool SameTypeAttackBonusAppliesToSpecial(Pokemon pokemon)
        {
            return pokemon.SpecialAttack.Type == pokemon.FirstType || pokemon.SpecialAttack.Type == pokemon.SecondType;
        }
    }
}
