using System;
using System.Text.Json;

namespace Ural.Common.Helpers
{
    /// <summary>
    /// The JsonHelper Class 
    /// https://stackoverflow.com/questions/14977848/how-to-make-sure-that-string-is-valid-json-using-json-net/14977915#14977915
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// Determines whether [is valid json].
        /// </summary>
        /// <param name="jsonString">The json string.</param>
        /// <returns>
        ///   <c>true</c> if [is valid json] [the specified json string]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidJson(this string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString))
            {
                return false;
            }

            jsonString = jsonString.Trim();

            if (string.IsNullOrWhiteSpace(jsonString) ||
                (!jsonString.StartsWith("{", StringComparison.OrdinalIgnoreCase) && !jsonString.EndsWith("}", StringComparison.OrdinalIgnoreCase)) ||
                (!jsonString.StartsWith("[", StringComparison.OrdinalIgnoreCase) && !jsonString.EndsWith("]", StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            try
            {
                string bytes = JsonSerializer.Serialize(jsonString);
                bytes = null;

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
