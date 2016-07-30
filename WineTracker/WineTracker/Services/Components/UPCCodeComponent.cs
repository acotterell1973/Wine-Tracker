using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Polly;
using WineTracker.Models;
using Xamarin;
// ReSharper disable ImplicitlyCapturedClosure

namespace WineTracker.Services.Components
{
    public class UpcCodeComponent : ComponentBase, IUpcCodeComponent
    {
        private readonly HttpClient _client;

        public UpcCodeComponent(HttpClient client)
        {
            _client = client;
        }
        public async Task<ProductInfo> GetProductByUpcCode(CancellationToken cancellationToken, string upccode)
        {
            ProductInfo product = null;
          
            var policy = Policy.Handle<Exception>().RetryAsync(3, (exception, attempt) =>
            {
                Insights.Report(exception, new Dictionary<string,string>
                {
                    { "Number Of Attempts", attempt.ToString()},
                    { "UPC COde", upccode}
                });
            });
            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    using (Insights.TrackTime("ProductInfo", "GetProductByUpcCode", upccode))
                    {
                        var response = await _client.GetAsync($"http://api.upcdatabase.org/json/25d857d64d51eaa72ac268b056472726/{upccode}", 
                            cancellationToken);
                        if (response.IsSuccessStatusCode)
                        {
                            var jsonMessage = await response.Content.ReadAsStringAsync();
                            product = JsonConvert.DeserializeObject<ProductInfo>(jsonMessage);
                        }
                    }
                });
            }
            catch (Exception exception)
            {
                Insights.Report(exception);
            }

            return product;
        }
    }
}
