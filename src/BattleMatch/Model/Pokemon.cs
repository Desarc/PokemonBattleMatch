namespace Optimizer.Model
{
    internal class Pokemon
    {
        public Pokemon(string name, Attack fastAttack, Attack specialAttack, string firstType, string secondType = null)
        {
            Name = name;
            FastAttack = fastAttack;
            SpecialAttack = specialAttack;
            FirstType = firstType;
            SecondType = secondType;
        }

        public string Name { get; }

        public Attack FastAttack { get; }

        public Attack SpecialAttack { get; }

        public string FirstType { get; }

        public string SecondType { get; }

        public override string ToString()
        {
            return $"{Name} - {FastAttack} - {SpecialAttack}";
        }
    }
}
