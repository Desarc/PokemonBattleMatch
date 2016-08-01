using System.Collections.Generic;
using System.Linq;

namespace Optimizer.Model
{
    internal class PokemonTemplate
    {
        public PokemonTemplate(int number, string name, int stage, int maxStage, double maxCP, double maxHP, IEnumerable<string> fastAttacks, IEnumerable<string> specialAttacks, string firstType, string secondType = null)
        {
            Number = number;
            Name = name;
            Stage = stage;
            MaxStage = maxStage;
            MaxCP = maxCP;
            MaxHP = maxHP;
            FastAttacks = new List<string>(fastAttacks.Select(fa => fa.ToLower()));
            SpecialAttacks = new List<string>(specialAttacks.Select(fa => fa.ToLower()));
            FirstType = firstType;
            SecondType = secondType;
        }

        public int Number { get; }

        public string NumberString => string.Format("{0,3:D3}", Number);

        public string Name { get; }

        public int Stage { get; }

        public int MaxStage { get; }

        public bool IsMaxStage => Stage == MaxStage;

        public double MaxCP { get; }

        public double MaxHP { get; }

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
