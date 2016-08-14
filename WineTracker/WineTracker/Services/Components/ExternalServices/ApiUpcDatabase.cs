
using System;
using System.Linq;
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

        [Get("/prod/trial/lookup?upc={upc}")]
        Task<UpcItemDb> UpcItemDb(string upc);
    }


    public class ApiUpcDatabase : IApiUpcDatabase
    {
        private HttpClient client;
        private IUpcDatabase _upcDatabaseApi;

        public ApiUpcDatabase()
        {
            client = FreshTinyIoC.FreshTinyIoCContainer.Current.Resolve<HttpClient>();

        }

        public async Task<ProductInfo> GetInfoByUpc(string upc)
        {
            var productInfo =  await TryUpcSource(upc) ?? new ProductInfo();
            return productInfo;
        }

        public async Task<ProductInfo> TryUpcSource(string upc)
        {

            var productInfo = new ProductInfo();
            client.BaseAddress = new Uri("http://api.upcdatabase.org/json");
            _upcDatabaseApi = RestService.For<IUpcDatabase>(client);
            var upcdatabase = await _upcDatabaseApi.GetInfoByUpc(upc);
            if (upcdatabase?.itemname != null)
            {
                productInfo.itemname = upcdatabase.itemname;
                productInfo.description = upcdatabase.description;

                return productInfo;
            }

            client.BaseAddress = new Uri("https://api.upcitemdb.com");
            _upcDatabaseApi = RestService.For<IUpcDatabase>(client);
            var upcItemDb = await _upcDatabaseApi.UpcItemDb(upc);

            if (upcItemDb != null)
            {
                productInfo.itemname = upcItemDb.items.First().title;
                productInfo.description = upcItemDb.items.First().description;

                return productInfo;
            }

            return productInfo;
        } 
    }
}
