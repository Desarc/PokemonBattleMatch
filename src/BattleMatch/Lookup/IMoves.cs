using Optimizer.Model;

namespace Optimizer.Lookup
{
    internal interface IMoves
    {
        Move GetFastMove(string moveName);

        Move GetSpecialMove(string moveName);
    }
}
