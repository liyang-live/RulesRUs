﻿using System;
using System.Diagnostics;
using System.Linq.Expressions;
using RuleEngine.Common;
using RuleEngine.Interfaces.Rules;
using RuleEngine.RuleCompilers;
using RuleEngine.Utils;

namespace RuleEngine.Rules
{
    public class UpdateRule<T1, T2> : Rule, IUpdateRule<T1, T2>
    {
        private Action<T1, T2> CompiledDelegate { get; set; }
        public string ObjectToUpdate;

        public override Expression BuildExpression(params ParameterExpression[] parameters)
        {
            if (parameters == null || parameters.Length != 2 || parameters[0].Type != typeof(T1) || parameters[1].Type != typeof(T2))
                throw new RuleEngineException($"{nameof(BuildExpression)} must call with two parameters of {typeof(T1)} and {typeof(T2)}");

            var targetObject = parameters[0];
            var sourceParam = parameters[1];

            var targetExpression = RuleCompilerBase.GetExpressionWithSubProperty(targetObject, ObjectToUpdate);
            return Expression.Assign(targetExpression, sourceParam);
        }

        public override bool Compile()
        {
            var paramObjectToValidate = Expression.Parameter(typeof(T1));
            var paramSourceValue = Expression.Parameter(typeof(T2));
            var expression = BuildExpression(paramObjectToValidate, paramSourceValue);
#if DEBUG
            Debug.WriteLine($"Expression for UpdateRule<{typeof(T1)},{typeof(T2)}>: {expression}");
            expression.TraceNode();
#endif
            CompiledDelegate = Expression.Lambda<Action<T1, T2>>(expression, paramObjectToValidate, paramSourceValue).Compile();
            return CompiledDelegate != null;
        }

        public void UpdateFieldOrPropertyValue(T1 targetObject, T2 source)
        {
            if (CompiledDelegate == null)
                throw new RuleEngineException("A Rule must be compiled first");

            CompiledDelegate(targetObject, source);
        }
    }
}