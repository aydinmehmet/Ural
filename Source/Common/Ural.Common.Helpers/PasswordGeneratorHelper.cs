using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ural.Common.Helpers
{
    /// <summary>
    /// The PasswordGeneratorHelper Class 
    /// </summary>
    public static class PasswordGeneratorHelper
    {
        /// <summary>
        /// Generates the specified required length.
        /// </summary>
        /// https://stackoverflow.com/questions/28480167/asp-net-identity-generate-random-password/46229180#46229180
        /// https://www.ryadel.com/en/c-sharp-random-password-generator-asp-net-core-mvc/
        /// https://github.com/Darkseal/PasswordGenerator/blob/master/LICENSE
        /// <param name="requiredLength">Length of the required.</param>
        /// <param name="requiredUniqueChars">The required unique chars.</param>
        /// <param name="requireDigit">if set to <c>true</c> [require digit].</param>
        /// <param name="requireLowercase">if set to <c>true</c> [require lowercase].</param>
        /// <param name="requireNonAlphanumeric">if set to <c>true</c> [require non alphanumeric].</param>
        /// <param name="requireUppercase">if set to <c>true</c> [require uppercase].</param>
        /// <returns></returns>
        public static string Generate(
            int requiredLength          = 8,
            int requiredUniqueChars     = 4,
            bool requireDigit           = true,
            bool requireLowercase       = true,
            bool requireNonAlphanumeric = true,
            bool requireUppercase       = true)
        {
            string[] randomChars = new[] {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",
                "abcdefghijkmnopqrstuvwxyz",
                "0123456789",
                "!@$?_-"
            };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (requireUppercase)
            {
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);
            }

            if (requireLowercase)
            {
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);
            }

            if (requireDigit)
            {
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);
            }

            if (requireNonAlphanumeric)
            {
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);
            }

            for (int i = chars.Count; i < requiredLength
                || chars.Distinct().Count() < requiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];

                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
        /// <summary>
        /// Generates the specified password options.
        /// </summary>
        /// <param name="passwordOptions">The password options.</param>
        /// <returns></returns>
        public static string Generate(PasswordOptions passwordOptions)
        {
            if (passwordOptions == null)
            {
                passwordOptions = new PasswordOptions()
                {
                    RequiredLength         = 8,
                    RequiredUniqueChars    = 4,
                    RequireDigit           = true,
                    RequireLowercase       = true,
                    RequireNonAlphanumeric = true,
                    RequireUppercase       = true
                };
            }

            string generate = Generate(
                requiredLength        : passwordOptions.RequiredLength,
                requiredUniqueChars   : passwordOptions.RequiredUniqueChars,
                requireDigit          : passwordOptions.RequireDigit,
                requireLowercase      : passwordOptions.RequireLowercase,
                requireNonAlphanumeric: passwordOptions.RequireNonAlphanumeric,
                requireUppercase      : passwordOptions.RequireUppercase);

            return generate;
        }
    }
}
