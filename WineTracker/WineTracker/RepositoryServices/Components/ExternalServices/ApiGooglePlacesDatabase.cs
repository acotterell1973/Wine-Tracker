using System;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using WineTracker.Models;

namespace WineTracker.RepositoryServices.Components.ExternalServices
{
    [Headers("Accept: application/json")]
    internal interface IGeoCodeDatabase
    {
        [Get("/maps/api/geocode/json?latlng={lat},{longt}&key=AIzaSyC1_I8NZs7-nVXNPMd-qfWm8SXp3xLHEZY")]
        Task<GeoCode> GetAddressByGeoCode(string lat, string longt);
    }


    public class ApiGooglePlacesDatabase : IApiGooglePlacesDatabase
    {
        private readonly HttpClient _client;
        private IGeoCodeDatabase _upcDatabaseApi;

        public ApiGooglePlacesDatabase()
        {
            _client = FreshTinyIoC.FreshTinyIoCContainer.Current.Resolve<HttpClient>();
        }

        public async Task<GeoCode> GetAddressByGeoCodeTask(string lat, string longt)
        {
            
            _client.BaseAddress = new Uri("https://maps.google.com/");
            _upcDatabaseApi = RestService.For<IGeoCodeDatabase>(_client);
            var geoCode = await _upcDatabaseApi.GetAddressByGeoCode(lat, longt);

            return geoCode;
        }
    }
}
