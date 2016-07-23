namespace Optimizer.Lookup
{
    internal interface ITypeMatchups
    {
        double GetModifier(string attackingType, string defendingType);

        double GetModifier(string attackingType, string firstDefendingType, string secondDefendingType);
    }
}
