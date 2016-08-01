
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Refit;
using WineTracker.Models;

namespace WineTracker.Services.Components.ExternalServices
{
    [Headers("Accept: application/json")]
    public interface IApiUpcDatabase
    {

        [Get("/25d857d64d51eaa72ac268b056472726/{upc}")]
        Task<ProductInfo> GetInfoByUpc(string upc);
    }
    public class ApiUpcDatabase : IApiUpcDatabase
    {
        private IApiUpcDatabase _upcDatabaseApi;

        public ApiUpcDatabase()
        {
            
        }
        public async Task<ProductInfo> GetInfoByUpc(string upc)
        {
            var client = FreshTinyIoC.FreshTinyIoCContainer.Current.Resolve<HttpClient>();
            client.BaseAddress = new Uri("http://api.upcdatabase.org/json");
            _upcDatabaseApi = RestService.For<IApiUpcDatabase>(client);
            return await _upcDatabaseApi.GetInfoByUpc(upc) ?? new ProductInfo();
        }
    }
}
