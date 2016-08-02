using Newtonsoft.Json;
using Optimizer.Data;
using Optimizer.Model;
using System.Collections.Generic;

namespace Optimizer.Lookup
{
    internal class Moves : IMoves
    {
        private readonly IDictionary<string, Move> _fastMoves;
        private readonly IDictionary<string, Move> _specialMoves;

        public Moves()
        {
            _fastMoves = new Dictionary<string, Move>();
            var fastMoves = JsonConvert.DeserializeObject<IList<Move>>(Files.FastMoves);
            foreach (var fastMove in fastMoves)
            {
                _fastMoves.Add(fastMove.Name.ToLower(), fastMove);
            }

            _specialMoves = new Dictionary<string, Move>();
            var specialMoves = JsonConvert.DeserializeObject<IList<Move>>(Files.SpecialMoves);
            foreach (var specialMove in specialMoves)
            {
                _specialMoves.Add(specialMove.Name.ToLower(), specialMove);
            }
        }

        public Move GetFastMove(string moveName)
        {
            return _fastMoves[moveName.ToLower()];
        }

        public Move GetSpecialMove(string moveName)
        {
            return _specialMoves[moveName.ToLower()];
        }
    }
}
