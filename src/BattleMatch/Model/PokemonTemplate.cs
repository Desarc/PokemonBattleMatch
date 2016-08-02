using System.Collections.Generic;
using System.Linq;

namespace Optimizer.Model
{
    internal class PokemonTemplate
    {
        public PokemonTemplate(int number, string name, int stage, int maxStage, double maxCP, double maxHP, IEnumerable<string> fastMoves, IEnumerable<string> specialMoves, string firstType, string secondType = null)
        {
            Number = number;
            Name = name;
            Stage = stage;
            MaxStage = maxStage;
            MaxCP = maxCP;
            MaxHP = maxHP;
            FastMoves = new List<string>(fastMoves.Select(fa => fa.ToLower()));
            SpecialMoves = new List<string>(specialMoves.Select(fa => fa.ToLower()));
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

        public IList<string> FastMoves { get; }

        public IList<string> SpecialMoves { get; }

        public string FirstType { get; }

        public string SecondType { get; }

        public bool FastMoveValid(string attackName)
        {
            return FastMoves.Contains(attackName.ToLower());
        }

        public bool SpecialMoveValid(string attackName)
        {
            return SpecialMoves.Contains(attackName.ToLower());
        }

        public IList<Pokemon> CreatePermutations(IPokemonFactory pokemonFactory)
        {
            var permutations = new List<Pokemon>();
            foreach (var fastMove in FastMoves)
            {
                foreach (var specialMove in SpecialMoves)
                {
                    permutations.Add(pokemonFactory.CreatePokemon(Name, fastMove, specialMove));
                }
            }

            return permutations;
        }
    }
}
