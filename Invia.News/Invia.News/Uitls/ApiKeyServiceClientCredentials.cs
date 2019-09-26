using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Rest;

namespace Invia.News.Uitls
{

    /// <summary>
    /// https://docs.microsoft.com/de-de/azure/cognitive-services/text-analytics/quickstarts/csharp
    /// </summary>
    class ApiKeyServiceClientCredentials : ServiceClientCredentials
    {
        private readonly string apiKey;

        public ApiKeyServiceClientCredentials(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Ocp-Apim-Subscription-Key", apiKey);
            return base.ProcessHttpRequestAsync(request, cancellationToken);
        }
    }
}
