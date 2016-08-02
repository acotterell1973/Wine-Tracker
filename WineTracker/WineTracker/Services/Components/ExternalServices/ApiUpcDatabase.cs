
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Refit;
using WineTracker.Models;

namespace WineTracker.Services.Components.ExternalServices
{
    [Headers("Accept: application/json")]
    internal interface IUpcDatabase
    {

        [Get("/25d857d64d51eaa72ac268b056472726/{upc}")]
        Task<ProductInfo> GetInfoByUpc(string upc);
    }


    public class ApiUpcDatabase : IApiUpcDatabase
    {

        private readonly IUpcDatabase _upcDatabaseApi;

        public ApiUpcDatabase()
        {
            var client = FreshTinyIoC.FreshTinyIoCContainer.Current.Resolve<HttpClient>();
            client.BaseAddress = new Uri("http://api.upcdatabase.org/json");
            _upcDatabaseApi = RestService.For<IUpcDatabase>(client);
        }
        public async Task<ProductInfo> GetInfoByUpc(string upc)
        {
            var productInfo =  await _upcDatabaseApi.GetInfoByUpc(upc) ?? new ProductInfo();
            return productInfo;
        }
    }
}
