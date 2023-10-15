// <copyright file="RandomHelper.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Core.Utilities
{
    using System.Text;

    public static class RandomHelper
    {
        private static readonly Random Rnd = new Random();

        private static readonly string Alphabet = "ABCDEFGHIKLMNOPQRSTVXYZabcdefghiklmnopqrstvxyz";
        private static readonly string Numbers = "0123456789";

        public static string AlphabeticalString(int size)
        {
            return RandomString(Alphabet, size);
        }

        public static string NumericString(int size)
        {
            return RandomString(Numbers, size);
        }

        public static string RandomString(string source, int size)
        {
            var builder = new StringBuilder();
            var length = source.Length;

            for (var i = 0; i < size; i++)
            {
                builder.Append(source[Rnd.Next(0, length)]);
            }

            return builder.ToString();
        }

        public static string RandomPassword()
        {
            return "_Welcome" + NumericString(3) + "!";
        }

        public static string RandomEmailAddress(
            string loginStartWith = "rptest",
            string prefix = "",
            string domainName = "gmail.com")
        {
            return $"{loginStartWith}{prefix}_{Util.GetCurrentDateTime()}@{domainName}";
        }
    }
}
