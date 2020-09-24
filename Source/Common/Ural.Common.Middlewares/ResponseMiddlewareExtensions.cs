using Microsoft.AspNetCore.Builder;

namespace Ural.Common.Middlewares
{
    /// <summary>
    /// The ResponseMiddlewareExtensions Class 
    /// </summary>
    public static class ResponseMiddlewareExtensions
    {
        /// <summary>
        /// Uses the response middleware.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static IApplicationBuilder UseResponseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseMiddleware>();
        }
    }
}
