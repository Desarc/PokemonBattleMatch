﻿using Optimizer.Analysis;
using Optimizer.Data;
using Optimizer.Lookup;
using Optimizer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Optimizer.Persistence
{
    internal class ResultWriter : IResultWriter
    {
        private const string ResultsFolder = @"..\..\..\..\results";

        

        private IPokemonTemplates _pokemonTemplates;
        private IPokemonFactory _pokemonFactory;
        private IMatchupCalculator _matchupCalculator;

        public ResultWriter(IPokemonTemplates pokemonTemplates, IPokemonFactory pokemonFactory, IMatchupCalculator matchupCalculator)
        {
            _pokemonTemplates = pokemonTemplates;
            _pokemonFactory = pokemonFactory;
            _matchupCalculator = matchupCalculator;
        }

        public void WriteAllMatchupResults()
        {
            Console.WriteLine("Writing matchup results...");

            foreach (var template in _pokemonTemplates.GetAllTemplates())
            {
                var matchups = _matchupCalculator.FindFavorableAttackMatchups(template.Name, true, false, -1);

                var resultsFolder = $@"{ResultsFolder}\matchups";
                if (!Directory.Exists(resultsFolder))
                {
                    Directory.CreateDirectory(resultsFolder);
                }

                Console.WriteLine($"Writing results for {template.Name}");

                using (StreamWriter file = new StreamWriter($@"{resultsFolder}\{template.NumberString}-{template.Name}.txt"))
                {
                    foreach (var matchup in matchups)
                    {
                        var value = Math.Round(matchup.Value, Constants.MatchupValuePrecision);
                        var matchupString = $"{matchup.Key} - {value}";
                        file.WriteLine(matchupString);
                    }
                }
            }
        }

        public void WriteAllCPAdjustedMatchupResults()
        {
            Console.WriteLine("Writing CP adjusted matchup results...");

            foreach (var template in _pokemonTemplates.GetAllTemplates())
            {
                var matchups = _matchupCalculator.FindFavorableAttackMatchups(template.Name, true, true, -1);

                var resultsFolder = $@"{ResultsFolder}\matchups-cpadjusted";
                if (!Directory.Exists(resultsFolder))
                {
                    Directory.CreateDirectory(resultsFolder);
                }

                Console.WriteLine($"Writing results for {template.Name}");

                using (StreamWriter file = new StreamWriter($@"{resultsFolder}\{template.NumberString}-{template.Name}.txt"))
                {
                    foreach (var matchup in matchups)
                    {
                        var value = Math.Round(matchup.Value, Constants.MatchupValuePrecision);
                        var matchupString = $"{matchup.Key} - {value}";
                        file.WriteLine(matchupString);
                    }
                }
            }
        }

        public void WriteMatchupRankings()
        {
            var allTemplates = _pokemonTemplates.GetAllTemplates(onlyMaxStage: true);
            var allMatchups = new List<KeyValuePair<Pokemon, double>>();

            foreach (var template in allTemplates)
            {
                allMatchups.AddRange(_matchupCalculator.FindFavorableAttackMatchups(template.Name, onlyMaxStage: true, adjustForCP: false, modifierLimit: -1).ToList());
            }

            var aggregatedMatchupValues = new Dictionary<Pokemon, double>();

            //foreach (var template in allTemplates)
            //{
            //    var aggregatedMatchupValue = allMatchups.Where(am => am.Key.Name == template.Name).Sum(am => am.Value);
            //    aggregatedMatchupValues[template] = aggregatedMatchupValue;
            //}

            foreach (var pokemon in _pokemonTemplates.GetAllPermutations(_pokemonFactory, onlyMaxStage: true))
            {
                var aggregatedMatchupValue = allMatchups.Where(am => am.Key.Name == pokemon.Name && am.Key.FastMove == pokemon.FastMove && am.Key.SpecialMove == pokemon.SpecialMove).Sum(am => am.Value);
                aggregatedMatchupValues[pokemon] = aggregatedMatchupValue;
            }

            var sortedMatchupValues = aggregatedMatchupValues.OrderByDescending(am => am.Value);

            Console.WriteLine("Writing matchup rankings...");

            using (StreamWriter file = new StreamWriter($@"{ResultsFolder}\matchup-ranking.txt"))
            {
                var rankingNumber = 1;

                foreach (var matchupValue in sortedMatchupValues)
                {
                    var templateString = $"{rankingNumber} - {matchupValue.Key.ToString()}: {matchupValue.Value}";
                    file.WriteLine(templateString);
                    rankingNumber++;
                }
            }
        }

        public void WriteCPAdjustedMatchupRankings()
        {
            var allTemplates = _pokemonTemplates.GetAllTemplates(onlyMaxStage: true);
            var allMatchups = new List<KeyValuePair<Pokemon, double>>();

            foreach (var template in allTemplates)
            {
                allMatchups.AddRange(_matchupCalculator.FindFavorableAttackMatchups(template.Name, onlyMaxStage: true, adjustForCP: true, modifierLimit: -1).ToList());
            }

            var aggregatedMatchupValues = new Dictionary<Pokemon, double>();

            //foreach (var template in allTemplates)
            //{
            //    var aggregatedMatchupValue = allMatchups.Where(am => am.Key.Name == template.Name).Sum(am => am.Value);
            //    aggregatedMatchupValues[template] = aggregatedMatchupValue;
            //}

            foreach (var pokemon in _pokemonTemplates.GetAllPermutations(_pokemonFactory, onlyMaxStage: true))
            {
                var aggregatedMatchupValue = allMatchups.Where(am => am.Key.Name == pokemon.Name && am.Key.FastMove == pokemon.FastMove && am.Key.SpecialMove == pokemon.SpecialMove).Sum(am => am.Value);
                aggregatedMatchupValues[pokemon] = aggregatedMatchupValue;
            }

            var sortedMatchupValues = aggregatedMatchupValues.OrderByDescending(am => am.Value);

            Console.WriteLine("Writing CP-adjusted matchup rankings...");

            using (StreamWriter file = new StreamWriter($@"{ResultsFolder}\matchup-ranking-cpadjusted.txt"))
            {
                var rankingNumber = 1;

                foreach (var matchupValue in sortedMatchupValues)
                {
                    var templateString = $"{rankingNumber} - {matchupValue.Key.ToString()}: {matchupValue.Value}";
                    file.WriteLine(templateString);
                    rankingNumber++;
                }
            }
        }

        public void WriteCPRankings()
        {
            var result = from template in _pokemonTemplates.GetAllTemplates()
                         orderby template.MaxCP descending
                         select template;

            Console.WriteLine("Writing CP rankings...");

            using (StreamWriter file = new StreamWriter($@"{ResultsFolder}\CP-ranking.txt"))
            {
                var rankingNumber = 1;

                foreach (var template in result)
                {
                    var templateString = $"{rankingNumber} - ({template.NumberString}) {template.Name}: {template.MaxCP}";
                    file.WriteLine(templateString);
                    rankingNumber ++;
                }
            }
        }

        public void WriteHPRankings()
        {
            var results = from template in _pokemonTemplates.GetAllTemplates()
                         orderby template.MaxHP descending
                         select template;

            Console.WriteLine("Writing HP rankings...");

            using (StreamWriter file = new StreamWriter($@"{ResultsFolder}\HP-ranking.txt"))
            {
                var rankingNumber = 1;

                foreach (var template in results)
                {
                    var templateString = $"{rankingNumber} - ({template.NumberString}) {template.Name}: {template.MaxHP}";
                    file.WriteLine(templateString);
                    rankingNumber++;
                }
            }
        }

        public void WriteTotalRankings()
        {
            var totalMaxHP = _pokemonTemplates.GetMaxHP();
            var totalMaxCP = _pokemonTemplates.GetMaxCP();

            var results = from template in _pokemonTemplates.GetAllTemplates()
                          let totalValue = CalculateTotalValue(template, totalMaxHP, totalMaxCP)
                          orderby totalValue descending
                          select $"({template.NumberString}) {template.Name}: {totalValue}";

            Console.WriteLine("Writing total rankings...");

            using (StreamWriter file = new StreamWriter($@"{ResultsFolder}\total-ranking.txt"))
            {
                var rankingNumber = 1;

                foreach (var result in results)
                {
                    var templateString = $"{rankingNumber} - {result}";
                    file.WriteLine(templateString);
                    rankingNumber++;
                }
            }
        }

        // TODO: make it right
        private double CalculateTotalValue(PokemonTemplate template, double totalMaxHP, double totalMaxCP)
        {
            return (template.MaxHP / totalMaxHP) + (template.MaxCP / totalMaxCP) * Constants.CPValueModifier;
        }
    }
}
