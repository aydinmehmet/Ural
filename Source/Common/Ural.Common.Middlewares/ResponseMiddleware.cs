using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Ural.Common.Helpers;
using Ural.Common.Models.Response;

namespace Ural.Common.Middlewares
{
    /// <summary>
    /// The ResponseMiddleware Class 
    /// </summary>
    public class ResponseMiddleware
    {
        /// <summary>
        /// The next
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        /// <exception cref="System.ArgumentNullException">next</exception>
        public ResponseMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
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

            if (IsSwagger(httpContext))
            {
                await _next.Invoke(httpContext).ConfigureAwait(false);
            }
            else
            {
                if (httpContext.Response != null)
                {
                    await BuildAPIResponseAsync(httpContext).ConfigureAwait(false);
                }
            }
        }
        /// <summary>
        /// Determines whether the specified HTTP context is swagger.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <returns>
        ///   <c>true</c> if the specified HTTP context is swagger; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsSwagger(HttpContext httpContext)
        {
            return httpContext.Request.Path.StartsWithSegments("/swagger", StringComparison.InvariantCultureIgnoreCase);
        }
        /// <summary>
        /// Determines whether the specified pagination string is pagination.
        /// </summary>
        /// <param name="paginationString">The pagination string.</param>
        /// <returns>
        ///   <c>true</c> if the specified pagination string is pagination; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsPagination(string paginationString)
        {
            return paginationString.Contains("CurrentPage", StringComparison.InvariantCultureIgnoreCase);
        }
        /// <summary>
        /// Builds the API response asynchronous.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <exception cref="System.ArgumentNullException">httpContext</exception>
        private async Task BuildAPIResponseAsync(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            string content = null;
            string result  = null;

            using (Stream responseBody = httpContext.Response.Body)
            {
                httpContext.Response.Body = new MemoryStream();

                await _next.Invoke(httpContext).ConfigureAwait(false);

                httpContext.Response.Body.Seek(0, SeekOrigin.Begin);

                using (BufferedStream bufferedStream = new BufferedStream(httpContext.Response.Body, (int)httpContext.Response.Body.Length))
                {
                    using (StreamReader streamReader = new StreamReader(bufferedStream))
                    {
                        while ((content = await streamReader.ReadLineAsync().ConfigureAwait(false)) != null)
                        {
                            result += content;
                        }

                        ResponseModel responseModel = new ResponseModel()
                        {
                            IsPagination = IsPagination(result),
                            StatusCode   = httpContext.Response.StatusCode,
                            Success      = httpContext.Response.StatusCode <= 299,
                            Result       = result.IsValidJson() ? JsonSerializer.Deserialize<object>(result) : result
                        };

                        string response = responseModel.ToString();

                        httpContext.Response.ContentType   = "application/json";
                        httpContext.Response.ContentLength = response.Length;
                        httpContext.Response.Body          = responseBody;

                        await httpContext.Response.WriteAsync(response).ConfigureAwait(false);
                    }
                }
            }
        }
    }
}
