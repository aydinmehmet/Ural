using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ural.Common.Middlewares
{
    /// <summary>
    /// The LocalizationMiddleware Class 
    /// </summary>
    public class LocalizationMiddleware
    {
        /// <summary>
        /// The next
        /// </summary>
        private readonly RequestDelegate _next;
        /// <summary>
        /// The options
        /// </summary>
        private readonly LocalizationMiddlewareOptions _options;
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizationMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        /// <param name="options">The options.</param>
        /// <exception cref="System.ArgumentNullException">
        /// next
        /// or
        /// options
        /// </exception>
        public LocalizationMiddleware(RequestDelegate next, LocalizationMiddlewareOptions options)
        {
            _next    = next ?? throw new ArgumentNullException(nameof(next));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <summary>
        /// Invokes the specified HTTP context.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <exception cref="System.ArgumentNullException">httpContext</exception>
        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            if (httpContext.Request.Headers.TryGetValue("Accept-Language", out StringValues values))
            {
                string requestLanguage = values.ToString().Split(',').FirstOrDefault();

                if (string.IsNullOrWhiteSpace(requestLanguage) || requestLanguage.Length != 5 || !_options.Allowed.Contains(requestLanguage))
                {
                    requestLanguage = _options.Default;
                }

                Thread.CurrentThread.CurrentCulture   = new CultureInfo(requestLanguage);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(requestLanguage);
            }

            await _next.Invoke(httpContext).ConfigureAwait(false);

            return;
        }
    }
}
