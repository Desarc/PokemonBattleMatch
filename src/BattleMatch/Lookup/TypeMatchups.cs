using Newtonsoft.Json;
using Optimizer.Data;
using Optimizer.Model;
using System.Collections.Generic;

namespace Optimizer.Lookup
{
    internal class TypeMatchups : ITypeMatchups
    {
        private readonly IDictionary<string, TypeMatchup> _typeMatchups;

        public TypeMatchups()
        {
            _typeMatchups = new Dictionary<string, TypeMatchup>();
            var typeMatchups = JsonConvert.DeserializeObject<IList<TypeMatchup>>(Files.TypeMathcups);
            foreach (var typeMatchup in typeMatchups)
            {
                _typeMatchups.Add(typeMatchup.AttackingType.ToLower(), typeMatchup);
            }
        }

        public double GetModifier(string attackingType, string defendingType)
        {
            var typeMatchup = _typeMatchups[attackingType.ToLower()];
            return typeMatchup.GetModifier(defendingType);
        }

        public double GetModifier(string attackingType, string firstDefendingType, string secondDefendingType)
        {
            var typeMatchup = _typeMatchups[attackingType.ToLower()];
            var firstModifier = typeMatchup.GetModifier(firstDefendingType);
            var secondModifier = typeMatchup.GetModifier(secondDefendingType);

            return firstModifier * secondModifier;
        }
    }
}
