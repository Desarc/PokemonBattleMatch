using System.Collections.Generic;
using System.Linq;

namespace Optimizer.Model
{
    internal class PokemonTemplate
    {
        public PokemonTemplate(int number, string name, IEnumerable<string> fastAttacks, IEnumerable<string> specialAttacks, string firstType, string secondType = null)
        {
            Number = number;
            Name = name;
            FastAttacks = new List<string>(fastAttacks.Select(fa => fa.ToLower()));
            SpecialAttacks = new List<string>(specialAttacks.Select(fa => fa.ToLower()));
            FirstType = firstType;
            SecondType = secondType;
        }

        public int Number { get; }

        public string Name { get; }

        public IList<string> FastAttacks { get; }

        public IList<string> SpecialAttacks { get; }

        public string FirstType { get; }

        public string SecondType { get; }

        public bool FastAttackValid(string attackName)
        {
            return FastAttacks.Contains(attackName.ToLower());
        }

        public bool SpecialAttackValid(string attackName)
        {
            return SpecialAttacks.Contains(attackName.ToLower());
        }

        public IList<Pokemon> CreatePermutations(IPokemonFactory pokemonFactory)
        {
            var permutations = new List<Pokemon>();
            foreach (var fastAttack in FastAttacks)
            {
                foreach (var specialAttack in SpecialAttacks)
                {
                    permutations.Add(pokemonFactory.CreatePokemon(Name, fastAttack, specialAttack));
                }
            }

            return permutations;
        }
    }
}
