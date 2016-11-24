using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using ModernHttpClient;

namespace WineTracker.Helpers
{
    internal static class HttpClientConnector
    {
        private static readonly Lazy<HttpClient> HttpClientConnection = new Lazy<HttpClient>(() =>
        {

            var baseAddress = new Uri("https://services.techverseenterprise.com:444");
            var cookieContainer = new CookieContainer();
            
            var httpClientHandler = new NativeMessageHandler
            {
                CookieContainer = cookieContainer
            };

            if (httpClientHandler.SupportsAutomaticDecompression)
            {
                httpClientHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }

            var authData = $"{""}:{""}";
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));


            var client = new HttpClient(httpClientHandler)
            {
                BaseAddress = baseAddress,
                MaxResponseContentBufferSize = 256000
            };


            client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authHeaderValue);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.MediaTypeNames.ApplicationJson));
            return client;


        }, LazyThreadSafetyMode.ExecutionAndPublication);
        public static HttpClient Instance => HttpClientConnection.Value;
    }
}
