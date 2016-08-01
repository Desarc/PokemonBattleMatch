using Optimizer.Analysis;
using Optimizer.Lookup;
using Optimizer.Model;
using Optimizer.Persistence;
using System;
using System.IO;

namespace Optimizer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var attacks = new Attacks();
            var pokemonTemplates = new PokemonTemplates();
            var typeMatchups = new TypeMatchups();
            var pokemonFactory = new PokemonFactory(attacks, pokemonTemplates);
            var matchupCalculator = new MatchupCalculator(typeMatchups, pokemonTemplates, pokemonFactory);

            var resultWriter = new ResultWriter(pokemonTemplates, pokemonFactory, matchupCalculator);

            resultWriter.WriteAllMatchupResults();
            resultWriter.WriteCPRankings();
            resultWriter.WriteHPRankings();
            resultWriter.WriteTotalRankings();
            resultWriter.WriteAllCPAdjustedMatchupResults();
            resultWriter.WriteMatchupRankings();
            resultWriter.WriteCPAdjustedMatchupRankings();

            //while (true)
            //{
                //Console.WriteLine("Enter Pokemon name");
                //var pokemonName = Console.ReadLine();
                //Console.WriteLine("Enter fast attack name");
                //var fastAttackName = Console.ReadLine();
                //Console.WriteLine("Enter special attack name");
                //var specialAttackName = Console.ReadLine();

                //var pokemon = _pokemonFactory.CreatePokemon(pokemonName, fastAttackName, specialAttackName);
                //var matchups = matchupCalculator.FindFavorableAttackMatchups(pokemonName);
            //}
        }
    }
}
