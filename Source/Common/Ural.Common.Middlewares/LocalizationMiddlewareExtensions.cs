using Microsoft.AspNetCore.Builder;
using System;

namespace Ural.Common.Middlewares
{
    /// <summary>
    /// The LocalizationMiddlewareExtensions Class 
    /// </summary>
    public static class LocalizationMiddlewareExtensions
    {
        /// <summary>
        /// Uses the localization middleware.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="configureOptions">The configure options.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">configureOptions</exception>
        public static IApplicationBuilder UseLocalizationMiddleware(this IApplicationBuilder builder, Action<LocalizationMiddlewareOptions> configureOptions)
        {
            if (configureOptions == null)
            {
                throw new ArgumentNullException(nameof(configureOptions));
            }

            LocalizationMiddlewareOptions options = new LocalizationMiddlewareOptions();
            configureOptions(options);

            return builder.UseMiddleware<LocalizationMiddleware>(options);
        }
    }
}
