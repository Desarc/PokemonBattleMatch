using Optimizer.Model;
using System.Collections.Generic;

namespace Optimizer.Lookup
{
    internal interface IPokemonTemplates
    {
        PokemonTemplate GetTemplate(string pokemonName);

        IList<Pokemon> GetAllPermutations(IPokemonFactory pokemonFactory);
    }
}
