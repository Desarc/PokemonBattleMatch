using Optimizer.Lookup;
using Optimizer.Model;
using System.Collections.Generic;
using System.Linq;

namespace Optimizer.Analysis
{
    internal class MatchupCalculator : IMatchupCalculator
    {
        private const double SameTypeAttackBonus = 1.25;

        private readonly ITypeMatchups _typeMatchups;
        private readonly IPokemonTemplates _pokemonTemplates;
        private readonly IPokemonFactory _pokemonFactory;

        public MatchupCalculator(ITypeMatchups typeMatchups, IPokemonTemplates pokemonTemplates, IPokemonFactory pokemonFactory)
        {
            _typeMatchups = typeMatchups;
            _pokemonTemplates = pokemonTemplates;
            _pokemonFactory = pokemonFactory;
        }

        // TODO: take max CP into account
        public IDictionary<Pokemon, double> FindFavorableAttackMatchups(string defendingPokemonName, int modifierLimit = 1)
        {
            var favorableMatchups = new Dictionary<Pokemon, double>();

            var defendingPokemonTemplate = _pokemonTemplates.GetTemplate(defendingPokemonName);
            var defendingPokemonPermutations = defendingPokemonTemplate.CreatePermutations(_pokemonFactory);

            var attackingPokemonPermutations = _pokemonTemplates.GetAllPermutations(_pokemonFactory);
            foreach (var attackingPokemonPermutation in attackingPokemonPermutations)
            {
                var modifiers = new List<double>();

                foreach (var defendingPokemonPermutation in defendingPokemonPermutations)
                {
                    var modifier = MatchTotal(attackingPokemonPermutation, defendingPokemonPermutation);

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

        public IDictionary<Pokemon, double> FindFavorableAttackMatchups(Pokemon defendingPokemon, int modifierLimit = 1)
        {
            var favorableMatchups = new Dictionary<Pokemon, double>();
            var attackingPokemonPermutations = _pokemonTemplates.GetAllPermutations(_pokemonFactory);
            foreach (var attackingPokemonPermutation in attackingPokemonPermutations)
            {
                var totalResult = MatchTotal(attackingPokemonPermutation, defendingPokemon);
                
                if (totalResult > modifierLimit)
                {
                    favorableMatchups.Add(attackingPokemonPermutation, totalResult);
                }
            }

            return SortByValue(favorableMatchups);
        }

        private double MatchTotal(Pokemon attackingPokemon, Pokemon defendingPokemon)
        {
            var attackingModifierResult1 = MatchFastAttack(attackingPokemon, defendingPokemon);
            var attackingModifierResult2 = MatchSpecialAttack(attackingPokemon, defendingPokemon);
            var defendingModifierResult1 = MatchFastAttack(defendingPokemon, attackingPokemon);
            var defendingModifierResult2 = MatchSpecialAttack(defendingPokemon, attackingPokemon);

            return (attackingModifierResult1 + attackingModifierResult1) / (defendingModifierResult1 + defendingModifierResult2);
        }

        private double MatchFastAttack(Pokemon attackingPokemon, Pokemon defendingPokemon)
        {
            double fastAttackModifier;
            var fastAttackType = attackingPokemon.FastAttack.Type;
            if (defendingPokemon.SecondType != null)
            {
                fastAttackModifier = _typeMatchups.GetModifier(fastAttackType, defendingPokemon.FirstType, defendingPokemon.SecondType);
            }
            else
            {
                fastAttackModifier = _typeMatchups.GetModifier(fastAttackType, defendingPokemon.FirstType);
            }

            if (SameTypeAttackBonusAppliesToFast(attackingPokemon))
            {
                fastAttackModifier = fastAttackModifier * SameTypeAttackBonus;
            }

            return fastAttackModifier;
        }

        private double MatchSpecialAttack(Pokemon attackingPokemon, Pokemon defendingPokemon)
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
