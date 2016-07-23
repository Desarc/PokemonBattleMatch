using System;
using System.Collections.Generic;

namespace Optimizer.Model
{
    internal class TypeMatchup
    {
        private readonly IList<TypeModifier> _typeModifiers;

        public TypeMatchup(string attackingType, IList<TypeModifier> typeModifiers)
        {
            AttackingType = attackingType;
            _typeModifiers = typeModifiers;
        }

        public string AttackingType { get; }

        public double GetModifier(string defendingType)
        {
            foreach (var typeModifier in _typeModifiers)
            {
                if (typeModifier.DefendingType.ToLower() == defendingType.ToLower())
                {
                    return typeModifier.Modifier;
                }
            }

            throw new InvalidOperationException($"No modifier found for attackingType {AttackingType} and defendingType {defendingType}");
        }
    }
}
