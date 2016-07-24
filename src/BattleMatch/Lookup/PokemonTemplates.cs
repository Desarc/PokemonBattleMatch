using Newtonsoft.Json;
using Optimizer.Data;
using Optimizer.Model;
using System.Collections.Generic;
using System.Linq;

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

        public int GetMaxHP()
        {
            var hps = from template in _pokemonTemplates.Values
                     orderby template.MaxHP descending
                     select template.MaxHP;

            return hps.First();
        }

        public int GetMaxCP()
        {
            var cps = from template in _pokemonTemplates.Values
                     orderby template.MaxCP descending
                     select template.MaxCP;

            return cps.First();
        }

        public IEnumerable<string> GetAllNames()
        {
            return from template in _pokemonTemplates.Values
                   select template.Name;
        }

        public IEnumerable<PokemonTemplate> GetAllTemplates(bool onlyMaxStage = false)
        {
            return onlyMaxStage
                ? from pokemonTemplate in _pokemonTemplates.Values
                  where pokemonTemplate.IsMaxStage
                  select pokemonTemplate
                : _pokemonTemplates.Values;
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
