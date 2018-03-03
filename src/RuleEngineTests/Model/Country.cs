﻿using System.Globalization;
using System.Linq;

namespace RuleEngine.Tests.Model
{
    public class Country
    {
        public static string[] Countries = CultureInfo.GetCultures(CultureTypes.AllCultures).Select(c=>c.Name).ToArray();
        public string CountryCode { get; set; }
    }
}