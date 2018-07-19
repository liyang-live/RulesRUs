﻿using System;
using FluentAssertions;
using RuleEngine.Rules;
using SampleModel;
using Xunit;
using Xunit.Abstractions;

namespace RuleEngine.Tests.Rules
{
    public class BlockRulesTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public BlockRulesTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void UpdateMultiplePropertiesOfaGameObject()
        {
            var nameChangeRule = new UpdateValueRule<Game>
            {
                ObjectToUpdate = "Name",
                SourceDataRule = new ConstantRule<string> {Value = "some fancy name"}
            };
            var rankingChangeRule = new UpdateValueRule<Game>
            {
                ObjectToUpdate = "Ranking",
                SourceDataRule = new ConstantRule<int>{Value = "1000"}
            };
            var descriptionChangeRule = new UpdateValueRule<Game>
            {
                ObjectToUpdate = "Description",
                SourceDataRule = new ConstantRule<string>{Value = "some cool description"}
            };

            var blockRule = new ActionBlockRule<Game>();
            blockRule.Rules.Add(nameChangeRule);
            blockRule.Rules.Add(rankingChangeRule);
            blockRule.Rules.Add(descriptionChangeRule);

            var compileResult = blockRule.Compile();
            compileResult.Should().BeTrue();

            _testOutputHelper.WriteLine(blockRule.ExpressionDebugView());

            var game = new Game();
            blockRule.Exectue(game);
            _testOutputHelper.WriteLine($"game object updated:{Environment.NewLine}{game}");
        }
    }
}