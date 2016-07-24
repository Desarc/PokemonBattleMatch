﻿using Optimizer.Model;
using System.Collections.Generic;

namespace Optimizer.Analysis
{
    interface IMatchupCalculator
    {
        IDictionary<Pokemon, double> FindFavorableAttackMatchups(string defendingPokemonName, int modifierLimit = 1);

        IDictionary<Pokemon, double> FindFavorableAttackMatchups(Pokemon defendingPokemon, int modifierLimit = 1);
    }
}