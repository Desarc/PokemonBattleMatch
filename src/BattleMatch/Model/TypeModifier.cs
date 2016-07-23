namespace Optimizer.Model
{
    internal class TypeModifier
    {
        public TypeModifier(string defendingType, double modifier)
        {
            DefendingType = defendingType;
            Modifier = modifier;
        }

        public string DefendingType { get; }

        public double Modifier { get; }
    }
}
