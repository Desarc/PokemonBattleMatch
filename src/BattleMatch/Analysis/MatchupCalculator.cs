using Optimizer.Data;
using Optimizer.Lookup;
using Optimizer.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Optimizer.Analysis
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
        
        public IDictionary<Pokemon, double> FindFavorableAttackMatchups(string defendingPokemonName, bool onlyMaxStage = false, bool adjustForCP = false, int modifierLimit = 1)
        {
            var favorableMatchups = new Dictionary<Pokemon, double>();

            var defendingPokemonTemplate = _pokemonTemplates.GetTemplate(defendingPokemonName);
            var defendingPokemonPermutations = defendingPokemonTemplate.CreatePermutations(_pokemonFactory);

            var attackingPokemonPermutations = _pokemonTemplates.GetAllPermutations(_pokemonFactory, onlyMaxStage);
            foreach (var attackingPokemonPermutation in attackingPokemonPermutations)
            {
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
            var attackingPokemonFastMoveTypeModifier = CalculateFastMoveTypeModifier(attackingPokemon, defendingPokemon);
            var attackingPokemonSpecialMoveTypeModifier = CalculateSpecialMoveTypeModifier(attackingPokemon, defendingPokemon);
            var defendingPokemonFastMoveTypeModifier = CalculateFastMoveTypeModifier(defendingPokemon, attackingPokemon);
            var defendingPokemonSpecialMoveTypeModifier = CalculateSpecialMoveTypeModifier(defendingPokemon, attackingPokemon);

            // compare fast and special move DPS
            var fastMoveDPSModifier = CalculateMoveDPSModifier(attackingPokemon.FastMove, defendingPokemon.FastMove);
            var specialMoveDPSModifier = CalculateMoveDPSModifier(attackingPokemon.SpecialMove, defendingPokemon.SpecialMove);

            var fastMoveModifier = (attackingPokemonFastMoveTypeModifier / defendingPokemonFastMoveTypeModifier) * fastMoveDPSModifier;
            var specialMoveModifier = (attackingPokemonSpecialMoveTypeModifier / defendingPokemonSpecialMoveTypeModifier) * specialMoveDPSModifier;

            var totalModifier = (fastMoveModifier * Constants.FastMoveWeighting + specialMoveModifier) / (Constants.FastMoveWeighting + 1);

            if (adjustForCP)
            {
                var CPmodifier = attackingPokemon.MaxCP / defendingPokemon.MaxCP;
                return totalModifier * Math.Pow(CPmodifier, Constants.CPPowerIncreaseModifier);
            }

            return totalModifier;
        }

        private double CalculateFastMoveTypeModifier(Pokemon attackingPokemon, Pokemon defendingPokemon)
        {
            double fastMoveModifier;
            if (defendingPokemon.SecondType != null)
            {
                fastMoveModifier = _typeMatchups.GetModifier(attackingPokemon.FastMove.Type, defendingPokemon.FirstType, defendingPokemon.SecondType);
            }
            else
            {
                fastMoveModifier = _typeMatchups.GetModifier(attackingPokemon.FastMove.Type, defendingPokemon.FirstType);
            }

            if (attackingPokemon.SameTypeAttackBonusAppliesToFastMove)
            {
                fastMoveModifier = fastMoveModifier * Constants.SameTypeAttackBonus;
            }

            return fastMoveModifier;
        }

        private double CalculateSpecialMoveTypeModifier(Pokemon attackingPokemon, Pokemon defendingPokemon)
        {
            double specialMoveModifier;
            if (defendingPokemon.SecondType != null)
            {
                specialMoveModifier = _typeMatchups.GetModifier(attackingPokemon.SpecialMove.Type, defendingPokemon.FirstType, defendingPokemon.SecondType);
            }
            else
            {
                specialMoveModifier = _typeMatchups.GetModifier(attackingPokemon.SpecialMove.Type, defendingPokemon.FirstType);
            }

            if (attackingPokemon.SameTypeAttackBonusAppliesToSpecialMove)
            {
                specialMoveModifier = specialMoveModifier * Constants.SameTypeAttackBonus;
            }

            return specialMoveModifier;
        }

        private double CalculateMoveDPSModifier(Move attackingPokemonMove, Move defendingPokemonMove)
        {
            var attackingPokemonDPS = attackingPokemonMove.DPS + attackingPokemonMove.DPS * Constants.CriticalHitMultiplier * attackingPokemonMove.CriticalHitChance;
            var defendingPokemonDPS = defendingPokemonMove.DPS + defendingPokemonMove.DPS * Constants.CriticalHitMultiplier * defendingPokemonMove.CriticalHitChance;

            return attackingPokemonDPS / defendingPokemonDPS;
        }

        private static IDictionary<Pokemon, double> SortByValue(IDictionary<Pokemon, double> dictionary)
        {
            var sorted = from entry in dictionary
                         orderby entry.Value descending
                         select entry;

            return sorted.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
    }
}
