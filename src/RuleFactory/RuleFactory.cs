﻿using System;
using RuleEngine.Rules;
using RuleEngine.Utils;
//using RuleFactory.Factory;

namespace RuleFactory
{
    public static class RuleFactory
    {
        public static Rule CreateRule(string ruleType, string[] boundingTypes)
        {
            switch (ruleType)
            {
                case "ConstantRule`1":
                case "ConstantRule`2":
                    return CreateConstantRule(boundingTypes);
                case "ValidationRule`1":
                case "ValidationRule`2":
                    return CreateValidationRule(boundingTypes);
                case "UpdateValueRule`1":
                case "UpdateValueRule`2":
                    return CreateUpdateValueRule(boundingTypes);
                case "MethodVoidCallRule`1":
                case "MethodCallRule`2":
                    return CreateMethodVoidCallRule(boundingTypes);
                case "ConditionalIfThActionRule`1":
                    return CreateConditionalIfThActionRule(boundingTypes);
                case "ConditionalIfThElActionRule`1":
                    return CreateConditionalIfThElActionRule(boundingTypes);
                case "ConditionalFuncRule`2":
                    return CreateConditionalFunctionRule(boundingTypes);
                case "ContainsValueRule`1":
                    return CreateContainsValueRule(boundingTypes);
                case "RegExRule`1":
                    return CreateRegExRule(boundingTypes);
                default:
                    break;
            }

            return null;
        }

        private static Rule CreateRegExRule(string[] boundingTypes)
        {
            if (boundingTypes == null || boundingTypes.Length != 1) return null;
            return CreateRule(typeof(RegExRule<>),
                new[] { ReflectionExtensions.GetTypeFor(boundingTypes[0]) });
        }

        private static Rule CreateContainsValueRule(string[] boundingTypes)
        {
            if (boundingTypes == null || boundingTypes.Length != 1) return null;
            return CreateRule(typeof(ContainsValueRule<>),
                new[] { ReflectionExtensions.GetTypeFor(boundingTypes[0]) });
        }

        private static Rule CreateConditionalFunctionRule(string[] boundingTypes)
        {
            if (boundingTypes == null || boundingTypes.Length != 2) return null;
            return CreateRule(typeof(ConditionalFuncRule<,>),
                new[]
                {
                    ReflectionExtensions.GetTypeFor(boundingTypes[0]),
                    ReflectionExtensions.GetTypeFor(boundingTypes[1])
                });
        }

        private static Rule CreateConditionalIfThElActionRule(string[] boundingTypes)
        {
            if (boundingTypes == null || boundingTypes.Length != 1) return null;
            return CreateRule(typeof(ConditionalIfThElActionRule<>),
                new[] { ReflectionExtensions.GetTypeFor(boundingTypes[0]) });
        }

        private static Rule CreateConditionalIfThActionRule(string[] boundingTypes)
        {
            if (boundingTypes == null || boundingTypes.Length != 1) return null;

            return CreateRule(typeof(ConditionalIfThActionRule<>),
                    new[] { ReflectionExtensions.GetTypeFor(boundingTypes[0]) });
        }

        private static Rule CreateMethodVoidCallRule(string[] boundingTypes)
        {
            if (boundingTypes == null || boundingTypes.Length < 1 || boundingTypes.Length > 2) return null;

            return (boundingTypes.Length == 1
                ? CreateRule(typeof(MethodVoidCallRule<>), new[] { ReflectionExtensions.GetTypeFor(boundingTypes[0]) })
                : CreateRule(typeof(MethodCallRule<,>),
                    new[]
                    {
                        ReflectionExtensions.GetTypeFor(boundingTypes[0]),
                        ReflectionExtensions.GetTypeFor(boundingTypes[1])
                    }));
        }

        public static Rule CreateConstantRule(string[] boundingTypes)
        {
            if (boundingTypes == null || boundingTypes.Length < 1 || boundingTypes.Length > 2) return null;

            return (boundingTypes.Length == 1
                ? CreateRule(typeof(ConstantRule<>), new[] {ReflectionExtensions.GetTypeFor(boundingTypes[0])})
                : CreateRule(typeof(ConstantRule<,>),
                    new[]
                    {
                        ReflectionExtensions.GetTypeFor(boundingTypes[0]),
                        ReflectionExtensions.GetTypeFor(boundingTypes[1])
                    }));
        }

        public static Rule CreateValidationRule(string[] boundingTypes)
        {
            if (boundingTypes == null || boundingTypes.Length < 1 || boundingTypes.Length > 2) return null;

            return (boundingTypes.Length == 1
                ? CreateRule(typeof(ValidationRule<>), new[] {ReflectionExtensions.GetTypeFor(boundingTypes[0])})
                : CreateRule(typeof(ValidationRule<,>),
                    new[]
                    {
                        ReflectionExtensions.GetTypeFor(boundingTypes[0]),
                        ReflectionExtensions.GetTypeFor(boundingTypes[1])
                    }));
        }

        public static Rule CreateUpdateValueRule(string[] boundingTypes)
        {
            if (boundingTypes == null || boundingTypes.Length < 1 || boundingTypes.Length > 2) return null;

            return (boundingTypes.Length == 1
                ? CreateRule(typeof(UpdateValueRule<>), new[] {ReflectionExtensions.GetTypeFor(boundingTypes[0])})
                : CreateRule(typeof(UpdateValueRule<,>),
                    new[]
                    {
                        ReflectionExtensions.GetTypeFor(boundingTypes[0]),
                        ReflectionExtensions.GetTypeFor(boundingTypes[1])
                    }));

        }

        private static Rule CreateRule(Type genericType, Type[] typesToBoundTo)
        {
            if (genericType == null || typesToBoundTo == null) return null;
            var boundedGenericType = genericType.MakeGenericType(typesToBoundTo);
            var instance = Activator.CreateInstance(boundedGenericType);
            return (Rule)instance;
        }
    }
}
