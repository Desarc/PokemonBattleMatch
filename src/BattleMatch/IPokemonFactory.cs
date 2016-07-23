using Optimizer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimizer
{
    internal interface IPokemonFactory
    {
        Pokemon CreatePokemon(string name, string fastAttackName, string specialAttackName);
    }
}
