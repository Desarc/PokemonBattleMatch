using Optimizer.Model;
using System.Collections.Generic;

namespace Optimizer.Analysis
{
    interface IMatchupCalculator
    {
        IDictionary<Pokemon, double> FindFavorableAttackMatchups(string defendingPokemonName, bool onlyMaxStage = false, bool adjustForCP = false, int modifierLimit = 1);

        IDictionary<Pokemon, double> FindFavorableAttackMatchups(Pokemon defendingPokemon, bool onlyMaxStage = false, bool adjustForCP = false, int modifierLimit = 1);
    }
}
