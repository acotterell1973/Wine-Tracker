using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using WineTracker.Models;

namespace WineTracker.RepositoryServices.Components.ExternalServices
{
    [Headers("Accept: application/json")]
    internal interface IUpcDatabase
    {

        [Get("/25d857d64d51eaa72ac268b056472726/{upc}")]
        Task<ProductInfo> GetInfoByUpc(string upc);

        [Get("/prod/trial/lookup?upc={upc}")]
        Task<UpcItemDb> UpcItemDb(string upc);

        [Get("/api/wine/upc/{upc}")]
        Task<WineItemInfo> GetWineInfoByUpc(string upc);
    }


    public class ApiUpcDatabase : IApiUpcDatabase
    {
        private readonly HttpClient _client;
        private IUpcDatabase _upcDatabaseApi;

        public ApiUpcDatabase()
        {
            _client = FreshTinyIoC.FreshTinyIoCContainer.Current.Resolve<HttpClient>();

        }

        public async Task<WineItemInfo> GetInfoByUpc(string upc)
        {
            var productInfo = await TryUpcSource(upc) ?? new WineItemInfo();
            return productInfo;
        }

        public async Task<WineItemInfo> TryUpcSource(string upc)
        {

            _client.BaseAddress = new Uri("http://apiwinehunter.azurewebsites.net");
            _upcDatabaseApi = RestService.For<IUpcDatabase>(_client);
            var productInfo = await _upcDatabaseApi.GetWineInfoByUpc(upc);

            return productInfo;


            //_client.BaseAddress = new Uri("http://api.upcdatabase.org/json");
            //_upcDatabaseApi = RestService.For<IUpcDatabase>(_client);
            //var upcdatabase = await _upcDatabaseApi.GetInfoByUpc(upc);

            //if (upcdatabase?.itemname != null)
            //{
            //    productInfo.itemname = upcdatabase.itemname;
            //    productInfo.description = upcdatabase.description;

            //    return productInfo;
            //}

            ////_client.BaseAddress = new Uri("https://api.upcitemdb.com");
            ////_upcDatabaseApi = RestService.For<IUpcDatabase>(_client);
            //var upcItemDb = await _upcDatabaseApi.UpcItemDb(upc);

            //if (upcItemDb == null || !upcItemDb.items.Any()) return productInfo;

            //productInfo.itemname = upcItemDb.items.First().title;
            //productInfo.description = upcItemDb.items.First().description;

            //return productInfo;
        }
    }
}
