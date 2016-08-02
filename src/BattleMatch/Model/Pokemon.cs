namespace Optimizer.Model
{
    internal class Pokemon
    {
        public Pokemon(int number, string name, Move fastMove, Move specialMove, double maxCP, string firstType, string secondType = null)
        {
            Number = number;
            Name = name;
            FastMove = fastMove;
            SpecialMove = specialMove;
            MaxCP = maxCP;
            FirstType = firstType;
            SecondType = secondType;
        }

        public int Number { get; }

        public string NumberString => string.Format("{0,3:D3}", Number);

        public string Name { get; }

        public Move FastMove { get; }

        public Move SpecialMove { get; }

        public double MaxCP { get; }

        public string FirstType { get; }

        public string SecondType { get; }

        public bool SameTypeAttackBonusAppliesToFastMove => FastMove.Type == FirstType || FastMove.Type == SecondType;

        public bool SameTypeAttackBonusAppliesToSpecialMove => SpecialMove.Type == FirstType || SpecialMove.Type == SecondType;

        public override string ToString()
        {
            return $"({NumberString}) {Name} - {FastMove} - {SpecialMove}";
        }
    }
}
