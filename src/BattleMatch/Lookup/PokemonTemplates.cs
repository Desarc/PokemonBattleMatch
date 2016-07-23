using Newtonsoft.Json;
using Optimizer.Data;
using Optimizer.Model;
using System.Collections.Generic;

namespace Optimizer.Lookup
{
    internal class PokemonTemplates : IPokemonTemplates
    {
        private readonly IDictionary<string, PokemonTemplate> _pokemonTemplates;

        public PokemonTemplates()
        {
            _pokemonTemplates = new Dictionary<string, PokemonTemplate>();
            var pokemonTemplates = JsonConvert.DeserializeObject<IList<PokemonTemplate>>(Files.PokemonTemplates);
            foreach (var pokemonTemplate in pokemonTemplates)
            {
                _pokemonTemplates.Add(pokemonTemplate.Name.ToLower(), pokemonTemplate);
            }
        }

        public PokemonTemplate GetTemplate(string pokemonName)
        {
            return _pokemonTemplates[pokemonName.ToLower()];
        }

        public IList<Pokemon> GetAllPermutations(IPokemonFactory pokemonFactory)
        {
            var permutations = new List<Pokemon>();
            foreach (var pokemonTemplate in _pokemonTemplates.Values)
            {
                permutations.AddRange(pokemonTemplate.CreatePermutations(pokemonFactory));
            }

            return permutations;
        }
    }
}
