﻿using System;
using System.Linq;
using RuleFactory.Tests.Model;

namespace RuleFactory.Tests.Fixture
{
    public class ValidationRulesJsonFixture : IDisposable
    {
        public void Dispose() { }

        public Game Game { get; }

        public ValidationRulesJsonFixture()
        {
            var someRandomNumber = new Random();

            Game = new Game
            {
                Name = "Game 1",
                Description = "super boring game",
                Active = false,
                Ranking = 99,
                Rating = "High"
            };
            Game.Players.AddRange(Enumerable.Range(1, 4).Select(x => new Player
            {
                Id = x,
                Name = $"Player{x}",
                Country = new Country
                {
                    CountryCode = Country.Countries[someRandomNumber.Next(x, Country.Countries.Length - 1)]
                },
                CurrentScore = 100 - x,
                CurrentCoOrdinates = new CoOrdinate { X = x, Y = x }
            }));
        }
    }
}