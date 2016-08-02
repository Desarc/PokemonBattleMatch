namespace Optimizer.Model
{
    internal class Move
    {
        public Move(string name, string type, double dps, double criticalHitChance)
        {
            Name = name;
            Type = type;
            DPS = dps;
            CriticalHitChance = criticalHitChance;
        }

        public string Type { get; }

        public string Name { get; }

        public double DPS { get; }

        public double CriticalHitChance { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
