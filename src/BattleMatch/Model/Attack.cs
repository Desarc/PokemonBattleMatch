namespace Optimizer.Model
{
    internal class Attack
    {
        public Attack(string name, string type)
        {
            Name = name;
            Type = type;
        }

        public string Type { get; }

        public string Name { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
