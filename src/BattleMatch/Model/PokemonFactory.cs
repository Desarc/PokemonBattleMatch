using Optimizer.Lookup;
using System;

namespace Optimizer.Model
{
    internal class PokemonFactory : IPokemonFactory
    {
        private readonly IMoves _moves;
        private readonly IPokemonTemplates _pokemonTemplates;

        public PokemonFactory(IMoves moves, IPokemonTemplates pokemonTemplates)
        {
            _moves = moves;
            _pokemonTemplates = pokemonTemplates;
        }

        public Pokemon CreatePokemon(string name, string fastMoveName, string specialMoveName)
        {
            var template = _pokemonTemplates.GetTemplate(name);
            if (!template.FastMoveValid(fastMoveName))
            {
                throw new InvalidOperationException($"Fast move {fastMoveName} is not valid for {name}");
            }

            if (!template.SpecialMoveValid(specialMoveName))
            {
                throw new InvalidOperationException($"Special move {specialMoveName} is not valid for {name}");
            }

            var fastMove = _moves.GetFastMove(fastMoveName);
            var specialMove = _moves.GetSpecialMove(specialMoveName);

            return new Pokemon(template.Number, name, fastMove, specialMove, template.MaxCP, template.FirstType, template.SecondType);
        }
    }
}
