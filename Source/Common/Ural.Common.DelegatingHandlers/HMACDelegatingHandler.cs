using System;
using System.Globalization;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;


namespace Ural.Common.DelegatingHandlers
{
    /// <summary>
    /// The HMACDelegatingHandler Class 
    /// </summary>
    /// <seealso cref="System.Net.Http.DelegatingHandler" />
    public class HMACDelegatingHandler : DelegatingHandler
    {
        /// <summary>
        /// The options
        /// </summary>
        private readonly HMACDelegatingHandlerOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="HMACDelegatingHandler"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <exception cref="System.ArgumentNullException">options</exception>
        public HMACDelegatingHandler(HMACDelegatingHandlerOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="HMACDelegatingHandler"/> class.
        /// </summary>
        /// <param name="appId">The application identifier.</param>
        /// <param name="apiKey">The API key.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <exception cref="System.ArgumentNullException">
        /// appId
        /// or
        /// apiKey
        /// </exception>
        public HMACDelegatingHandler(string appId, string apiKey, bool isActive)
        {
            if (string.IsNullOrEmpty(appId))
            {
                throw new ArgumentNullException(nameof(appId));
            }

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentNullException(nameof(apiKey));
            }

            _options = new HMACDelegatingHandlerOptions
            {
                ApplicationId  = appId,
                ApplicationKey = apiKey,
                IsActive       = isActive
            };
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

            if (!_options.IsActive)
            {
                return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            }

            byte[] requestContentHash         = null;
            string requestContentBase64String = string.Empty;
            string requestUri                 = HttpUtility.UrlEncode(request.RequestUri.AbsoluteUri.ToUpperInvariant());
            string requestHttpMethod          = request.Method.Method;
            DateTime epochStart               = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan timeSpan                 = DateTime.UtcNow - epochStart;
            string requestTimeStamp           = Convert.ToUInt64(timeSpan.TotalSeconds, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
            string nonce                      = Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture);

            if (request.Content != null)
            {
                string contentString = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
                byte[] content       = Encoding.UTF8.GetBytes(contentString);

                using (HMACSHA256 hmac = new HMACSHA256())
                {
                    requestContentHash = hmac.ComputeHash(content);
                }

                requestContentBase64String = Convert.ToBase64String(requestContentHash);
            }

            string signatureRawData   = $"{_options.ApplicationId}{requestHttpMethod}{requestUri}{requestTimeStamp}{nonce}{requestContentBase64String}";
            byte[] secretKeyByteArray = Convert.FromBase64String(_options.ApplicationKey);
            byte[] signature          = Encoding.UTF8.GetBytes(signatureRawData);

            using (HMACSHA256 hmac = new HMACSHA256(secretKeyByteArray))
            {
                byte[] signatureBytes               = hmac.ComputeHash(signature);
                string requestSignatureBase64String = Convert.ToBase64String(signatureBytes);

                request.Headers.Add("x-Authorization-Schema", "amx");
                request.Headers.Add("x-Authorization", $"{_options.ApplicationId}:{requestSignatureBase64String}:{nonce}:{requestTimeStamp}");
            }

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}