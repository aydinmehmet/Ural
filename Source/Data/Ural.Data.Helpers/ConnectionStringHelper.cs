using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Ural.Data.Helpers
{
    /// <summary>
    /// The ConnectionStringHelper Class 
    /// </summary>
    public static class ConnectionStringHelper
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public static IConfigurationRoot Configuration => new ConfigurationBuilder()
           .SetBasePath(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location))
           .AddJsonFile("connectionStrings.json", false, true)
           .AddJsonFile($"connectionStrings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true)
           .AddEnvironmentVariables()
           .Build();

        /// <summary>
        /// Connections the string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// value
        /// or
        /// connectionString
        /// </exception>
        public static string ConnectionString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            string connectionString = Configuration.GetValue<string>(value);

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            return connectionString;
        }
    }
}
