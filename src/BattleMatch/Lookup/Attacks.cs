using Newtonsoft.Json;
using Optimizer.Data;
using Optimizer.Model;
using System.Collections.Generic;

namespace Optimizer.Lookup
{
    internal class Attacks : IAttacks
    {
        private readonly IDictionary<string, Attack> _fastAttacks;
        private readonly IDictionary<string, Attack> _specialAttacks;

        public Attacks()
        {
            _fastAttacks = new Dictionary<string, Attack>();
            var fastAttacks = JsonConvert.DeserializeObject<IList<Attack>>(Files.FastAttacks);
            foreach (var fastAttack in fastAttacks)
            {
                _fastAttacks.Add(fastAttack.Name.ToLower(), fastAttack);
            }

            _specialAttacks = new Dictionary<string, Attack>();
            var specialAttacks = JsonConvert.DeserializeObject<IList<Attack>>(Files.SpecialAttacks);
            foreach (var specialAttack in specialAttacks)
            {
                _specialAttacks.Add(specialAttack.Name.ToLower(), specialAttack);
            }
        }

        public Attack GetFastAttack(string attackName)
        {
            return _fastAttacks[attackName.ToLower()];
        }

        public Attack GetSpecialAttack(string attackName)
        {
            return _specialAttacks[attackName.ToLower()];
        }
    }
}
