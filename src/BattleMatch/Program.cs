using Optimizer.Lookup;
using System;

namespace Optimizer
{
    public class Program
    {
        private static IPokemonFactory _pokemonFactory;
        private static IMatchupCalculator _matchupCalculator;

        static void Main(string[] args)
        {
            var attacks = new Attacks();
            var pokemonTemplates = new PokemonTemplates();
            var typeMatchups = new TypeMatchups();
            _pokemonFactory = new PokemonFactory(attacks, pokemonTemplates);
            _matchupCalculator = new MatchupCalculator(typeMatchups, pokemonTemplates, _pokemonFactory);

            while (true)
            {
                Console.WriteLine("Enter Pokemon name");
                var pokemonName = Console.ReadLine();
                //Console.WriteLine("Enter fast attack name");
                //var fastAttackName = Console.ReadLine();
                //Console.WriteLine("Enter special attack name");
                //var specialAttackName = Console.ReadLine();

                //var pokemon = _pokemonFactory.CreatePokemon(pokemonName, fastAttackName, specialAttackName);
                //var matchups = _matchupCalculator.FindFavorableAttackMatchups(pokemon);
                var matchups = _matchupCalculator.FindFavorableAttackMatchups(pokemonName);

                Console.WriteLine("");
                foreach (var matchup in matchups)
                {
                    Console.WriteLine($"{matchup.Key} - {matchup.Value}");
                }

                Console.WriteLine("");
            }
        }
    }
}
