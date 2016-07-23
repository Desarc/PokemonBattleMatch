using Optimizer.Model;

namespace Optimizer.Lookup
{
    internal interface IAttacks
    {
        Attack GetFastAttack(string attackName);

        Attack GetSpecialAttack(string attackName);
    }
}
