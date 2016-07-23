using Optimizer.Lookup;
using Optimizer.Model;
using System;

namespace Optimizer
{
    internal class PokemonFactory : IPokemonFactory
    {
        private readonly IAttacks _attacks;
        private readonly IPokemonTemplates _pokemonTemplates;

        public PokemonFactory(IAttacks attacks, IPokemonTemplates pokemonTemplates)
        {
            _attacks = attacks;
            _pokemonTemplates = pokemonTemplates;
        }

        public Pokemon CreatePokemon(string name, string fastAttackName, string specialAttackName)
        {
            var template = _pokemonTemplates.GetTemplate(name);
            if (!template.FastAttackValid(fastAttackName))
            {
                throw new InvalidOperationException($"Fast attack {fastAttackName} is not valid for {name}");
            }

            if (!template.SpecialAttackValid(specialAttackName))
            {
                throw new InvalidOperationException($"Special attack {specialAttackName} is not valid for {name}");
            }

            var fastAttack = _attacks.GetFastAttack(fastAttackName);
            var specialAttack = _attacks.GetSpecialAttack(specialAttackName);

            return new Pokemon(name, fastAttack, specialAttack, template.FirstType, template.SecondType);
        }
    }
}
