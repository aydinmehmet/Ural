using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Ural.Common.DelegatingHandlers
{
    /// <summary>
    /// The LocalizationDelegatingHandler Class 
    /// </summary>
    /// <seealso cref="System.Net.Http.DelegatingHandler" />
    public class LocalizationDelegatingHandler : DelegatingHandler
    {
        /// <summary>
        /// The HTTP context accessor
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizationDelegatingHandler"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <exception cref="System.ArgumentNullException">httpContextAccessor</exception>
        public LocalizationDelegatingHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        /// <summary>
        /// Sends an HTTP request to the inner handler to send to the server as an asynchronous operation.
        /// </summary>
        /// <param name="request">The HTTP request message to send to the server.</param>
        /// <param name="cancellationToken">A cancellation token to cancel operation.</param>
        /// <returns>
        /// The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">request</exception>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Accept-Language", out StringValues values))
            {
                string requestLanguage = values.ToString().Split(',').FirstOrDefault();

                StringWithQualityHeaderValue qualityHeaderValue = new StringWithQualityHeaderValue(requestLanguage);

                request.Headers.AcceptLanguage.Add(qualityHeaderValue);
            }

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

            return response;
        }
    }
}
