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
        IDictionary<Pokemon, double> FindFavorableAttackMatchups(Pokemon attackingPokemon);
    }
}
