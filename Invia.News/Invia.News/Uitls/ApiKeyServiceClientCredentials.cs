using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Rest;

namespace Invia.News.Uitls
{

    /// <summary>
    /// Helper class to simplifiy the usage of http requests for Azure services.
    /// 
    /// Based on:
    ///     - https://docs.microsoft.com/en-us/azure/cognitive-services/text-analytics/quickstarts/csharp
    /// </summary>
    class ApiKeyServiceClientCredentials : ServiceClientCredentials
    {
        private readonly string apiKey;

        /// <summary>
        /// Init.
        /// 
        /// Will set the api key.
        /// </summary>
        /// <param name="apiKey">Azure API Key</param>
        public ApiKeyServiceClientCredentials(string apiKey)
        {
            this.apiKey = apiKey;
        }

        /// <summary>
        /// Processes given request with api key http header values.
        /// </summary>
        /// <param name="request">Request to process.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Ocp-Apim-Subscription-Key", apiKey);
            return base.ProcessHttpRequestAsync(request, cancellationToken);
        }
    }
}
