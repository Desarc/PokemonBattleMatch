using Optimizer.Lookup;
using Optimizer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimizer
{
    internal class MatchupCalculator : IMatchupCalculator
    {
        private readonly ITypeMatchups _typeMatchups;
        private readonly IPokemonTemplates _pokemonTemplates;
        private readonly IPokemonFactory _pokemonFactory;

        public MatchupCalculator(ITypeMatchups typeMatchups, IPokemonTemplates pokemonTemplates, IPokemonFactory pokemonFactory)
        {
            _typeMatchups = typeMatchups;
            _pokemonTemplates = pokemonTemplates;
            _pokemonFactory = pokemonFactory;
        }

        public IDictionary<Pokemon, double> FindFavorableAttackMatchups(Pokemon defendingPokemon)
        {
            var favorableMatchups = new Dictionary<Pokemon, double>();
            var permutations = _pokemonTemplates.GetAllPermutations(_pokemonFactory);
            foreach (var permutation in permutations)
            {
                var attackingModifierResult1 = MatchFastAttack(permutation, defendingPokemon);
                var attackingModifierResult2 = MatchSpecialAttack(permutation, defendingPokemon);
                var defendingModifierResult1 = MatchFastAttack(defendingPokemon, permutation);
                var defendingModifierResult2 = MatchSpecialAttack(defendingPokemon, permutation);
                var totalResult = attackingModifierResult1 * attackingModifierResult1 * defendingModifierResult1 * defendingModifierResult2;
                if (totalResult > 1)
                {
                    favorableMatchups.Add(permutation, totalResult);
                }
            }

            return SortByValue(favorableMatchups);
        }

        private double MatchFastAttack(Pokemon attackingPokemon, Pokemon defendingPokemon)
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

            return specialAttackModifier;
        }

        private IDictionary<Pokemon, double> SortByValue(IDictionary<Pokemon, double> dictionary)
        {
            var sorted = from entry in dictionary
                         orderby entry.Value descending
                         select entry;

            return sorted.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
    }
}
