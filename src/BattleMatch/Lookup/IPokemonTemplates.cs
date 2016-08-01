using Optimizer.Model;
using System.Collections.Generic;

namespace Optimizer.Lookup
{
    internal interface IPokemonTemplates
    {
        PokemonTemplate GetTemplate(string pokemonName);

        double GetMaxHP();

        double GetMaxCP();

        IEnumerable<string> GetAllNames();

        IEnumerable<PokemonTemplate> GetAllTemplates(bool onlyMaxStage = false);

        IList<Pokemon> GetAllPermutations(IPokemonFactory pokemonFactory, bool onlyMaxStage = false);
    }
}
