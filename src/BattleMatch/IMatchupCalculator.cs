using Optimizer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimizer
{
    interface IMatchupCalculator
    {
        IDictionary<Pokemon, double> FindFavorableAttackMatchups(string defendingPokemonName, int modifierLimit = 1);

        IDictionary<Pokemon, double> FindFavorableAttackMatchups(Pokemon defendingPokemon, int modifierLimit = 1);
    }
}
